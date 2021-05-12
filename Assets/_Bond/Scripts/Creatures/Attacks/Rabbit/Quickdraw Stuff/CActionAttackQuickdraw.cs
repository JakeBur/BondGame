﻿// Jake
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActionAttackQuickdraw : BTLeaf
{
    CreatureAttackRanged attack;
    public CActionAttackQuickdraw(string _name, CreatureAIContext _context ) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    protected override void OnEnter()
    {
        attack = (CreatureAttackRanged) context.creatureStats.abilities[context.lastTriggeredAbility];

        //Play correct anim once its made
        EmptyCreatureAnimator animator = context.animator as EmptyCreatureAnimator;
        if (animator == null)
        {
            Debug.LogError("animator is not Lilibun animator");
        }
        animator.Ability();
    }

    protected override void OnExit()
    {
        
    }

    public override NodeState Evaluate() 
    {
        //Spawn the sun beam
        context.abilitySpawner.GetComponent<RabbitAbilitySpawner>().SpawnQuickdrawBullet(attack.projectile, context.targetEnemy, attack.projectileSpeed, attack.baseDamage, attack.abilityBuff);
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
