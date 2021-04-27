// Jake
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActionAttackWaterBeam : BTLeaf
{
    CreatureAttackRanged attack;
    public CActionAttackWaterBeam(string _name, CreatureAIContext _context ) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    protected override void OnEnter()
    {
        attack = (CreatureAttackRanged) context.basicCreatureAttack;
        //Play anim
        context.animator.DefaultAttack();
    }

    protected override void OnExit()
    {
        
    }

    public override NodeState Evaluate() 
    {
        //Spawn the sun beam
        context.abilitySpawner.GetComponent<AbilitySpawner>().SpawnWaterBeam(attack.projectile, context.targetEnemy, attack.flyTime, attack.baseDamage, attack.abilityBuff);
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
