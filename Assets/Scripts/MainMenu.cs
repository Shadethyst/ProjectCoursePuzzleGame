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

    public void Start()
    {
        SelectedMenu(menuId);
    }

    public void OnStartGamePressed()
    {
        SceneManager.LoadScene(1);
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
}
