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
            //makes player not take more damage after death
            if(other.gameObject.GetComponent<StatManager>().getStat(ModiferType.CURR_HEALTH) <= 0)
            {
                return;
            }

            other.gameObject.GetComponent<StatManager>().TakeDamage(context.statManager.stats[ModiferType.DAMAGE].modifiedValue, ModiferType.MELEE_RESISTANCE);
            other.gameObject.GetComponent<PlayerController>().DeathCheck();

            if(context.enemyType != "SwarmEnemy")
            {
                other.GetComponent<PlayerController>().isHit = true;
            }

            SFXPlayer.PlayOneShot(SFX.PlayerDamagedDonutSFX, transform.position);

            DisableHitbox();
        }
    }

    private void DisableHitbox()
    {
        GetComponent<BoxCollider>().enabled = false;
    }
    
}
