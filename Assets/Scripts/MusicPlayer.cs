using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer instance = null;

    [SerializeField] private AudioClip storyMusic;
    [SerializeField] private AudioClip gameplayMusic;

    private AudioSource audioSource;
    private AudioClip activeClip;
    private int sceneNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(base.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        sceneNumber = SceneManager.GetActiveScene().buildIndex;

        if (sceneNumber < 2 || sceneNumber > 4)
        {
            if (audioSource.clip == gameplayMusic)
            {
                FadeOutMusic(0.8f);
            }

            ChangeActiveClip(storyMusic);
            
        }
        else if (sceneNumber > 1 && sceneNumber < 5)
        {
            if (audioSource.clip == storyMusic)
            {
                FadeOutMusic(0.4f);
            }

            ChangeActiveClip(gameplayMusic);
        }
    }

    public void FadeOutMusic(float amountPerSecond)
    {
        audioSource.volume -= amountPerSecond * Time.deltaTime;
    }

    public void ChangeActiveClip(AudioClip targetMusic)
    {
        if (audioSource.volume <= 0.0f || audioSource.clip == null)
        {
            activeClip = targetMusic;
            audioSource.clip = targetMusic;
            audioSource.volume = 1.0f;
            CheckPlayability();
        }
    }
    public void CheckPlayability()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

}
