// Jake
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActionAttackQuickdraw : BTLeaf
{
    creatureAttackRanged attack;
    public CActionAttackQuickdraw(string _name, CreatureAIContext _context ) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    protected override void OnEnter()
    {
        attack = (creatureAttackRanged) context.creatureStats.abilities[context.lastTriggeredAbility];

        //Play correct anim once its made
        context.animator.DefaultAttack();
    }

    protected override void OnExit()
    {
        
    }

    public override NodeState Evaluate() 
    {
        //Spawn the sun beam
        context.abilitySpawner.GetComponent<SheriffAbilitySpawner>().SpawnQuickdrawBullet(attack.projectile, context.targetEnemy, attack.projectileSpeed, attack.baseDmg, attack.isHoming, attack.abilityBuff);
        context.targetEnemy = null;
        context.isAbilityTriggered = false;
        if(true) 
        { //if animation done, have to add that 
            OnParentExit();
            context.player.GetComponent<PlayerController>().PutOnCD();
            return NodeState.SUCCESS;
        }
    }
}
