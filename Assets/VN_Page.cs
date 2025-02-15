using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VN_Page : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private int pageNumber;
    private CanvasGroup canvasElements; 
    private GameObject pageCanvas;
    private GameObject backgroundImage;
    private GameObject characterLeft;
    private GameObject characterMiddle;
    private GameObject characterRight;
    private GameObject textBox;
    private GameObject nextButton;
    [SerializeField] private bool automaticalTurn;
    private bool readyForPageTurn;
    private bool buttonHasBeenClicked;
    private bool fadingIn;
    [SerializeField] private bool hasTransition;
    private bool isPageTurned;

    void Awake()
    {
        this.gameObject.SetActive(true);
        readyForPageTurn = false;
        isPageTurned = false;
        pageCanvas = this.transform.GetChild(0).gameObject;
        backgroundImage = pageCanvas.transform.GetChild(0).gameObject;
        characterLeft = pageCanvas.transform.GetChild(1).gameObject;
        characterMiddle = pageCanvas.transform.GetChild(2).gameObject;
        characterRight = pageCanvas.transform.GetChild(3).gameObject;
        textBox = pageCanvas.transform.GetChild(4).gameObject;
        nextButton = pageCanvas.transform.GetChild(5).gameObject;
        canvasElements = this.transform.GetChild(0).GetComponent<CanvasGroup>();
        fadingIn = false;
    }

    private void Start()
    {
        nextButton.SetActive(false);
        if (pageNumber != 0)
        {
            canvasElements.gameObject.SetActive(false);
        }
        if (pageNumber == 0 && !hasTransition)
        {
            canvasElements.alpha = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (automaticalTurn && !readyForPageTurn)
        {
            nextButton.SetActive(false);
        }
        if (!automaticalTurn && readyForPageTurn)
        {
            nextButton.SetActive(true);
        }

        if (fadingIn)
        {
            if (hasTransition)
            {
                if (canvasElements.alpha < 1.0f)
                {
                    canvasElements.alpha += Time.deltaTime;
                }
            }
            if (!hasTransition)
            {
               canvasElements.alpha = 1.0f;
            }
            
            if (canvasElements.alpha == 1.0f)
            {
                fadingIn = false;
                isPageTurned = true;
            }
        }
    }

    public GameObject GetPageCanvas()
    {
        return this.pageCanvas;
    }

    public GameObject GetBackgroundImage()
    {
        return this.backgroundImage;
    }

    public GameObject GetCharacterLeft()
    {
        return this.characterLeft;
    }

    public GameObject GetCharacterMiddle()
    {
        return this.characterMiddle;
    }

    public GameObject GetCharacterRight()
    {
        return this.characterRight;
    }

    public GameObject GetTextBox()
    {
        return this.textBox;
    }

    public bool GetAutomaticalTurn()
    {
        return automaticalTurn;
    }

    public void SetReadyForPageTurn(bool ready)
    {
        readyForPageTurn = ready;
    }

    public bool GetReadyForPageTurn()
    {
        return this.readyForPageTurn;
    }

    public void SetIsPageTurned(bool ready)
    {
        isPageTurned = ready;
    }

    public bool GetIsPageTurned()
    {
        return this.isPageTurned;
    }


    public void SetButtonHasBeenClicked(bool answer)
    {
        this.buttonHasBeenClicked = answer;
    }

    public bool GetHasButtonBeenClicked()
    {
        return this.buttonHasBeenClicked;
    }

    public void ActivatePage()
    {
        canvasElements.gameObject.SetActive(true);
        canvasElements.alpha = 0;
        fadingIn = true;
    }
    public void DeactivatePage()
    {
        Debug.Log(this + " is deleted");
        this.gameObject.SetActive(false);
    }
}
