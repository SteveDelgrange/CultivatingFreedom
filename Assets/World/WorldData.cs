using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWorldData", menuName = "Data/World")]
public class WorldData : ScriptableObject
{
    [Header("Basic settings")]
    public int seed;

    public bool isInfinite = false;
    [Range(1,100)] public int radius = 3;

    public Dictionary<Vector2Int, ChunkData> chunks;
    public bool updateOnChange = false;

    public Texture2D worldTexture;
    public Renderer rend;

    // Width and height of the texture in pixels.
    public int pixWidth;
    public int pixHeight;

    // The origin of the sampled area in the plane.
    public float xOrg;
    public float yOrg;

    // The number of cycles of the basic noise pattern that are repeated
    // over the width and height of the texture.
    public float scale = 1.0F;
    public float alphaClipping = 0.5F;
    

    [Header("Sect settings")]
    public int xSectPos;
    public int ySectPos;
    
    private Color[] pix;

    public void Initialize()
    {
        RandomizeMap();
        chunks = new Dictionary<Vector2Int, ChunkData>();
    }

    public ChunkData GenerateChunkData(Vector2Int position)
    {
        if (chunks.ContainsKey(position)) {
            return chunks[position];
        }

        ChunkData chunk = new ChunkData();
        for(int i = 0; i < ChunkData.size; i++) {
            for (int j = 0; j < ChunkData.size; j++) {
                chunk.cells[i,j] = CellType.Rock;
            }
        }
        chunks[position] = chunk;
        return chunk;
    }

    [ContextMenu("Compute data")]
    void RandomizeMap()
    {

#if UNITY_EDITOR
        System.Random random = new System.Random(seed);
        xOrg = random.Next(-1000000, 1000000);
        yOrg = random.Next(-1000000, 1000000);

        Texture2D tex = new Texture2D(pixWidth, pixHeight, TextureFormat.ARGB32, false);
        pix = new Color[tex.width * tex.height];

        // For each pixel in the texture...
        float y = 0.0F;
        Color voidColor = Color.black;
        Color grassColor = Color.white;
        Color sectColor = Color.red;

        while (y < tex.height) {
            float x = 0.0F;
            while (x < tex.width) {
                float xCoord = xOrg + x / tex.width * scale;
                float yCoord = yOrg + y / tex.height * scale;
                float sample = Mathf.PerlinNoise(xCoord, yCoord);
                int index = (int)y * tex.width + (int)x;
                if (sample < alphaClipping) {
                    pix[index] = voidColor;
                } else {
                    pix[index] = grassColor;
                }
                x++;
            }
            y++;
        }

        if(ySectPos < tex.height && ySectPos >= 0 && xSectPos < tex.width && xSectPos >= 0) {
            pix[ySectPos * tex.width + xSectPos] = sectColor;
        }

        // Copy the pixel data to the texture and load it into the GPU.
        tex.SetPixels(pix);
        tex.Apply();
        if (worldTexture == null || !UnityEditor.AssetDatabase.Contains(worldTexture)) {
            UnityEditor.AssetDatabase.AddObjectToAsset(tex, this);
            worldTexture = tex;
        } else {
            UnityEditor.EditorUtility.CopySerialized(tex, worldTexture);
        }
        UnityEditor.AssetDatabase.SaveAssets();
#endif

    }

    private void OnValidate()
    {
        if (updateOnChange) {
            RandomizeMap();
        }
    }
}
