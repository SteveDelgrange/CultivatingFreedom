using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public enum VoxelType
{
    Void,
    Grass,
    Sect
}

[CreateAssetMenu(fileName = "NewWorldData", menuName = "Data/World")]
public class WorldData : ScriptableObject
{
    [Header("Basic settings")]
    public Texture2D worldTexture;
    [HideInInspector] public VoxelType[] worldVoxels;
    public bool updateOnChange = false;

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

    [ContextMenu("Compute data")]
    void CalcNoise()
    {

#if UNITY_EDITOR
        Texture2D tex = new Texture2D(pixWidth, pixHeight, TextureFormat.ARGB32, false);
        pix = new Color[tex.width * tex.height];
        worldVoxels = new VoxelType[tex.width * tex.height];

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
                    worldVoxels[index] = VoxelType.Void;
                } else {
                    pix[index] = grassColor;
                    worldVoxels[index] = VoxelType.Grass;
                }
                x++;
            }
            y++;
        }

        if(ySectPos < tex.height && ySectPos >= 0 && xSectPos < tex.width && xSectPos >= 0) {
            pix[ySectPos * tex.width + xSectPos] = sectColor;
            worldVoxels[ySectPos * tex.width + xSectPos] = VoxelType.Sect;
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
            CalcNoise();
        }
    }

    public static VoxelType GetVoxelTypeFromColor(Color color)
    {
        if(color == Color.white) {
            return VoxelType.Grass;
        } else if(color == Color.red) {
            return VoxelType.Sect;
        } else {
            return VoxelType.Void;
        }
    }
}
