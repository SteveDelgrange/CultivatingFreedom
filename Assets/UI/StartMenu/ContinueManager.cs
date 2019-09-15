using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueManager : MonoBehaviour
{
    [SerializeField] protected SaveDisplay _saveDisplayPrefab;
    [SerializeField] protected RectTransform _contentParent;
    [SerializeField] protected float _doubleClickTiming = 0.2f;

    protected SaveDisplay _selectedSave;
    protected SaveDisplay SelectedSave
    {
        get => _selectedSave;
        set {
            _selectedSave = value;
        }
    }
    protected float _selectedTime = Mathf.NegativeInfinity;

    protected List<SaveDisplay> _allDisplays;

    public void DisplaySaves()
    {
        if(_allDisplays != null) {
            foreach(SaveDisplay sd in _allDisplays) {
                Destroy(sd.gameObject);
            }
            _allDisplays.Clear();
        } else {
            _allDisplays = new List<SaveDisplay>();
        }
        List<GameSave> saves = SaveSystem.RetrieveSaves();
        saves.Sort(new GameSaveComparer());
        foreach(GameSave save in saves) {
            SaveDisplay sd = Instantiate(_saveDisplayPrefab, _contentParent);
            sd.DisplayedSave = save;
            sd.Selected += OnSelectedSaveDisplay;
            _allDisplays.Add(sd);
        }
    }

    protected void OnSelectedSaveDisplay(SaveDisplay selected)
    {
        if(SelectedSave != null && SelectedSave == selected) {
            if (_selectedTime + _doubleClickTiming > Time.time) {
                PlaySelected();
            }
        } else {
            SelectedSave = selected;
        }
        _selectedTime = Time.time;
    }

    public void PlaySelected()
    {
        if (SelectedSave != null) {
            SaveSystem.PlayingSave = SelectedSave.DisplayedSave;
            if (!SceneLoader.LoadSectScene()) {
                Debug.Log("Can't load save");
            }
        }
    }

    public void DeleteSelected()
    {
        if (SelectedSave != null) {
            if (SaveSystem.DeleteSave(SelectedSave.DisplayedSave)) {
                Destroy(SelectedSave.gameObject);
                _allDisplays.Remove(SelectedSave);
            } else {
                Debug.Log("Can't destroy save");
            }
        }
    }

    protected class GameSaveComparer : IComparer<GameSave>
    {
        public int Compare(GameSave x, GameSave y)
        {
            if(x.order < y.order) {
                return -1;
            } else if (x.order > y.order) {
                return 1;
            } else {
                return 0;
            }
        }
    }
}
