using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActionAttackSlimeShot : BTLeaf
{
    CreatureAttackRanged attack;
    public CActionAttackSlimeShot(string _name, CreatureAIContext _context ) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    protected override void OnEnter()
    {
        attack = (CreatureAttackRanged) context.creatureStats.abilities[context.lastTriggeredAbility];
        context.abilitySpawner.GetComponent<AbilitySpawner>().SpawnSlimeShot(attack.projectile, context.targetEnemy, attack.projectileSpeed, attack.baseDamage, attack.abilityBuff);

        //Play correct anim once its made
        SnailAnimator animator = context.animator as SnailAnimator;
        if (animator == null)
        {
            Debug.LogError("animator is not Slugger animator");
        }
        animator.SlimeShot();
    }

    protected override void OnExit()
    {
        
    }

    public override NodeState Evaluate() 
    {
        //Spawn the sun beam
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
