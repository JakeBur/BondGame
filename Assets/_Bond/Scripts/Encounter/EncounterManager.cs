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
    public float arenaRadius;
    [HideInInspector]
    public Vector3 farthestPointFromPlayer;
    [HideInInspector]
    public int numberOfCurrMeleeAttackers;
    [HideInInspector]
    public int numberOfCurrRangedAttackers;
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


    private void OnTriggerEnter(Collider other) 
    {
        if(other.transform.tag == "Player")
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
            
            PersistentData.Instance.Player.GetComponent<PlayerController>().SetCombatState(true);
            PersistentData.Instance.AudioController.GetComponent<AudioController>().BeginCombatMusic();
            encounterTriggered = true;
        }
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