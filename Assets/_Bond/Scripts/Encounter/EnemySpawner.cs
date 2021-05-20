using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> Enemies = new List<GameObject>();

    public GameObject SpawnEnemy(EncounterManager _manager)
    {
        int index = Enemies.Count > 1 ? Random.Range(0,Enemies.Count) : 0;
        GameObject enemy = Instantiate(Enemies[index], gameObject.transform.position, new Quaternion(0,140,0,0), gameObject.transform);
        enemy.GetComponent<EnemyAIContext>().EncounterManager = _manager;
        return enemy;
    }
}
