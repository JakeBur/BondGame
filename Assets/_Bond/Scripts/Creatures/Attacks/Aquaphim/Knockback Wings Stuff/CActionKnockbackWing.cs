using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActionKnockbackWing : BTLeaf
{
    creatureAttackUtility attack;

    public CActionKnockbackWing(string _name, CreatureAIContext _context ) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }


    protected override void OnEnter()
    {
        attack = (creatureAttackUtility) context.creatureStats.abilities[context.lastTriggeredAbility];
        //Play anim
        AquaphimAnimator animator = context.animator as AquaphimAnimator;
        if (animator == null)
        {
            Debug.LogError("animator is not aquaphim animator");
        }
        animator.WingGust();
    }
    protected override void OnExit()
    {
        
    }

    public override NodeState Evaluate() 
    {
        context.abilitySpawner.GetComponent<AbilitySpawner>().SpawnKnockbackSource(attack.projectile);
        context.targetEnemy = null;
        context.isAbilityTriggered = false;
        if( !context.animator.inAbility ) 
        { //if animation done, have to add that 
            OnParentExit();
            context.player.GetComponent<PlayerController>().PutOnCD();
            return NodeState.SUCCESS;
        }
        
        return NodeState.RUNNING;
    }


}
