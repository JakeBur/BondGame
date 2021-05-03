//Herman
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

using SFXPlayer = FMODUnity.RuntimeManager;

public class PauseManager : MonoBehaviour
{
    protected SFXManager SFX
    {
        get => PersistentData.Instance.SFXManager.GetComponent<SFXManager>();
    }

    public GameObject SettingsMenu;
    private Canvas PauseMenu => GetComponent<Canvas>();
    private PlayerController player => PersistentData.Instance.Player.GetComponent<PlayerController>();

    private enum PauseState
    {
        GAMEPLAY, PAUSE, SETTINGS
    }

    private PauseState currentState = PauseState.GAMEPLAY;

    private void Start()
    {
        SettingsMenu.SetActive(false);
        PauseMenu.enabled = false;
    }

    public void ProcessKeyPress()
    {
        switch( currentState )
        {
            case PauseState.GAMEPLAY:
                OpenPauseMenu();
                break;
            case PauseState.PAUSE:
                ClosePauseMenu();
                break;
            case PauseState.SETTINGS:
                CloseSettings();
                break;
            default:
                Debug.LogError("Not in a valid pause menu state");
                break;
        }
    }

    public void OpenPauseMenu()
    {
        SFXPlayer.PlayOneShot(SFX.MenuOpenSFX, transform.position);

        PauseMenu.enabled = true;
        currentState = PauseState.PAUSE;

        Time.timeScale = 0f;

        player.Pause();
    }

    public void ClosePauseMenu()
    {
        SFXPlayer.PlayOneShot(SFX.MenuOpenSFX, transform.position);

        PauseMenu.enabled = false;
        currentState = PauseState.GAMEPLAY;

        Time.timeScale = 1;

        player.Unpause();
    }

    public void OpenSettings()
    {
        PlayClickSFX();

        SettingsMenu.SetActive(true);

        currentState = PauseState.SETTINGS;
    }

    public void CloseSettings()
    {
        PlayClickSFX();

        SettingsMenu.SetActive(false);

        currentState = PauseState.PAUSE;
    }

    public void RetireRun()
    {
        PlayClickSFX();
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
}