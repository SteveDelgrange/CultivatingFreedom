using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldBehaviour : MonoBehaviour
{
    [SerializeField] protected WorldData _data;
    [SerializeField] protected GameObject _chunkPrefab;

    protected Vector3 _halfChunkSize;

    protected bool _initialized = false;

    public void Initialize()
    {
        if (_initialized) {
            return;
        }
        GenerateWorld();
    }

    protected void GenerateWorld()
    {
        if(_data == null) {
            Debug.Log("Data is null, can't generate world");
            return;
        }

        _halfChunkSize = new Vector3(ChunkData.size / 2, 0, ChunkData.size / 2);

        ChunkData chunk = _data.GenerateChunkData(Vector2Int.zero);
        GenerateChunkDisplay(chunk, Vector3Int.zero);
    }

    protected MeshFilter GenerateChunkDisplay(ChunkData data, Vector3 position)
    {
        MeshFilter meshFilter = Instantiate(_chunkPrefab, transform).GetComponent<MeshFilter>();
        meshFilter.transform.position = position - _halfChunkSize;
        meshFilter.mesh = GenerateChunkMesh(data);
        return meshFilter;
    }

    protected Mesh GenerateChunkMesh(ChunkData data)
    {
        int verticesNumber = ChunkData.size * ChunkData.size * 4;
        int trianglesNumber = ChunkData.size * ChunkData.size * 6;

        Vector3[] vertices = new Vector3[verticesNumber];
        int[] triangles = new int[trianglesNumber];
        Vector3[] normals = Enumerable.Repeat(Vector3.up, verticesNumber).ToArray();
        Mesh m = new Mesh();

        int vi = 0;
        int ti = 0;

        for (int i = 0; i < ChunkData.size; i++) {
            for (int j = 0; j < ChunkData.size; j++) {
                vertices[vi] =      new Vector3Int(j, 0, i);
                vertices[vi+1] =    new Vector3Int(j, 0, i+1);
                vertices[vi+2] =    new Vector3Int(j+1, 0, i+1);
                vertices[vi+3] =    new Vector3Int(j+1, 0, i);

                triangles[ti] = vi;
                triangles[ti+1] = vi+1;
                triangles[ti+2] = vi+2;

                triangles[ti+3] = vi;
                triangles[ti+4] = vi+2;
                triangles[ti+5] = vi+3;

                vi += 4;
                ti += 6;
            }
        }

        m.vertices = vertices;
        m.triangles = triangles;
        m.normals = normals;
        return m;
    }
}
