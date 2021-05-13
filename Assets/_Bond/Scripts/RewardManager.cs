using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardManager : MonoBehaviour
{
    public Transform spawnLocation;

    public GameObject relicBase;

    public GameObject acornBagPrefab;

    public GameObject creatureSpawnerPrefab;

    public GameObject instantiatedCreatureSpawner;

    public Vector2 costRange;

    public bool spawnOnStart = true;

    public GameObject relicSprite;

    public GameObject acornBagSprite;

    private string reward;

    private float randomNum;

    private int randomRelic;

    private GameObject tempSprite;

    private void Start()
    {
        randomRelic = Random.Range(0,PersistentData.Instance.availableRelics.Count);
        decideReward();

        //Spawn the item immediately
        if(spawnOnStart)
        {
            spawnReward();
        } else 
        {
            //Spawn the sprite only
            if(reward == "relic") //Spawn relic icon
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
        if(reward == "relic")
        {
            SetUpRelic();
        } else if(reward == "acorn")
        {
            SetUpAcornBag();
        } else 
        {
            SetUpCreature();
        }
    }

    private void SetUpRelic()
    {
        var tempGameObj = Instantiate(relicBase, spawnLocation.position, Quaternion.Euler(new Vector3(25,-45,0)));
        // Debug.Log("Setting up relic: " + randomRelic);
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

    private void SetUpCreature()
    {
        instantiatedCreatureSpawner = Instantiate(creatureSpawnerPrefab, spawnLocation.position, Quaternion.Euler(new Vector3(25,-45,0)));
    }

    private void decideReward()
    {
        randomNum = Random.Range(0f, 1f);
        //If all are given, choose one of them
        if(relicBase && acornBagPrefab && creatureSpawnerPrefab)
        {
            if(randomNum >= .8) //Spawn a creature
            {
                reward = "creature";
            } else if(randomNum >= .5) //Spawn relic
            {
                reward = "relic";
            } else //Spawn acorns
            {
                reward = "acorn";
            }
        } else if(relicBase && acornBagPrefab) //If relic and acorn are given, choose one of them
        {
            if(randomNum >= .5) //Spawn a relic
            {
                reward = "relic";
            } else //Spawn acorns
            {
                reward = "acorn";
            }
        } else if(relicBase && creatureSpawnerPrefab) //If relic and acorn are given, choose one of them
        {
            if(randomNum >= .4) //Spawn a relic
            {
                reward = "relic";
            } else //Spawn a creature
            {
                reward = "creature";
            }
        } else if(acornBagPrefab && creatureSpawnerPrefab) //If relic and acorn are given, choose one of them
        {
            if(randomNum >= .4) //Spawn acorns
            {
                reward = "acorn";
            } else //Spawn creature
            {
                reward = "creature";
            }
        } else if(acornBagPrefab) //If only acorn is given, choose acorn
        {
            reward = "acorn";
        } else if(relicBase) //If only relic is given, choose relic
        {
            reward = "relic";
        } else if(creatureSpawnerPrefab) //If only creature is given, choose creature
        {
            reward = "creature";
        }
        

        if(reward == "creature")
        {
            spawnOnStart = true;
            creatureSpawnerPrefab.GetComponent<CreatureSpawner>().frozen = true;
        }
    }
}
