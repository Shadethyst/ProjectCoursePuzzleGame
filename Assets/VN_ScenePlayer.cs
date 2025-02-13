using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VisualNovelPlayer : MonoBehaviour
{
    [SerializeField] private VN_Page[] vnPages;
    private int pages;
    private bool start;
    private bool completed;
    int currentPage;

    private bool readyToChangePages;

    // Start is called before the first frame update
    void Start()
    {
        start = true;
        completed = false;
        currentPage = 0;
        pages = vnPages.Length;
        vnPages[currentPage].ActivatePage();

        readyToChangePages = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(SceneManager.GetActiveScene().name);
        if ((start && currentPage == 0) || vnPages[currentPage].GetAutomaticalTurn())
        {
            if (!readyToChangePages)
            {
                readyToChangePages = true;
                StartCoroutine(AutomaticPageTurn());
            }
        }
        if (PageTurningAttempted() && readyToChangePages && vnPages[currentPage].GetReadyForPageTurn() && currentPage < vnPages.Length - 1 && !vnPages[currentPage].GetAutomaticalTurn())
        {
            readyToChangePages = false;
            TurnPage();
        }
        else if (PageTurningAttempted() && readyToChangePages && currentPage > vnPages.Length - 1)
        {
            completed = true;
        }

        if (completed && SceneManager.GetActiveScene().name == "Story_Intro")
        {
            SceneManager.LoadScene(2);
        }
        else if (completed && SceneManager.GetActiveScene().name != "Story_Intro")
        {
            this.gameObject.SetActive(false);
        }
    }


    private void TurnPage()
    {
        readyToChangePages = false;
        currentPage++;
        vnPages[currentPage].ActivatePage();
        vnPages[currentPage].SetIsPageTurned(true);
        vnPages[currentPage-1].DeactivatePage();
        StartCoroutine(ResetPageTurnReadiness());
    }

    private bool PageTurningAttempted()
    {
        return Input.GetKeyDown(KeyCode.Space) || vnPages[currentPage].GetHasButtonBeenClicked();
    }

    IEnumerator AutomaticPageTurn()
    {
        yield return new WaitForSeconds(2);
        if (currentPage < vnPages.Length - 1)
        {
            TurnPage();
        }
        else
        {
            completed = true;
        }
    }

    IEnumerator ResetPageTurnReadiness()
    {
        yield return new WaitForSeconds(2.0f);
        vnPages[currentPage].SetReadyForPageTurn(true);
        readyToChangePages = true;
    }
}

