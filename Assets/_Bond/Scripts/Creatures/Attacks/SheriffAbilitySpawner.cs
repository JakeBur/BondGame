using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-----------
// for FMOD
//-----------
using SFXPlayer = FMODUnity.RuntimeManager;

public class SheriffAbilitySpawner : MonoBehaviour
{
    //-----------
    // for FMOD
    //-----------
    private SFXManager SFX
    {
        get => PersistentData.Instance.SFXManager.GetComponent<SFXManager>();
    }


    public void SpawnProjectile(GameObject projectile, GameObject target, float speed, float damage, bool isHoming) 
    {
        var proj = Instantiate(projectile, transform.position, Quaternion.identity);
        proj.GetComponent<ProjectileScript>().SetTarget(target, speed, damage);
    }

    public void SpawnThrowingKnife(GameObject projectile, GameObject target, float speed, float damage, Buff debuff)
    {
        var proj = Instantiate(projectile, transform.position, Quaternion.identity);
        proj.GetComponent<ThrowingKnife>().setDamage(damage, debuff);
        proj.GetComponent<ThrowingKnife>().setTarget(target, speed, damage);
    }

    public void SpawnQuickdrawBullet(GameObject projectile, GameObject target, float speed, float damage, Buff debuff)
    {
        var proj = Instantiate(projectile, transform.position, Quaternion.identity);
        proj.GetComponent<QuickdrawBullet>().setDamage(damage, debuff);
        proj.GetComponent<QuickdrawBullet>().setTarget(target, speed, damage);
    }

}
