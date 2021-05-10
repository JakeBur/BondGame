using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public Transform spawnLocation;

    public GameObject relicBase;

    public GameObject acornBagPrefab;

    public Vector2 costRange;

    public bool spawnOnStart = true;

    public GameObject relicSprite;

    public GameObject acornBagSprite;

    private bool willSpawnRelic;

    private float randomNum;

    private int randomRelic;

    private GameObject tempSprite;

    private void Start()
    {
        randomRelic = Random.Range(0,PersistentData.Instance.availableRelics.Count);
        // Debug.Log("Relic Num: " + randomRelic);
        randomNum = Random.Range(0f, 1f);

        //If both are given, choose one of them
        if(relicBase && acornBagPrefab)
        {
            //Spawn a relic
            if(randomNum >= .5)
            {
                willSpawnRelic = true;
            } else //spawn a bag of acorns
            {
                willSpawnRelic = false;
            }
        } else if(relicBase)
        {
            willSpawnRelic = true;
        } else if(acornBagPrefab)
        {
            willSpawnRelic = false;
        }

        //Spawn the item immediately
        if(spawnOnStart)
        {
            spawnReward();
        } else 
        {
            //Spawn the sprite only
            if(willSpawnRelic) //Spawn relic icon
            {
                tempSprite = Instantiate(relicSprite, spawnLocation.position, Quaternion.Euler(new Vector3(25,-45,0)));
                tempSprite.GetComponent<SpriteRenderer>().sprite = PersistentData.Instance.availableRelics[randomRelic].relicSprite;
            } else //Spawn bag icon
            {
                tempSprite = Instantiate(acornBagSprite, spawnLocation.position, Quaternion.Euler(new Vector3(25,-45,0)));
            }
        }
    }

    public void spawnReward()
    {
        if(tempSprite)
        {
            Destroy(tempSprite);
        }
        if(willSpawnRelic)
        {
            SetUpRelic();
        } else 
        {
            SetUpAcornBag();
        }
    }

    private void SetUpRelic()
    {
        var tempGameObj = Instantiate(relicBase, spawnLocation.position, Quaternion.Euler(new Vector3(25,-45,0)));
        Debug.Log("Setting up relic: " + randomRelic);
        tempGameObj.GetComponent<RelicInteractable>().relicStats = PersistentData.Instance.availableRelics[randomRelic]; 
        // PersistentData.Instance.availableRelics.Remove(PersistentData.Instance.availableRelics[randomRelic]);
        tempGameObj.GetComponent<RelicInteractable>().cost = Random.Range((int)costRange.x, (int)costRange.y);

        tempGameObj.GetComponent<RelicInteractable>().updateSprite();
    }

    private void SetUpAcornBag()
    {
        var acornBag = Instantiate(acornBagPrefab, spawnLocation.position, Quaternion.Euler(new Vector3(25,-45,0)));
        acornBag.GetComponent<AcornBagInteractable>().cost = Random.Range((int)costRange.x, (int)costRange.y);
    }
}
