using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectPlayer : MonoBehaviour
{

    public AudioSource soundEffect;

    // Start is called before the first frame update
    void Awake()
    {
        soundEffect = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaySoundEffect()
    {
        if (GameManager.Instance.state == GameState.Story)
        {
            soundEffect.mute = true;
        }
        else
        {
            soundEffect.mute = false;
            soundEffect.volume = 1.0f;
            soundEffect.Play();
        }
    }
}
