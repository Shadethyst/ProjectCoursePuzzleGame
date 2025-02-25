using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    public static PauseMenu Instance;

    [SerializeField] private GameObject pauseMenuBase;
    [SerializeField] private GameObject settingsBase;
    [SerializeField] private GameObject returnToMenuBase;

    // These buttons are used to make sure that they are focused automatically
    // when the correct menu page is activated (so that they can be navigated by keyboard)
    [SerializeField] private Button[] pauseMenuButtons;
    [SerializeField] private Button[] settingsButtons;
    [SerializeField] private Button[] returnToMenuButtons;
    
    [SerializeField] private GameObject itemCanvas;

    private bool isPaused = false;

    private GameState latestGameState;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenuBase.SetActive(false);
        settingsBase.SetActive(false);
        returnToMenuBase.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.Instance.state != GameState.Story)
        {
            isPaused = !isPaused;
            pauseMenuBase.SetActive(isPaused);
            pauseMenuButtons[0].Select();
            returnToMenuBase.SetActive(false);
            settingsBase.SetActive(false);

            if (isPaused)
            {
                ActivatePause();
            }
            if (!isPaused)
            {
                Resume();
            }
        }
    }

    public void Resume()
    {
        isPaused = false;
        pauseMenuBase.SetActive(false);
        GameManager.Instance.UpdateGameState(latestGameState);
        if (itemCanvas)
        {
            itemCanvas.SetActive(true);
        }
    }

    public void ActivatePause()
    {
        latestGameState = GameManager.Instance.state;
        GameManager.Instance.UpdateGameState(GameState.Pause);
        if (itemCanvas)
        {
            itemCanvas.SetActive(false);
        }
    }

    public void OpenSettings()
    {
        settingsBase.SetActive(true);
        settingsButtons[0].Select();
        pauseMenuBase.SetActive(false);
        returnToMenuBase.SetActive(false);
    }

    public void CloseSettings()
    {
        pauseMenuBase.SetActive(true);
        pauseMenuButtons[3].Select();
        settingsBase.SetActive(false);
    }

    public void ReturnToMainMenu()
    {
        returnToMenuBase.SetActive(true);
        returnToMenuButtons[0].Select();
        pauseMenuBase.SetActive(false);
    }

    public void NoReturnToMenu()
    {
        pauseMenuBase.SetActive(true);
        pauseMenuButtons[4].Select();
        returnToMenuBase.SetActive(false);
    }

    public void YesReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public bool GetIsPaused()
    {
        return this.isPaused;
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
