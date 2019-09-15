using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class SaveDisplay : Selectable
{
    [SerializeField] protected GameSave _displayedSave;
    public GameSave DisplayedSave { get => _displayedSave; set { _displayedSave = value; RefreshDisplay(); } }

    [SerializeField] protected TextMeshProUGUI _saveNameText;
    [SerializeField] protected TextMeshProUGUI _spiritStoneText;

    public System.Action<SaveDisplay> Selected;

    public void RefreshDisplay()
    {
        if(DisplayedSave != null) {
            _saveNameText.text = DisplayedSave.saveName;
            _spiritStoneText.text = DisplayedSave.spiritStones.ToString();
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        Selected?.Invoke(this);
    }
}
