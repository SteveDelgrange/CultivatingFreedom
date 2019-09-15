using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    protected bool _paused = false;
    [SerializeField] protected GameObject _ui;

    void Start()
    {
        Time.timeScale = _paused ? 0 : 1;
        _ui.SetActive(_paused);
    }

    private void OnDestroy()
    {
        Time.timeScale = 1;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        _paused = !_paused;
        Time.timeScale = _paused ? 0 : 1;
        _ui.SetActive(_paused);
    }

    public void ToSettings()
    {

    }

    public void ToMainMenu()
    {
        SceneLoader.LoadMainMenuScene();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
