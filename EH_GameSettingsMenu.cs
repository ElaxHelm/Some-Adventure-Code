using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EH_GameSettingsMenu : MonoBehaviour {

    public AudioMixer sfxMixer;
    public AudioMixer gameMixer;

    void Update()
    {
        if(sfxMixer == null)
        {
            sfxMixer = FindObjectOfType<AudioMixer>();
        }
    }
    public void SetGameVolume(float gameVolume)
    {
        gameMixer.SetFloat("gamevolume", gameVolume);
    }

    public void SetSFXVolume(float sfxVolume)
    {
        sfxMixer.SetFloat("sfxvolume", sfxVolume);
    }
}
