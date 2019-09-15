using UnityEngine;

public enum CellType
{
    Void    = 0,
    Water   = 1,
    Earth   = 2,
    Rock    = 3,
    Grass   = 4
}

public static class CellTypeExtension {
    public static Color GetColor(this CellType cell){
        switch (cell){
            case CellType.Void : return Color.black;
            case CellType.Water : return Color.blue;
            case CellType.Earth : return Color.yellow;
            case CellType.Rock : return Color.gray;
            case CellType.Grass : return Color.green;
        }
        return Color.black;
    }
}
