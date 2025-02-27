using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectPlayer : MonoBehaviour
{

    public AudioSource soundEffect;
    private float volume;
    private bool isWater;
    private bool isFire;
    private bool isPlayer;

    private bool readyToPlay;

    private int playTimes;
    // Start is called before the first frame update
    void Awake()
    {
        soundEffect = GetComponent<AudioSource>();
        volume = soundEffect.volume;
        isWater = GetComponent<Water>();
        isFire = GetComponent<Fire>();
        isPlayer = GetComponent<PlayerController>();
        readyToPlay = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySoundEffect()
    {

        readyToPlay = false;

        if (GameManager.Instance.state == GameState.Story)
        {
            soundEffect.mute = true;
        }
        else 
        {
            if (isFire)
            {
                soundEffect.loop = true;
                readyToPlay = true;
            }
            else if (isWater && playTimes < 1)
            {
                soundEffect.loop = false;
                readyToPlay = true; 
            }
            else if (isPlayer && (playTimes % 2) == 0)
            {
                soundEffect.loop = false;
                readyToPlay = true;
            }
            else if (!isFire && !isWater && !isPlayer && playTimes >= 0)
            {
                readyToPlay = true;
            }

            if (readyToPlay)
            {
                soundEffect.mute = false;
                soundEffect.volume = volume;
                soundEffect.Play();
            }
            playTimes++;
        }
    }
}
