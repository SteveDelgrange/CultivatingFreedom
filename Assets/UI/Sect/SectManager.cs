using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectManager : MonoBehaviour
{
    public void IncreaseSpiritStones(int increase)
    {
        SaveSystem.PlayingSave.SpiritStones += increase;
    }

    public void GoToWorld()
    {
        SceneLoader.LoadWorldScene();
    }
}
