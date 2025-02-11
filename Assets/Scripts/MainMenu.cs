using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] listOfMenus;
    [SerializeField] private Button[] firstButtonsOfMenus;
    [SerializeField] private int menuId = 0;
    [SerializeField] private Image gameStartFadeScreen;

    private bool readyToChangeScenes;

    public void Start()
    {
        readyToChangeScenes = false;
        SelectedMenu(menuId);
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

    public void OnQuitPressed()
    {
        Application.Quit();
    }

    private void SelectedMenu(int menuId)
    {
        foreach (GameObject menu in listOfMenus)
        {
            menu.SetActive(false);
        }
        listOfMenus[menuId].SetActive(true);
        firstButtonsOfMenus[menuId].Select();
    }

    public void ActivateMenu(Button button)
    {
        if (button.name == "Settings")
        {
            menuId = 1;
            StartCoroutine(PrepareForMenuChange());
        }
        else if (button.name == "Back")
        {
            menuId = 0;
            StartCoroutine(PrepareForMenuChange());
        }
    }

    IEnumerator PrepareForMenuChange()
    {
        yield return new WaitForSeconds(.2f);
        SelectedMenu(menuId);
    }

    IEnumerator DoSceneTransition()
    {
        yield return new WaitForSeconds(1.0f);
        SceneManager.LoadScene(1);
    }
}
