using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    protected static SaveSystem _instance;

    protected static GameSave _playingSave;
    public static GameSave PlayingSave
    {
        get => _playingSave;
        set => _playingSave = value;
    }

    private void Awake()
    {
        if(_instance != null) {
            Destroy(gameObject);
        } else {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnDestroy()
    {
        if(_instance == this) {
            _instance = null;
        }
    }

    protected void OnApplicationQuit()
    {
        Save();
    }

    public static List<GameSave> RetrieveSaves()
    {
        List<GameSave> saves = new List<GameSave>();

        string saveDirPath = Directory.GetCurrentDirectory() + "\\Saves";
        bool saveDirExists = Directory.Exists(saveDirPath);

        if (!saveDirExists) {
            Directory.CreateDirectory(saveDirPath);
        } else {
            foreach (string file in Directory.EnumerateFiles(saveDirPath)) {
                StreamReader sr = File.OpenText(file);
                GameSave save = JsonUtility.FromJson<GameSave>(sr.ReadToEnd());
                if (save != null && save.IsNameValid()) {
                    saves.Add(save);
                }
                sr.Close();
            }
        }

        return saves;
    }

    public static GameSave CreateNewSave(string newSaveName)
    {
        if (!GameSave.IsNameValid(newSaveName)) {
            Debug.Log("Can't create save, name is not valid : " + newSaveName);
            return null;
        }

        GameSave newSave = null;

        string saveDirPath = Directory.GetCurrentDirectory() + "\\Saves";
        bool saveDirExists = Directory.Exists(saveDirPath);

        if (!saveDirExists) {
            Directory.CreateDirectory(saveDirPath);
        }

        bool foundSameName = false;
        int order = 0;

        foreach (string file in Directory.EnumerateFiles(saveDirPath)) {
            StreamReader sr = File.OpenText(file);
            GameSave save = JsonUtility.FromJson<GameSave>(sr.ReadToEnd());
            if (save != null) {
                order++;
                if (save.saveName == newSaveName) {
                    foundSameName = true;
                }
            }
            sr.Close();
        }

        if (!foundSameName) {
            newSave = new GameSave {
                saveName = newSaveName,
                order = order,
                dateOfCreation = System.DateTime.Now.ToLongDateString()
            };
            string jsonSave = JsonUtility.ToJson(newSave, false);
            StreamWriter sw = File.CreateText(saveDirPath + "\\" + newSave.saveName + ".save");
            sw.Write(jsonSave);
            sw.Close();
        } else {
            Debug.Log("Found file of the same name : " + newSaveName);
        }

        return newSave;
    }

    public static bool Save()
    {
        return Save(PlayingSave);
    }

    public static bool Save(GameSave save)
    {
        if (save == null || !GameSave.IsNameValid(save.saveName)) {
            Debug.Log("Can't create save, name is not valid : " + save.saveName);
            return false;
        }

        string saveDirPath = Directory.GetCurrentDirectory() + "\\Saves";
        bool saveDirExists = Directory.Exists(saveDirPath);

        if (!saveDirExists) {
            Directory.CreateDirectory(saveDirPath);
        }

        string jsonSave = JsonUtility.ToJson(save, false);
        StreamWriter sw = File.CreateText(saveDirPath + "\\" + save.saveName + ".save");
        sw.Write(jsonSave);
        sw.Close();

        return false;
    }

    public static bool DeleteSave(GameSave save)
    {
        if (save == null) {
            Debug.Log("Can't delete a null save");
            return false;
        }

        string saveDirPath = Directory.GetCurrentDirectory() + "\\Saves";
        bool saveDirExists = Directory.Exists(saveDirPath);

        if (!saveDirExists) {
            Debug.Log("No directory");
            return false;
        }
        
        foreach (string file in Directory.EnumerateFiles(saveDirPath)) {
            StreamReader sr = File.OpenText(file);
            GameSave s = JsonUtility.FromJson<GameSave>(sr.ReadToEnd());
            sr.Close();
            if (s != null && s.saveName == save.saveName) {
                File.Delete(file);
                return true;
            }
        }

        return false;
    }
}
