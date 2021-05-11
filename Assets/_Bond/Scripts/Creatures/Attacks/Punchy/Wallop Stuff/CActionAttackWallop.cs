using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActionAttackWallop : BTLeaf
{

    CreatureAttackMelee attack;
    public CActionAttackWallop(string _name, CreatureAIContext _context ) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    protected override void OnEnter()
    {
        attack = (CreatureAttackMelee) context.creatureStats.abilities[context.lastTriggeredAbility];
        CreatureCoroutineController.Start( knockback() );
        //Play amim
    }

    protected override void OnExit()
    {
        
    }

    public override NodeState Evaluate() 
    {
        context.targetEnemy = null;
        context.isAbilityTriggered = false;
        if( !context.animator.inAbility )
        { //if animation done, have to add that 
      
            context.player.GetComponent<PlayerController>().PutOnCD();
            // Debug.Log("Ability Id: ");
            // Debug.Log(context.creatureStats.abilities[context.lastTriggeredAbility].id);
            OnParentExit();
            return NodeState.SUCCESS;
        }
        
        return NodeState.RUNNING;
    }
    IEnumerator knockback()
    {
        context.targetEnemy.GetComponent<EnemyAIContext>().statManager.TakeDamage(attack.baseDmg, ModiferType.MELEE_RESISTANCE);
        context.targetEnemy.GetComponent<EnemyAIContext>().healthUIUpdate();
        Vector3 moveDirection = context.targetEnemy.transform.position - context.creatureTransform.transform.position;
        context.targetEnemy.GetComponent<EnemyAIContext>().rb.isKinematic = false;
        context.targetEnemy.GetComponent<EnemyAIContext>().agent.isStopped = true;
        context.targetEnemy.GetComponent<EnemyAIContext>().rb.AddForce(moveDirection.normalized * 50f);
        yield return new WaitForSeconds(1);
        context.targetEnemy.GetComponent<EnemyAIContext>().rb.velocity = Vector3.zero;
        context.targetEnemy.GetComponent<EnemyAIContext>().rb.angularVelocity = Vector3.zero;
        context.targetEnemy.GetComponent<EnemyAIContext>().rb.isKinematic = true;
        context.targetEnemy.GetComponent<EnemyAIContext>().agent.isStopped = false;   
    }

}
