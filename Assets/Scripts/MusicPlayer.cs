using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip storyMusic;
    [SerializeField] private AudioClip gameplayMusic;

    private AudioSource audioSource;
    private AudioClip activeClip;
    private int sceneNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        sceneNumber = SceneManager.GetActiveScene().buildIndex;

        if (sceneNumber < 2)
        {
            if (audioSource.clip == gameplayMusic)
            {
                FadeOutMusic();
            }

            ChangeActiveClip(storyMusic);
            
        }
        else if (sceneNumber > 1)
        {
            if (audioSource.clip == storyMusic)
            {
                FadeOutMusic();
            }

            ChangeActiveClip(gameplayMusic);
        }
    }

    public void FadeOutMusic()
    {
        audioSource.volume -= 0.003f;
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
