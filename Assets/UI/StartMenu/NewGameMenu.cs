using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NewGameMenu : MonoBehaviour
{
    [SerializeField] protected TMP_InputField _nameInputField;

    public void CreateNewSave()
    {
        string inputedName = _nameInputField.text.Trim();
        if (!GameSave.IsNameValid(inputedName)) {
            Debug.Log("Name too long or too short : " + GameSave.nameMinimumLength.ToString() + " min, " + GameSave.nameMaximumLength.ToString() + " max");
            return;
        }
        
        GameSave save = SaveSystem.CreateNewSave(inputedName);
        if (save != null) {
            SaveSystem.PlayingSave = save;
            SceneLoader.LoadSectScene();
        }
    }
}
