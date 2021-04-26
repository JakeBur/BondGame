// Jake
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActionAttackThrowKnife : BTLeaf
{
    CreatureAttackRanged attack;
    public CActionAttackThrowKnife(string _name, CreatureAIContext _context ) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    protected override void OnEnter()
    {
        attack = (CreatureAttackRanged) context.basicCreatureAttack;

        context.animator.DefaultAttack();
    }

    protected override void OnExit()
    {
        
    }

    public override NodeState Evaluate() 
    {
        //Spawn the sun beam
        context.abilitySpawner.GetComponent<RabbitAbilitySpawner>().SpawnThrowingKnife(attack.projectile, context.targetEnemy, attack.projectileSpeed, attack.baseDamage, attack.abilityBuff);
        context.targetEnemy = null;
        context.isAbilityTriggered = false;
        if( !context.animator.inAttack ) 
        {
            OnParentExit();
            context.player.GetComponent<PlayerController>().PutBasicOnCD();
            return NodeState.SUCCESS;
        }

        return NodeState.RUNNING;
    }
}
