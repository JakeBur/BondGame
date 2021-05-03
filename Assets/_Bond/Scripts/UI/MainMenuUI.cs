//Herman
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

using SFXPlayer = FMODUnity.RuntimeManager;

public class MainMenuUI : MonoBehaviour
{
    //-----------
    // for FMOD
    //-----------
    public GameObject SFXObject;
    public GameObject audioControllerObject;
    public GameObject menuButtonsObject;
    public GameObject creditsObject;
    public GameObject settingsObject;

    private SFXManager SFX;
    private AudioController audioController;

    // Since PersistentData does not exist in Main Menu
    private void Start()
    {
        /*
        if (SFX == null)
        {
            try
            {
                SFX = PersistentData.Instance.SFXManager.GetComponent<SFXManager>();
            }
            catch
            {
                SFX = GameObject.FindGameObjectWithTag("SFXManager").GetComponent<SFXManager>();
            }
        }
        */
        SFX = SFXObject.GetComponent<SFXManager>();
        audioController = audioControllerObject.GetComponent<AudioController>();

        creditsObject.SetActive(false);
        settingsObject.SetActive(false);
    }

    public void Play()
    {
        PlayClickSFX();

        LoadSceneNoPersist("Tutorial");

        audioController.BeginOverworldMusic();
    }

    public void Settings()
    {
        PlayClickSFX();

        menuButtonsObject.SetActive(false);
        settingsObject.SetActive(true);
    }

    public void Credits()
    {
        PlayClickSFX();

        menuButtonsObject.SetActive(false);
        creditsObject.SetActive(true);
    }

    public void Exit()
    {
        PlayClickSFX();

        Application.Quit();
    }

    private void PlayClickSFX()
    {
        SFXPlayer.PlayOneShot(SFX.ButtonClickSFX, transform.position);
    }

    private void TransitionScene( string name )
    {
        SceneManager.LoadScene(name);
    }

    private void LoadSceneNoPersist( string name )
    {
        SceneManager.LoadScene(name);
    }
}