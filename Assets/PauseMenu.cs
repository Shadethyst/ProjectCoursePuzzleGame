using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public static PauseMenu Instance;

    [SerializeField] private GameObject pauseMenuBase;
    [SerializeField] private GameObject exitToMenuConfirm;
    [SerializeField] private GameObject itemCanvas;

    private bool isPaused = false;

    private GameState latestGameState;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenuBase.SetActive(false);
        exitToMenuConfirm.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameManager.Instance.state != GameState.Story)
        {
            isPaused = !isPaused;
            pauseMenuBase.SetActive(isPaused);
            exitToMenuConfirm.SetActive(false);

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

    public void ReturnToMainMenu()
    {
        exitToMenuConfirm.SetActive(true);
        pauseMenuBase.SetActive(false);
    }

    public void NoReturnToMenu()
    {
        pauseMenuBase.SetActive(true);
        exitToMenuConfirm.SetActive(false);
    }

    public void YesReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public bool GetIsPaused()
    {
        return this.isPaused;
    }
}
