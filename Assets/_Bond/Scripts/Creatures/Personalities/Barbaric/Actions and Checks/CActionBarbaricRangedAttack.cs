﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActionBarbaricRangedAttack : BTLeaf
{
    creatureAttackRanged attack;
    public CActionBarbaricRangedAttack(string _name, CreatureAIContext _context ) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    protected override void OnEnter()
    {
        attack = (creatureAttackRanged) context.creatureStats.abilities[context.lastTriggeredAbility];
        //Play amim
        context.animator.Attack1();
    }

    protected override void OnExit()
    {
        
    }

    public override NodeState Evaluate() 
    {
        context.abilitySpawner.GetComponent<AbilitySpawner>().SpawnProjectile(attack.projectile, context.targetEnemy, attack.flyTime, attack.baseDamage);
        if(Random.Range(0f,1f) < 0.5) 
        {
            context.abilitySpawner.GetComponent<AbilitySpawner>().SpawnProjectile(attack.projectile, context.targetEnemy, attack.flyTime, attack.baseDamage);
        }
        context.targetEnemy = null;
        context.isAbilityTriggered = false;
        if(true) 
        { //if animation done, have to add that
            OnParentExit(); 
            return NodeState.SUCCESS;
        }
    }
}
