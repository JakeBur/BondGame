using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public List<GameObject> spawnpoints;
    public List<GameObject> checkpoints;
    public List<GameObject> encounters;
    public GameObject currSpawnpoint;

    int checkpointIndex = 0;


    public void setSpawnpoint()
    {
        // foreach(GameObject spawn in spawnpoints)
        // {
        //     if(spawn == currSpawnpoint)
        //     {
        //         spawn.SetActive(true);
        //     }
        //     else
        //     {
        //         spawn.SetActive(false);
        //     }
        // }
    }

    public void UpdateSpawnpoint(int i)
    {
        checkpointIndex = i;
        currSpawnpoint = spawnpoints[checkpointIndex];
        setSpawnpoint();
    }


    public void RespawnPlayer()
    {
        //set pos to currspawn
        PersistentData.Instance.Player.GetComponent<PlayerController>().warpPlayer(spawnpoints[checkpointIndex].transform.position);
    }


    public void ResetEncounter()
    {
        //encounters[checkpointIndex].GetComponent<EncounterManager>();
    }





}
