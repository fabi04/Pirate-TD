using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SettingsUIManager : MonoBehaviour
{

    public AudioMixer masterAudioMixer;
    public Sprite volumeOn;
    public Sprite volumeOff;
    public Button musicButton;
    public Slider musicSlider;
    public Button sfxButton;
    public Slider sfxSlider;

    public void SetMusicVolume(float volume)
    {
        masterAudioMixer.SetFloat("musicVolume", volume);
        if(volume <= -80)
        {
            musicButton.image.sprite = volumeOff;
        }
        else
        {
            musicButton.image.sprite = volumeOn;
        }

    }

    public void SetSoundEffectsVolume(float volume)
    {
        masterAudioMixer.SetFloat("soundEffectsVolume", volume);
        if (volume <= -80)
        {
            sfxButton.image.sprite = volumeOff;
        }
        else
        {
            sfxButton.image.sprite = volumeOn;
        }
    }

    public void ToggleMusicVolume()
    {
        float musicVolume;
        masterAudioMixer.GetFloat("musicVolume", out musicVolume);
        if(musicVolume != -80)
        {
            SetMusicVolume(-80);
            musicSlider.value = -80;
        }
        else
        {
            SetMusicVolume(0);
            musicSlider.value = 0;
        }
    }

    public void ToggleSFXVolume()
    {
        float sfxVolume;
        masterAudioMixer.GetFloat("soundEffectsVolume", out sfxVolume);
        if (sfxVolume != -80)
        {
            SetSoundEffectsVolume(-80);
            sfxSlider.value = -80;
        }
        else
        {
            SetSoundEffectsVolume(0);
            sfxSlider.value = 0;
        }
    }
}
