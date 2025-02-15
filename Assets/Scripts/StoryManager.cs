using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{

    public static StoryManager instance;

    [SerializeField] public GameObject[] storyScenes;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {

    }

    public void PlayScene(int storySceneNumber)
    {
        storyScenes[storySceneNumber].SetActive(true);
    }
}
