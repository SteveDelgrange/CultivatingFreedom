
[System.Serializable]
public class GameSave
{
    public static int nameMinimumLength = 3;
    public static int nameMaximumLength = 16;

    public int order = -1;
    public string saveName = "New Game";
    public string dateOfCreation;
    public float timePlayed = 0;

    public int spiritStones = 0;
    public int SpiritStones
    {
        get => spiritStones;
        set {
            spiritStones = value;
            SpiritStonesValueChanged?.Invoke(spiritStones);
        }
    }

    public System.Action<int> SpiritStonesValueChanged;

    public bool IsNameValid()
    {
        return saveName.Length >= nameMinimumLength && saveName.Length <= nameMaximumLength;
    }
    public static bool IsNameValid(string name)
    {
        return name.Length >= nameMinimumLength && name.Length <= nameMaximumLength;
    }
}
