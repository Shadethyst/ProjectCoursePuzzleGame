using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{

    public static StoryManager instance;
    public int storySceneNumber;

    [SerializeField] public GameObject[] storyScenes;

    // Start is called before the first frame update
    void Start()
    {
        storySceneNumber = 0;
    }

    public void PlayScene()
    {
        storyScenes[storySceneNumber].SetActive(true);
        storySceneNumber++;
    }
}
