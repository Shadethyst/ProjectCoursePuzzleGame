using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Canvas menuCanvas;
    [SerializeField] private Canvas settingsCanvas;

    public void Start()
    {
        menuCanvas.enabled = true;
        settingsCanvas.enabled = false;
    }

    public void OnStartGamePressed()
    {
        SceneManager.LoadScene(1);
    }

    public void OnSettingsPressed()
    {
        menuCanvas.enabled = false;
        settingsCanvas.enabled = true;
    }
    public void OnBackPressed()
    {
        settingsCanvas.enabled = false;
        menuCanvas.enabled = true;
    }

    public void OnQuitPressed()
    {
        Application.Quit();
    }
}
