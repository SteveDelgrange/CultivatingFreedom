using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkData
{
    public static int size = 32;

    public CellType[,] cells;

    protected Vector2Int[] vertices;

    public ChunkData()
    {
        cells = new CellType[32, 32];
    }
}
