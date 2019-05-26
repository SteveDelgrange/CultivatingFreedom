using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldBehaviour : MonoBehaviour
{
    [SerializeField] protected WorldData _data;
    [SerializeField] protected GameObject _voxelPrefab;
    [SerializeField] protected GameObject _sectPrefab;

    void Start()
    {
        GenerateWorld();
    }

    protected void GenerateWorld()
    {
        if(_data == null || _data.worldTexture == null) {
            Debug.Log("Data is null, can't generate world");
            return;
        }

        VoxelType[] voxels = _data.worldVoxels;
        int width = _data.pixWidth;
        int height = _data.pixHeight;
        int halfWidth = width / 2;
        int halfHeight = height / 2;

        for (int i = 0; i < height; i++) {
            for (int j = 0; j < width; j++) {
                CreateVoxel(voxels[i * width + j], j - halfWidth, i - halfHeight);
            }
        }
    }

    protected void CreateVoxel(VoxelType type, int x, int y)
    {
        if(type == VoxelType.Void) {
            return;
        }

        GameObject voxel = Instantiate(_voxelPrefab, transform);
        voxel.transform.localPosition = new Vector3(x, -0.5f, y);

        if(type == VoxelType.Sect) {
            GameObject sect = Instantiate(_sectPrefab, transform);
            sect.transform.localPosition = new Vector3(x, 0, y);
        }
    }
}
