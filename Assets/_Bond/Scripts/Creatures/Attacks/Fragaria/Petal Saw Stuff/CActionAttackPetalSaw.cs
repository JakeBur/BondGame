using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActionAttackPetalSaw : BTLeaf
{

    CreatureAttackMelee attack;
    public CActionAttackPetalSaw(string _name, CreatureAIContext _context ) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    protected override void OnEnter()
    {
        attack = (CreatureAttackMelee) context.creatureStats.abilities[context.lastTriggeredAbility];
        context.targetEnemy.GetComponent<EnemyAIContext>().statManager.TakeDamage(attack.baseDmg, ModiferType.MELEE_RESISTANCE);
        //Play amim
        // Debug.Log("Attacking");
        FragariaAnimator animator = context.animator as FragariaAnimator;
        if (animator == null)
        {
            Debug.LogError("animator is not fragaria animator");
        }
        animator.PetalSaw();
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

}
