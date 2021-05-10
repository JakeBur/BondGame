using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public List<GameObject> spawnpoints;
    public List<GameObject> checkpoints;
    public GameObject currSpawnpoint;


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
        currSpawnpoint = spawnpoints[i];
        setSpawnpoint();
    }





}
