﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PersistentData : MonoBehaviour
{

    private static PersistentData _instance;
    public static PersistentData Instance { get { return _instance; } }

    //References to things that need to be easily accessible
    [Header("PlayerReference")]
    public GameObject PlayerPrefab;
    public GameObject Player { get; private set; }
    private GameObject player;

    [Header("UIReference")]
    public GameObject UIPrefab;
    public GameObject UI { get; private set; }
    private GameObject ui;
    [SerializeField]
    private bool displayUI = true;

    [Header("PauseReference")]
    public GameObject PauseMenuPrefab;
    public GameObject PauseMenu { get; private set; }
    private GameObject pauseMenu;

    [Header("StatUIReference")]
    public GameObject StatUIPrefab;
    public GameObject StatUI { get; private set;}
    private GameObject statUI;

    [Header("ShopRelicUIReference")]
    public GameObject ShopRelicUIPrefab;
    public GameObject ShopRelicUI { get; private set;}
    private GameObject shopRelicUI;

    [Header("AudioReference")]
    public GameObject AudioControllerPrefab;
    public GameObject AudioController {get; private set;}
    private GameObject audioController;

    public GameObject SFXManagerPrefab;
    public GameObject SFXManager {get; private set;}
    private GameObject sfxManager;

    private AudioSettings audioSettings;

    [Header("LoadScreen")]
    public CanvasGroup loadScreen;

    public bool isGeneratorDone;

    public List<RelicStats> availableRelics;

    [Header("InputActionAsset")]
    public PlayerInput playerInputs;


    private void OnApplicationQuit()
    {
        //------------------------------------------------
        // saves PlayerPrefs for next application launch
        //------------------------------------------------
        audioSettings.SaveVolumesOnQuit();
        SaveControls();
        PlayerPrefs.Save();
    }

    private void Awake() 
    {
        if(_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } 
        else 
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            Init();
        }
    }
    

    private void Init() 
    {
        if(Player == null)
        {
            try
            {
                Player = GameObject.FindGameObjectWithTag("Player");
                if(Player == null)
                {
                    Player = Instantiate(PlayerPrefab, GetSpawnpoint(), Quaternion.identity);
                }
            }
            catch
            {
                Player = Instantiate(PlayerPrefab, GetSpawnpoint(), Quaternion.identity);
            }
            
        }
        Camera.main.GetComponent<CamFollow>().toFollow = Player.transform;

        playerInputs = Player.GetComponent<PlayerInput>();
        //LoadControls();


        if(ShopRelicUI == null)
        {
            try
            {
                ShopRelicUI = GameObject.FindGameObjectWithTag("ShopRelicUI");
                if(ShopRelicUI == null)
                {
                    ShopRelicUI = Instantiate(ShopRelicUIPrefab, Vector3.zero, Quaternion.identity);
                    ShopRelicUI.SetActive(false);
                }
                
            }
            catch
            {
                ShopRelicUI = Instantiate(ShopRelicUIPrefab, Vector3.zero, Quaternion.identity);
                ShopRelicUI.SetActive(false);
            }
            
        }



        if(UI == null)
        {
            try
            {
                UI = GameObject.FindGameObjectWithTag("UI");
                if(UI == null)
                {
                    UI = Instantiate(UIPrefab, Vector3.zero, Quaternion.identity);
                    UI.SetActive( displayUI );
                }
            }
            catch
            {
                UI = Instantiate(UIPrefab, Vector3.zero, Quaternion.identity);
                UI.SetActive( displayUI );
            }
            
        }


        if(PauseMenu == null)
        {
            try
            {
                PauseMenu = GameObject.FindGameObjectWithTag("PauseMenu");
                if(PauseMenu == null)
                {
                    PauseMenu = Instantiate(PauseMenuPrefab, Vector3.zero, Quaternion.identity);
                }
            }
            catch
            {
                PauseMenu = Instantiate(PauseMenuPrefab, Vector3.zero, Quaternion.identity);
            }
            
        }



        if(statUI == null)
        {
            try
            {
                StatUI = GameObject.FindGameObjectWithTag("StatUI");
                if(StatUI == null)
                {
                    StatUI = Instantiate(StatUIPrefab, Vector3.zero, Quaternion.identity);
                }
            }
            catch
            {
                StatUI = Instantiate(StatUIPrefab, Vector3.zero, Quaternion.identity);
            }
            
        }



        if (AudioController == null)
        {
            try
            {
                AudioController = GameObject.FindGameObjectWithTag("AudioController");
                if (AudioController == null)
                {
                    AudioController = Instantiate(AudioControllerPrefab, Vector3.zero, Quaternion.identity);
                }
            }
            catch
            {
                AudioController = Instantiate(AudioControllerPrefab, Vector3.zero, Quaternion.identity);
            }
        }
        MakeChild(AudioController);

        if (SFXManager == null)
        {
            try
            {
                SFXManager = GameObject.FindGameObjectWithTag("SFXManager");
                if (SFXManager == null)
                {
                    SFXManager = Instantiate(SFXManagerPrefab, Vector3.zero, Quaternion.identity);
                }
            }
            catch
            {
                SFXManager = Instantiate(SFXManagerPrefab, Vector3.zero, Quaternion.identity);
            }
        }
        MakeChild(SFXManager);

        var settings = PauseMenu.transform.Find("Settings");
        var backdrop = settings.Find("backdrop");
        audioSettings = backdrop.Find("Volume sliders").GetComponent<AudioSettings>();
        audioSettings.LoadVolumesOnStart();
    }


    public void LoadScene(int _scene)
    {
        StartCoroutine(Transition(_scene));
    }


    IEnumerator Transition(int _scene) 
    {
        PlayerController playerController = Player.GetComponent<PlayerController>();

        AsyncOperation loadNewScene;
        try 
        {
            loadNewScene = SceneManager.LoadSceneAsync(_scene);
        } 
        catch 
        {
            Debug.LogError("COULD NOT LOAD SCENE WITH NAME :" + _scene);
            yield break;
        }

        //probably want to make the game "pause" so you cant move or die
        
       
        //make child everything we want to keep
        MakeChild(Player);
        MakeChild(playerController.currCreature);
        MakeChild(playerController.swapCreature);
        MakeChild(UI);
        MakeChild(PauseMenu);
        MakeChild(StatUI);
        MakeChild(ShopRelicUI);
        //Loading Scene, can make transition stuff here
         //for example, some screen fading stuff : 
            //transition OUT
        
        yield return StartCoroutine(FadeInScreen(0.5f));
        // Debug.Log("fade in");
        
        while (!loadNewScene.isDone)
        {
            //update some slider to show = loadNewScene.progress
            yield return null;
        }

        
        //unmake all children
        UnmakeChild(Player);
        UnmakeChild(playerController.currCreature);
        UnmakeChild(playerController.swapCreature);
        UnmakeChild(UI);
        UnmakeChild(PauseMenu);
        UnmakeChild(StatUI);
        UnmakeChild(ShopRelicUI);

        //set players position in new scene
        //CALL BUILD LEVEL, WHICH SHOULD GENERATE EVERYTHING, INCLUDING A SPAWNPOINT;
        if(SceneManager.GetActiveScene().name == "VoronoiPCG")
        {
            isGeneratorDone = false;
            loadScreen.alpha = 0;
            GameObject levelGen = GameObject.FindGameObjectWithTag("LevelGenerator");
            levelGen.GetComponent<VoronoiPCG>().InitializeGenerator();
        }

        // Debug.Log(GetSpawnpoint());
        while(!isGeneratorDone)
        {
            yield return null;
        }
        
        playerController.warpPlayer(GetSpawnpoint());
        //update Game State for FMOD if necessary
        switch(_scene)
        {
            case 1:
                AudioController.GetComponent<AudioController>().BeginFarmMusic();
                break;
            case 2:
                AudioController.GetComponent<AudioController>().BeginOverworldMusic();
                break;
            case 3:
                AudioController.GetComponent<AudioController>().BeginOverworldMusic();
                break;
            default:
                break;
        }

        if( playerController.currCreature != null)
        {
            playerController.currCreature.transform.position = 
                    playerController.backFollowPoint.transform.position;    
               
            // playerController.currCreature.GetComponent<CreatureAIContext>().agent.ResetPath();
            // playerController.currCreature.GetComponent<CreatureAIContext>().agent.SetDestination(playerController.backFollowPoint.transform.position);
             playerController.currCreature.GetComponent<CreatureAIContext>().agent.Warp(playerController.backFollowPoint.transform.position);
        }
        
        /*
            //transition IN
            yield return StartCoroutine(FadeLoadingScreen(0, 1));
        */
        yield return StartCoroutine(FadeOutScreen(1));


    }

    private Vector3 GetSpawnpoint(){
        Vector3 spawnpoint;
        try
        {
            spawnpoint = GameObject.FindGameObjectWithTag("spawnpoint").transform.position;
        } 
        catch 
        {   
            Debug.LogError("NO SPAWNPOINT SET IN THIS SCENE");
            spawnpoint = Vector3.zero;
        }
        return spawnpoint;
    }

    private void MakeChild(GameObject _gameObject)
    {
        if(_gameObject != null)
        {
            _gameObject.transform.SetParent(gameObject.transform);//changed to set parent instead of .parent
        }
        
    }

    private void UnmakeChild(GameObject _gameObject)
    {
        if(_gameObject != null)
        {
           _gameObject.transform.SetParent(null);//changed to set parent instead of .parent
           SceneManager.MoveGameObjectToScene(_gameObject,SceneManager.GetActiveScene());
        }
        
    }

    IEnumerator FadeInScreen(float duration)
    {
        float startValue = loadScreen.alpha;
        float time = 0;

        while (time < duration)
        {
            loadScreen.alpha = Mathf.Lerp(startValue, 1, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        loadScreen.alpha = 1;
    }

    IEnumerator FadeOutScreen(float duration)
    {
        float startValue = loadScreen.alpha;
        float time = 0;

        while (time < duration)
        {
            loadScreen.alpha = Mathf.Lerp(startValue, 0, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        loadScreen.alpha = 0;
    }

    public void SaveControls()
    {
        string bindings = playerInputs.actions.ToJson();
        PlayerPrefs.SetString("Bindings", bindings);
    }

    public void LoadControls()
    {
        string bindings = PlayerPrefs.GetString("Bindings", string.Empty);

        if (string.IsNullOrEmpty(bindings))
        {
            return;
        }
    
        playerInputs.actions.LoadFromJson(bindings);
    }
}
