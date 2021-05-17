//Herman
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

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

    private enum MenuState
    {
        MENU, SETTINGS, CREDITS
    }

    private MenuState currentState = MenuState.MENU;

    // Since PersistentData does not exist in Main Menu
    private void Start()
    {
        SFX = SFXObject.GetComponent<SFXManager>();
        audioController = audioControllerObject.GetComponent<AudioController>();

        creditsObject.SetActive(false);
        settingsObject.SetActive(false);
    }

    private void OnPause()
    {
        switch( currentState )
        {
            case MenuState.MENU:
                break;
            case MenuState.CREDITS:
                CloseCredits();
                break;
            case MenuState.SETTINGS:
                CloseSettings();
                break;
            default:
                Debug.LogError("Not in a valid menu state");
                break;
        }
    }

    public void Play()
    {
        PlayClickSFX();
        audioController.BeginOverworldMusic();
        
        LoadSceneNoPersist("Tutorial");
    }

    public void OpenSettings()
    {
        PlayClickSFX();

        menuButtonsObject.SetActive(false);
        settingsObject.SetActive(true);

        currentState = MenuState.SETTINGS;
    }

    public void CloseSettings()
    {
        PlayClickSFX();

        menuButtonsObject.SetActive(true);
        settingsObject.SetActive(false);

        currentState = MenuState.MENU;
    }

    public void OpenCredits()
    {
        PlayClickSFX();

        menuButtonsObject.SetActive(false);
        creditsObject.SetActive(true);

        currentState = MenuState.CREDITS;
    }

    public void CloseCredits()
    {
        PlayClickSFX();

        menuButtonsObject.SetActive(true);
        creditsObject.SetActive(false);

        currentState = MenuState.MENU;
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