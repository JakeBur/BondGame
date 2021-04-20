using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-----------
// for FMOD
//-----------
using SFXPlayer = FMODUnity.RuntimeManager;

public class EnemyRangedAttackSpawner : MonoBehaviour
{
    public GameObject projectile;

    //-----------
    // for FMOD
    //-----------
    private SFXManager SFX
    {
        get => PersistentData.Instance.SFXManager.GetComponent<SFXManager>();
    }

    public void SpawnProjectile(GameObject target, float speed, float damage, bool isHoming) 
    {
        var proj = Instantiate(projectile, transform.position, Quaternion.identity);
        proj.GetComponent<EnemyProjectile>().setTarget(target, speed, damage, isHoming);
    }
}
