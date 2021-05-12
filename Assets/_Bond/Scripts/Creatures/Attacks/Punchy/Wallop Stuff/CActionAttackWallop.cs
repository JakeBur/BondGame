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
        EnemyAIContext enemyAIContext = context.targetEnemy.GetComponent<EnemyAIContext>();
        attack = (CreatureAttackMelee) context.creatureStats.abilities[context.lastTriggeredAbility];
        enemyAIContext.statManager.TakeDamage(attack.baseDmg, ModiferType.MELEE_RESISTANCE);
        enemyAIContext.healthUIUpdate();
        //Play amim
    }

    protected override void OnExit()
    {
        
    }

    public override NodeState Evaluate() 
    {
        EnemyAIContext enemyAIContext = context.targetEnemy.GetComponent<EnemyAIContext>();
        Vector3 moveDirection = context.targetEnemy.transform.position - context.creatureTransform.transform.position;
        enemyAIContext.rb.isKinematic = false;
        enemyAIContext.agent.isStopped = true;
        enemyAIContext.rb.AddForce(moveDirection.normalized * 450f);
        CreatureCoroutineController.Start( knockback(enemyAIContext) );
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
    IEnumerator knockback(EnemyAIContext enemyAIContext)
    {
        yield return new WaitForSeconds(1);
        enemyAIContext.rb.velocity = Vector3.zero;
        enemyAIContext.rb.angularVelocity = Vector3.zero;
        enemyAIContext.rb.isKinematic = true;
        enemyAIContext.agent.isStopped = false;   
    }

}
