using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{

    // This fade effect is only used
    // at the beginning and at the end of the scene
    [SerializeField] private Canvas fadeCanvas;
    [SerializeField] private Image fadeImage;

    private bool readyToBeginScene;
    private bool readyToEndScene;

    private bool endComplete = false;

    // Start is called before the first frame update
    void Start()
    {
        fadeImage.enabled = true;
        readyToBeginScene = true;
        readyToEndScene = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (readyToBeginScene)
        {
            fadeImage.CrossFadeAlpha(0.0f, 2.0f, false);
            readyToBeginScene = false;
        }

        if (readyToEndScene)
        {
            fadeImage.CrossFadeAlpha(1.0f, 0.4f, false);
            StartCoroutine(ChangeScene());
        }
    }

    public void SetReadyToBeginScene(bool answer)
    {
        this.readyToBeginScene = answer;
    }

    public void SetReadyToEndScene(bool answer)
    {
        this.readyToEndScene = answer;
    }

    public bool GetReadyToBeginScene()
    {
        return this.readyToBeginScene;
    }

    public bool GetReadyToEndScene()
    {
        return this.readyToEndScene;
    }

    public IEnumerator ChangeScene()
    {
        Debug.Log("Changing Scene");
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
