using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class SceneLoader
{
    public static void LoadMainMenuScene()
    {
        SaveSystem.Save();
        SaveSystem.PlayingSave = null;
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public static bool LoadSectScene()
    {
        if (SaveSystem.PlayingSave == null) {
            return false;
        }

        SaveSystem.Save();

        SceneManager.LoadScene(1, LoadSceneMode.Single);

        return true;
    }

    public static void LoadWorldScene()
    {
        SaveSystem.Save();
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }
}
