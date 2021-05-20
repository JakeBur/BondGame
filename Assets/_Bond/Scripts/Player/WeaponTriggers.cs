// Jake
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-----------
// for FMOD
//-----------
using SFXPlayer = FMODUnity.RuntimeManager;

public class WeaponTriggers : MonoBehaviour
{
    //-----------
    // for FMOD
    //-----------
    private SFXManager SFX
    {
        get => PersistentData.Instance.SFXManager.GetComponent<SFXManager>();
    }

    private StatManager statManager;
    private PlayerStateMachine playerStateMachine;
    private PlayerAnimator playerAnimator;
    // Start is called before the first frame update
    private void Start() 
    {
        statManager = GameObject.FindGameObjectWithTag("Player").GetComponent<StatManager>();
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStateMachine>();
        playerStateMachine = PersistentData.Instance.Player.GetComponent<PlayerStateMachine>();
        playerAnimator = PersistentData.Instance.Player.GetComponent<PlayerAnimator>();
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.gameObject.tag == "Enemy")
        {
            EnemyAIContext enemyAIContext = other.gameObject.GetComponent<EnemyAIContext>();

            float damage = statManager.getStat(ModiferType.DAMAGE);

            //Check for crits
            int range = (int) Mathf.Round(100/statManager.getStat(ModiferType.CRIT_CHANCE));
            if(Random.Range(1, range) == 1)
            {
                damage *= statManager.getStat(ModiferType.CRIT_DAMAGE);
                SFXPlayer.PlayOneShot(SFX.PlayerSwordCritSFX, transform.position);
            }

            float damageModifier = 1.0f;

            // Check which attack state we're in to determine damage
            if (playerStateMachine.currentState == playerStateMachine.Slash0)
            {
                damageModifier = 1.2f;
            } 
            else if(playerStateMachine.currentState == playerStateMachine.Slash1)
            {
                damageModifier = 1f;
            } 
            else if(playerStateMachine.currentState == playerStateMachine.Slash2)
            {
                damageModifier = 1f;
            } 
            else if(playerStateMachine.currentState == playerStateMachine.Slash3)
            {
                damageModifier = 1f;
            } 
            else if(playerStateMachine.currentState == playerStateMachine.Slash4)
            {
                damageModifier = 1.5f;
            } 
            else if(playerStateMachine.currentState == playerStateMachine.HeavySlash)
            {
                damageModifier = 2f;
            }

            enemyAIContext.statManager.TakeDamage(damage * damageModifier, ModiferType.MELEE_RESISTANCE);
            enemyAIContext.healthUIUpdate();

            //If enemy is spawning, don't play hit sound or effects
            if(!enemyAIContext.animator.inSpawn)
            {
                GameObject squib = Instantiate(playerAnimator.vfxData.enemySquib, enemyAIContext.transform);
                squib.transform.localPosition = Vector3.up * 0.7f;
                //GameObject squib = Instantiate(playerAnimator.vfxData.enemySquib, enemyAIContext.transform.position, Quaternion.identity);
                //GameObject squib = Instantiate(playerAnimator.vfxData.enemySquib, transform.position, Quaternion.identity);
                //GameObject squib = Instantiate(playerAnimator.vfxData.enemySquib, Vector3.zero, Quaternion.identity);
                squib.transform.LookAt(squib.transform.position + new Vector3(transform.forward.x, 0, transform.forward.z));

                SFXPlayer.PlayOneShot(SFX.PlayerSwordImpactSFX, transform.position);
            }
        }
        else if(other.gameObject.tag == "FruitTree")
        {
            print("Hit tree with sword");
            other.gameObject.GetComponent<FruitTree>().dropFruit();
        }
    }
}
