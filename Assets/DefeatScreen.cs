using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefeatScreen : MonoBehaviour
{

    [SerializeField] private Canvas defeatCanvas;
    [SerializeField] private Image defeatImage;

    private bool readyToFadeIn;
    private bool readyToFadeOut;

    // Start is called before the first frame update
    void Start()
    {
        defeatImage.enabled = false;
        readyToFadeIn = false;
        readyToFadeOut = false;

        defeatCanvas.GetComponent<GraphicRaycaster>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
       if (readyToFadeIn)
       {
            defeatImage.enabled = true;
            if (defeatImage.enabled)
            {
                defeatImage.CrossFadeAlpha(1.0f, 2.0f, false);
                readyToFadeIn = false;
            }
       }
       /*if (readyToFadeOut)
       {
            if (readyToFadeIn == false)
            {
                Debug.Log("Fading out...");
                defeatImage.CrossFadeAlpha(0.0f, 2.0f, true);
                //readyToFadeOut = false;
            }
       }*/
    }


    public void SetReadyToFadeIn(bool answer)
    {
        this.readyToFadeIn = answer;
    }

    public void SetReadyToFadeOut(bool answer)
    {
        this.readyToFadeOut = answer;
    }
}
