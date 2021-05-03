using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActionAttackBeegPunch : BTLeaf
{

    CreatureAttackMelee attack;
    public CActionAttackBeegPunch(string _name, CreatureAIContext _context ) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    protected override void OnEnter()
    {
        attack = (CreatureAttackMelee) context.creatureStats.abilities[context.lastTriggeredAbility];
        //Play amim
    }

    protected override void OnExit()
    {
        
    }

    public override NodeState Evaluate() 
    {
        context.targetEnemy.GetComponent<EnemyAIContext>().statManager.TakeDamage(attack.baseDmg, ModiferType.MELEE_RESISTANCE);
        Rigidbody rb =  context.targetEnemy.GetComponent<EnemyAIContext>().rb;
        // hit.GetComponent<EnemyAIContext>().rb;
        // NavMeshAgent agent = context.targetEnemy.GetComponent<EnemyAIContext>().agent;
        // agent.isStopped = false;

        // rb.AddExplosionForce(power, explosionPos, radius, 1.0F, ForceMode.Impulse);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
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

}
