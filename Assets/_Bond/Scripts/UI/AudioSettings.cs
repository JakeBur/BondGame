using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Bus = FMOD.Studio.Bus;
using Audio = FMODUnity.RuntimeManager;

public class AudioSettings : MonoBehaviour
{
    private Bus Master;
    private Bus Music;
    private Bus SFX;

    public GameObject masterSlider;
    public GameObject musicSlider;
    public GameObject sfxSlider;

    private Slider _masterSlider;
    private Slider _musicSlider;
    private Slider _sfxSlider;

    private float masterVolume = 1f;
    private float musicVolume = 1f;
    private float sfxVolume = 1f;

    private void Awake()
    {
        Master = Audio.GetBus("bus:/Master");
        Music = Audio.GetBus("bus:/Master/Music");
        SFX = Audio.GetBus("bus:/Master/SFX");

        if (_masterSlider == null)
        {
            _masterSlider = masterSlider.GetComponent<Slider>();
        }

        if (_musicSlider == null)
        {
            _musicSlider = musicSlider.GetComponent<Slider>();
        }

        if (_sfxSlider == null)
        {
            _sfxSlider = sfxSlider.GetComponent<Slider>();
        }

        _masterSlider.value = masterVolume;
        _musicSlider.value = musicVolume;
        _sfxSlider.value = sfxVolume;
    }

    private void Update()
    {
        Master.setVolume(masterVolume);
        Music.setVolume(musicVolume);
        SFX.setVolume(sfxVolume);
    }

    public void SetMasterVolume(float newVolume)
    {
        masterVolume = newVolume;
        Debug.Log("a");
    }

    public void SetMusicVolume(float newVolume)
    {
        musicVolume = newVolume;
        Debug.Log("b");
    }

    public void SetSFXVolume(float newVolume)
    {
        sfxVolume = newVolume;
        Debug.Log("c");
    }
}
