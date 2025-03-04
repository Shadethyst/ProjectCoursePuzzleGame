using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainMenu : MonoBehaviour
{

    [SerializeField] private GameObject mainMenuBase;
    [SerializeField] private GameObject levelSelectBase;
    [SerializeField] private GameObject settingsBase;

    [SerializeField] private Button[] mainMenuButtons;
    [SerializeField] private Button[] levelSelectButtons;
    [SerializeField] private Button[] settingsButtons;

    [SerializeField] private Image gameStartFadeScreen;

    private bool readyToChangeScenes;

    public void Start()
    {
        mainMenuBase.SetActive(true);
        mainMenuButtons[0].Select();
        settingsBase.SetActive(false);
    }

    public void Update()
    {
        if (readyToChangeScenes)
        {
            gameStartFadeScreen.CrossFadeAlpha(1.0f, 0.3f, false);
            StartCoroutine(DoSceneTransition());
        }
    }
    public void OnStartGamePressed()
    {
        gameStartFadeScreen.gameObject.SetActive(true);
        gameStartFadeScreen.canvasRenderer.SetAlpha(0.0f);
        readyToChangeScenes = true;
    }

    public void OpenLevelSelect()
    {
        levelSelectBase.SetActive(true);
        levelSelectButtons[0].Select();
        mainMenuBase.SetActive(false);
    }

    public void CloseLevelSelect()
    {
        mainMenuBase.SetActive(true);
        mainMenuButtons[2].Select();
        levelSelectBase.SetActive(false);
    }

    public void OpenLevel(int i)
    {
        SceneManager.LoadScene(i);
    }

    public void OpenSettings()
    {
        settingsBase.SetActive(true);
        settingsButtons[0].Select();
        mainMenuBase.SetActive(false);
    }

    public void CloseSettings()
    {
        mainMenuBase.SetActive(true);
        mainMenuButtons[3].Select();
        settingsBase.SetActive(false);
    }

    public void OnQuitPressed()
    {
        Application.Quit();
    }

    IEnumerator DoSceneTransition()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(1);
    }
}
