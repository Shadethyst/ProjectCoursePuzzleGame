using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeAdjust : MonoBehaviour
{

    public AudioMixer musicMixer;
    public AudioMixer soundMixer;

    public Slider musicSlider;
    public Slider soundSlider;


    private void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 1.0f);
        soundSlider.value = PlayerPrefs.GetFloat("SoundVolume", 1.0f);
    }

    public void SetMusicVolume(float sliderValue)
    {
        musicMixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MusicVolume", sliderValue);
    }

    public void SetSoundVolume(float sliderValue)
    {
        soundMixer.SetFloat("SoundVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("SoundVolume", sliderValue);
    }

}
