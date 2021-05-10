using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-----------
// for FMOD
//-----------
using SFXPlayer = FMODUnity.RuntimeManager;

public class AbilitySpawner : MonoBehaviour
{
    //-----------
    // for FMOD
    //-----------
    private SFXManager SFX
    {
        get => PersistentData.Instance.SFXManager.GetComponent<SFXManager>();
    }

    public void SpawnProjectile(GameObject projectile, GameObject target, float flyTime, float damage) 
    {
        var proj = Instantiate(projectile, transform.position, Quaternion.identity);
        proj.GetComponent<ProjectileScript>().SetTarget(target, flyTime, damage);
    }

    public void SpawnPetals(GameObject projectile, Transform creatureTransform) 
    {
        //Debug.Log("Creature Rot: " + creatureTransform.eulerAngles.y);
        var creatureRotation = creatureTransform.rotation;
        creatureRotation *= Quaternion.Euler(0, -90, 0);
        Quaternion rot = new Quaternion(creatureTransform.rotation.x, creatureTransform.rotation.y, creatureTransform.rotation.z, creatureTransform.rotation.w);
        var proj = Instantiate(projectile, gameObject.transform.position, creatureRotation);
        // proj.GetComponent<PetalThrow>().setTarget(target, speed, damage, isHoming);
    }

    public void SpawnSunBeam(GameObject projectile, GameObject target, float damage, Buff debuff)
    {
        SFXPlayer.PlayOneShot(SFX.FragariaSunbeamSFX, transform.position);

        var proj = Instantiate(projectile, target.transform.position, Quaternion.identity);
        proj.GetComponent<SunBeam>().setDamage(damage, debuff);
    }

    public void SpawnSporeToss(GameObject projectile, float damage, Buff debuff)
    {
        SFXPlayer.PlayOneShot(SFX.FragariaSporeTossSFX, transform.position);

        var proj = Instantiate(projectile, gameObject.transform.position, Quaternion.identity);
        proj.GetComponent<SporeToss>().setDamage(damage, debuff);
    }

    public void SpawnWaterBeam(GameObject projectile, GameObject target, float speed, float damage, Buff debuff)
    {
        var proj = Instantiate(projectile, transform.position, Quaternion.identity);
        proj.GetComponent<WaterBeam>().setDamage(damage, debuff);
        proj.GetComponent<WaterBeam>().setTarget(target, speed, damage);
    }

    public void SpawnRain(GameObject projectile, float damage, Buff debuff)
    {
        var proj = Instantiate(projectile, transform.position, Quaternion.identity);
        proj.GetComponent<Rain>().setDamage(damage, debuff);
    }
    public void SpawnKnockbackSource(GameObject projectile)
    {
        var proj = Instantiate(projectile, transform.position, Quaternion.identity);
    }    
    public void SpawnSlimeShot(GameObject projectile, GameObject target, float speed, float damage, Buff debuff)
    {
        var proj = Instantiate(projectile, transform.position, Quaternion.identity);
        proj.GetComponent<QuickdrawBullet>().setDamage(damage, debuff);
        proj.GetComponent<QuickdrawBullet>().setTarget(target, speed, damage);
    }

}
