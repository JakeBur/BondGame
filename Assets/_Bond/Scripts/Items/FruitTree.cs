// Jake
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitTree : MonoBehaviour
{
    [SerializeField]
    public Fruit newFruit;
    [SerializeField]
    public GameObject fruitSpawnLocation;

    public void dropFruit()
    {
        Instantiate(newFruit, fruitSpawnLocation.transform.position, Quaternion.identity);
    }
}
