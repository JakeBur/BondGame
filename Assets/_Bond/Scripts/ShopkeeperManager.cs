using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopkeeperManager : MonoBehaviour
{
    public List<Transform> relicSpawnLocations;

    public GameObject relicBase;

    public Vector2 costRange;

    private void Start() {
        foreach(Transform _transform in relicSpawnLocations)
        {
            var tempGameObj = Instantiate(relicBase, _transform.position, Quaternion.Euler(new Vector3(25,-45,0)));
            int randomRelic = Random.Range(0,PersistentData.Instance.availableRelics.Count);
            tempGameObj.GetComponent<RelicInteractable>().relicStats = PersistentData.Instance.availableRelics[randomRelic]; 
            PersistentData.Instance.availableRelics.Remove(PersistentData.Instance.availableRelics[randomRelic]);
            tempGameObj.GetComponent<RelicInteractable>().cost = Random.Range((int)costRange.x, (int)costRange.y);
        }
    }
}
