using System.Collections;
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
    public GameObject coreLattice;
	
    [Header("VFX")]
    public ArenaAnimator vfx;

    [Header("Enemies")]
    public List<GameObject> CurrEnemies;
    public int maxCurrMeleeAttackers;
    public int maxCurrRangedAttackers;

    //For triggering post battle dialogue
    DialogueManager dialogueManager;

    [HideInInspector]
    public bool encounterTriggered;
    [HideInInspector]
    public int currEnemyCount = 0;
    [HideInInspector]
    public float arenaRadius = 23;
    [HideInInspector]
    public Vector3 farthestPointFromPlayer;
    // [HideInInspector]
    public int numberOfCurrMeleeAttackers;
    // [HideInInspector]
    public int numberOfCurrRangedAttackers;
    [HideInInspector]
    public bool encounterFinished;

    private PlayerController pc;
    public RewardManager rewardManager;
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
        encounterFinished = false;
    }

    private void Awake() 
    {

    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.transform.tag == "Player")
        {
            startEncounter();
        }
    }

    private void OnTriggerExit(Collider other) 
    {

    }

    public void startEncounter()
    {
        //SFXPlayer.PlayOneShot(SFX.ArenaSpawnSFX, transform.position);
        //Warp creatures to player
        if(pc.currCreature)
        {
            pc.currCreature.GetComponent<NavMeshAgent>().Warp(pc.backFollowPoint.position);
        }
        if(pc.swapCreature)
        {
            pc.swapCreature.GetComponent<NavMeshAgent>().Warp(pc.backFollowPoint.position);
        }

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

    public void enemyKilled(GameObject _enemy)
    {
        currEnemyCount--;
        
        try
        {   
            CurrEnemies.Remove(_enemy);
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
        } catch {
            ClearEncounter();
        }

       
    }

    public void SpawnNextEnemy()
    {
        CurrEnemies.Add(waves[currWave].spawners[waves[currWave].index].GetComponent<EnemySpawner>().SpawnEnemy(this));
        currEnemyCount++;
        waves[currWave].index++;
    }


    private void ClearEncounter()
    {
        foreach(GameObject g in CurrEnemies)
        {
            Destroy(g);
        }
        barrier.SetActive(false);
        //Turn off the lattice around the core to show it has been cleared
        if(coreLattice)
        {
            coreLattice.SetActive(false);
        }
        vfx.PlayDeathAnimation();
        // blobParent.SetActive(false);
        PersistentData.Instance.Player.GetComponent<PlayerController>().SetCombatState(false);
        //PersistentData.Instance.AudioController.GetComponent<AudioController>().BeginOverworldMusic();
        PersistentData.Instance.AudioController.GetComponent<AudioController>().BeginCombatMusicOutro();
        // PersistentData.Instance.Player.GetComponent<PlayerController>().stats.RemoveBuff(corruptionDebuff);
        encounterFinished = true;
        if(rewardManager)
        {
            if(!rewardManager.spawnOnStart)
            {
                rewardManager.spawnReward();
            }
            //Unfreeze the creature
            if(rewardManager.instantiatedCreatureSpawner)
            {
                rewardManager.instantiatedCreatureSpawner.GetComponent<CreatureSpawner>().Creature.GetComponent<CreatureAIContext>().creatureFrozen = false;
            }
        }
        EndofFightDialogue();
        

        PersistentData.Instance.CameraManager.SetExploreCameraDistance();
    }


    //Triggers dialogue 
    void EndofFightDialogue()
    {
        try
        {
            dialogueManager = gameObject.GetComponent<DialogueManager>();

            if(dialogueManager.dialogue != null)
            {
                PersistentData.Instance.Player.GetComponent<PlayerController>().dialogueManager = dialogueManager;
                dialogueManager.StartDialogue();
                

                //set player in standby
                PersistentData.Instance.Player.GetComponent<PlayerController>().SetStandbyState(true);
            }
        }
        catch
        {
            return;
        }     
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