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
            other.gameObject.GetComponent<StatManager>().TakeDamage(context.statManager.stats[ModiferType.DAMAGE].modifiedValue, ModiferType.MELEE_RESISTANCE);
            other.gameObject.GetComponent<PlayerController>().DeathCheck();

            if(context.enemyType != "SwarmEnemy")
            {
                other.GetComponent<PlayerController>().isHit = true;
            } else 
            {
                PersistentData.Instance.hudManager.HurtFeedback(1f, 0f); //makes the red flash on screen
                PersistentData.Instance.hudManager.HurtFeedback(0f, 0.3f);
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
