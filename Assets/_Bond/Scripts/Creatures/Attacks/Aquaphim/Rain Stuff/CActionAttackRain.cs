using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActionAttackRain : BTLeaf
{
    creatureAttackRanged attack;

    public CActionAttackRain(string _name, CreatureAIContext _context ) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    protected override void OnEnter()
    {
        attack = (creatureAttackRanged) context.creatureStats.abilities[context.lastTriggeredAbility];

        AquaphimAnimator animator = context.animator as AquaphimAnimator;
        if (animator == null)
        {
            Debug.LogError("animator is not aquaphim animator");
        }
        animator.Rain();
    }

    protected override void OnExit()
    {
        
    }

    public override NodeState Evaluate() 
    {
        //Spawn the sun beam
        context.abilitySpawner.GetComponent<AbilitySpawner>().SpawnRain(attack.projectile, attack.baseDmg, attack.abilityBuff);
        
        context.targetEnemy = null;
        context.isAbilityTriggered = false;
        if( !context.animator.inAbility )
        {
            OnParentExit();
            context.player.GetComponent<PlayerController>().PutOnCD();
            return NodeState.SUCCESS;
        }
        
        return NodeState.RUNNING;
    }
}
