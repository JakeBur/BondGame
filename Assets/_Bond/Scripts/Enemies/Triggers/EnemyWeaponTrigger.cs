using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-----------
// for FMOD
//-----------
using SFXPlayer = FMODUnity.RuntimeManager;

public class EnemyWeaponTrigger : MonoBehaviour
{
    [SerializeField]
    public EnemyAIContext context;

    //public BoxCollider hitbox => gameObject.GetComponent<BoxCollider>();

    //-----------
    // for FMOD
    //-----------
    private SFXManager SFX
    {
        get => PersistentData.Instance.SFXManager.GetComponent<SFXManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<StatManager>().TakeDamage(context.statManager.stats[ModiferType.DAMAGE].modifiedValue, ModiferType.MELEE_RESISTANCE);
            other.gameObject.GetComponent<PlayerController>().DeathCheck();


            if(!(context.enemyType == "SwarmEnemy"))
            {
                other.GetComponent<PlayerController>().isHit = true;
            }
            // other.GetComponent<PlayerController>().isHit = true;

            SFXPlayer.PlayOneShot(SFX.PlayerDamagedDonutSFX, transform.position);

            DisableHitbox();
        }
        // else if(other.gameObject.tag == "CaptCreature")
        // {
        //     other.gameObject.GetComponent<StatManager>().TakeDamageCreature(context.statManager.stats[ModiferType.DAMAGE].modifiedValue, ModiferType.MELEE_RESISTANCE);
        //     other.GetComponent<CreatureAIContext>().isHit = true;
        //     // other.GetComponentInChildren<EnthusiasmUI>().UpdateEnthusiasm();
        // }
    }

    // public void ColliderOnOff()
    // {
    //     hitbox.enabled = !hitbox.enabled;
    // }

    private void DisableHitbox()
    {
        GetComponent<BoxCollider>().enabled = false;
    }
    
}
