using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuManager : MonoBehaviour
{
    protected static int displayedHash = Animator.StringToHash("Displayed");

    [SerializeField] protected Animator _menuAnimator;
    [SerializeField] protected Animator _startNewGameAnimator;
    [SerializeField] protected Animator _continueAnimator;
    [SerializeField] protected Animator _creditsAnimator;
    [SerializeField] protected Animator _settingsAnimator;

    [SerializeField] protected UnityEngine.UI.Button _menuContinueButton;
    [SerializeField] protected TMPro.TextMeshProUGUI _menuContinueText;
    [SerializeField] protected ContinueManager _continueManager;

    protected Animator _currentDisplayed;
    protected Animator CurrentDisplayed
    {
        get => _currentDisplayed;
        set {
            if(_currentDisplayed != null) {
                _currentDisplayed.SetBool(displayedHash, false);
            }
            _currentDisplayed = value;
            if (_currentDisplayed != null) {
                _currentDisplayed.SetBool(displayedHash, true);
            }
        }
    }

    protected List<GameSave> _saves;

    private void Awake()
    {
        OnMenu();
    }

    public void OnMenu()
    {
        CurrentDisplayed = _menuAnimator;

        _saves = SaveSystem.RetrieveSaves();
        _menuContinueButton.interactable = _saves.Count > 0;
        _menuContinueText.alpha = _saves.Count > 0 ? 1 : 0.3f;
    }

    public void OnStartNewGame()
    {
        CurrentDisplayed = _startNewGameAnimator;
    }

    public void OnContinue()
    {
        CurrentDisplayed = _continueAnimator;
        _continueManager.DisplaySaves();
    }

    public void OnCredits()
    {
        CurrentDisplayed = _creditsAnimator;
    }

    public void OnSettings()
    {
        CurrentDisplayed = _settingsAnimator;
    }

    public void OnQuit()
    {
        Application.Quit();
    }
}
