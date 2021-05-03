﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

//-----------
// for FMOD
//-----------
using SFXPlayer = FMODUnity.RuntimeManager;

public class EncounterManager : MonoBehaviour
{
    public List<Wave> waves = new List<Wave>();
    public GameObject barrier;
	
    [Header("VFX")]
    public ArenaAnimator vfx;

    [Header("Blobs")]
    public float blobSpawnRadius;
    public int blobAmount;
    public GameObject blobPrefab;
    public GameObject blobParent;
    public Buff corruptionDebuff;

    [Header("Enemies")]
    public int maxCurrMeleeAttackers;
    public int maxCurrRangedAttackers;

    [HideInInspector]
    public bool encounterTriggered;
    [HideInInspector]
    public int currEnemyCount = 0;
    [HideInInspector]
    public float arenaRadius = 23;
    [HideInInspector]
    public Vector3 farthestPointFromPlayer;
    [HideInInspector]
    public int numberOfCurrMeleeAttackers;
    [HideInInspector]
    public int numberOfCurrRangedAttackers;

    // private bool playerInside;
    // private bool creature1Inside;
    // private bool creature2Inside;
    private PlayerController pc;
    // [HideInInspector]
    // public int numberOfCurrSwarmAttackers;
    [HideInInspector]
    public Transform playerTransform;

    private int currWave = 0;

    //-----------
    // for FMOD
    //-----------
    private SFXManager SFX
    {
        get => PersistentData.Instance.SFXManager.GetComponent<SFXManager>();
    }

    private void Start() 
    {
        pc = PersistentData.Instance.Player.GetComponent<PlayerController>();
    }

    private void Awake() 
    {

    }

    // UGLY  UGLY  UGLY  UGLY  UGLY  UGLY  UGLY  UGLY  UGLY  UGLY  UGLY  UGLY  UGLY  UGLY  UGLY  UGLY  UGLY  UGLY  UGLY  UGLY 
    private void OnTriggerEnter(Collider other) 
    {
        if(other.transform.tag == "Player")
        {
            //Warp creatures to player
            pc.currCreature.GetComponent<NavMeshAgent>().Warp(pc.backFollowPoint.position);
            if(pc.swapCreature)
            {
                pc.swapCreature.GetComponent<NavMeshAgent>().Warp(pc.backFollowPoint.position);
            }
            
            startEncounter();
        //    playerInside = true;
        //    if(pc.currCreature == null)
        //    {
        //        creature1Inside = true;
        //    }
        //    if(pc.swapCreature == null)
        //    {
        //        creature2Inside = true;
        //    }
        }
        // else if(other.transform.tag == "CaptCreature" && other.gameObject == pc.currCreature)
        // {
        //     creature1Inside = true;
        // }
        // else if(other.transform.tag == "CaptCreature" && other.gameObject == pc.swapCreature)
        // {
        //     creature2Inside = true;
        // }
        // if(checkIfEveryoneInside())
        // {
        //     startEncounter();
        // }
    }

    private void OnTriggerExit(Collider other) 
    {
        // if(other.transform.tag == "Player")
        // {
        //    playerInside = false;
        //     if(pc.currCreature == null)
        //    {
        //        creature1Inside = false;
        //    }
        //    if(pc.swapCreature == null)
        //    {
        //        creature2Inside = false;
        //    }
        // }
        // else if(other.transform.tag == "CaptCreature" && other.gameObject == PersistentData.Instance.Player.GetComponent<PlayerController>().currCreature)
        // {
        //     creature1Inside = false;
        // }
        // else if(other.transform.tag == "CaptCreature" && other.gameObject == PersistentData.Instance.Player.GetComponent<PlayerController>().swapCreature)
        // {
        //     creature2Inside = false;
        // }
    }

    // UGLY  UGLY  UGLY  UGLY  UGLY  UGLY  UGLY  UGLY  UGLY  UGLY  UGLY  UGLY  UGLY  UGLY  UGLY  UGLY  UGLY  UGLY  UGLY  UGLY 
    // private bool checkIfEveryoneInside()
    // {
    //     if(playerInside && creature1Inside && creature2Inside)
    //     {
    //         return true;
    //     } 
    //     else 
    //     {
    //         return false;
    //     }
    // }

    private void startEncounter()
    {
        //SFXPlayer.PlayOneShot(SFX.ArenaSpawnSFX, transform.position);
        for(int i = 0; i < blobAmount; i++)
        {
            Vector2 randomPos = Random.insideUnitCircle;
            randomPos *= (Random.Range(10,blobSpawnRadius));
            Instantiate(blobPrefab, new Vector3(transform.position.x + randomPos.x, transform.position.y, transform.position.z + randomPos.y), Quaternion.identity, blobParent.transform);
        }
        blobParent.SetActive(true);
        barrier.SetActive(true);
        vfx.PlayEncounterBegin();
        SpawnEncounter();
        GetComponent<Collider>().enabled = false;
        
        playerTransform = PersistentData.Instance.Player.transform;

        PersistentData.Instance.CameraManager.SetCombatCameraDistance();
        
        PersistentData.Instance.Player.GetComponent<PlayerController>().SetCombatState(true);
        PersistentData.Instance.AudioController.GetComponent<AudioController>().BeginCombatMusic();
        encounterTriggered = true;
        arenaRadius = 23;
    }

    private void Update() 
    {
        if(encounterTriggered)
        {
            farthestPointFromPlayer = (transform.position - playerTransform.position).normalized * arenaRadius;
            farthestPointFromPlayer += transform.position;
            farthestPointFromPlayer.y = 0;
        }
    }



    public void SpawnEncounter()
    {
        if(waves[currWave].spawnWholeWave)
        {
            foreach(GameObject spawner in waves[currWave].spawners)
            {
                SpawnNextEnemy();
            }
        } 
        else 
        {
            SpawnNextEnemy();
        }
    }

    public void enemyKilled()
    {
        currEnemyCount--;
        if(currWave < waves.Count)
        {
            if(waves[currWave].index < waves[currWave].spawners.Count)
            {
                SpawnNextEnemy();
            } 
            else 
            {
                currWave++;
                if(currWave < waves.Count)
                {
                    if(waves[currWave].spawnWholeWave)
                    {
                        foreach(GameObject spawner in waves[currWave].spawners)
                        {
                            SpawnNextEnemy();
                        }
                    }
                    else
                    {
                        SpawnNextEnemy();
                    }
                }
            }
        }
        else
        {
            if(currEnemyCount < 1)
            {
                ClearEncounter();
            }
        }
    }

    public void SpawnNextEnemy()
    {
        waves[currWave].spawners[waves[currWave].index].GetComponent<EnemySpawner>().SpawnEnemy(this);
        currEnemyCount++;
        waves[currWave].index++;
    }


    private void ClearEncounter()
    {
        barrier.SetActive(false);
        vfx.PlayDeathAnimation();
        blobParent.SetActive(false);
        PersistentData.Instance.Player.GetComponent<PlayerController>().SetCombatState(false);
        //PersistentData.Instance.AudioController.GetComponent<AudioController>().BeginOverworldMusic();
        PersistentData.Instance.AudioController.GetComponent<AudioController>().BeginCombatMusicOutro();
        PersistentData.Instance.Player.GetComponent<PlayerController>().stats.RemoveBuff(corruptionDebuff);

        PersistentData.Instance.CameraManager.SetExploreCameraDistance();
    }
}



[System.Serializable]
public class Wave 
{
    public bool spawnWholeWave;
    public int waitUntilEnemiesLeft = 0; //if 0 it will wait for last wave to be finished; otherwise it will spawn more enemies as you kill them;
    public int index = 0;
    public List<GameObject> spawners = new List<GameObject>();
}