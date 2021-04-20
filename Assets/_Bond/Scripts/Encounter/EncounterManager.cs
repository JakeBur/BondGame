using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-----------
// for FMOD
//-----------
using SFXPlayer = FMODUnity.RuntimeManager;

public class EncounterManager : MonoBehaviour
{
    public List<Wave> waves = new List<Wave>();
    public int currEnemyCount = 0;
    public GameObject barrier;
    public GameObject blobs;
    public Buff corruptionDebuff;
    private int currWave = 0;
    public bool encounterTriggered;

    public float farthestDistRadius;
    public Vector3 farthestPointFromPlayer;

    public int maxCurrMeleeAttackers;
    public int numberOfCurrMeleeAttackers;

    public int maxCurrRangedAttackers;
    public int numberOfCurrRangedAttackers;

    public Transform playerTransform;

    //-----------
    // for FMOD
    //-----------
    private SFXManager SFX
    {
        get => PersistentData.Instance.SFXManager.GetComponent<SFXManager>();
    }


    private void OnTriggerEnter(Collider other) 
    {
        if(other.transform.tag == "Player")
        {
            //SFXPlayer.PlayOneShot(SFX.ArenaSpawnSFX, transform.position);
            blobs.SetActive(true);
            barrier.SetActive(true);
            SpawnEncounter();
            GetComponent<Collider>().enabled = false;
            
            playerTransform = PersistentData.Instance.Player.transform;
            
            PersistentData.Instance.Player.GetComponent<PlayerController>().SetCombatState(true);
            PersistentData.Instance.AudioController.GetComponent<AudioController>().BeginCombatMusic();
            encounterTriggered = true;
        }
    }

    private void Update() {
        if(encounterTriggered)
        {
            farthestPointFromPlayer = (transform.position - playerTransform.position).normalized * farthestDistRadius;
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
        blobs.SetActive(false);
        PersistentData.Instance.Player.GetComponent<PlayerController>().SetCombatState(false);
        //PersistentData.Instance.AudioController.GetComponent<AudioController>().BeginOverworldMusic();
        PersistentData.Instance.AudioController.GetComponent<AudioController>().BeginCombatMusicOutro();
        PersistentData.Instance.Player.GetComponent<PlayerController>().stats.RemoveBuff(corruptionDebuff);
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