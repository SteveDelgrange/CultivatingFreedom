using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SectUI : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI _spiritStoneText;

    private void Start()
    {
        SaveSystem.PlayingSave.SpiritStonesValueChanged += OnSpritiStonesValueChanged;
        _spiritStoneText.text = SaveSystem.PlayingSave.SpiritStones.ToString();
    }

    protected void OnSpritiStonesValueChanged(int newValue)
    {
        _spiritStoneText.text = newValue.ToString();
    }
}
