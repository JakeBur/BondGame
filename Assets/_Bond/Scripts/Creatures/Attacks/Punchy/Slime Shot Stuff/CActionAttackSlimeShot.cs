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

        //Play correct anim once its made
        context.animator.DefaultAttack();
    }

    protected override void OnExit()
    {
        
    }

    public override NodeState Evaluate() 
    {
        //Spawn the sun beam
        context.gameObject.transform.LookAt(context.targetEnemy.transform);
        context.gameObject.transform.rotation = new Quaternion(0, context.gameObject.transform.rotation.y, 0, context.gameObject.transform.rotation.w);
        context.abilitySpawner.GetComponent<AbilitySpawner>().SpawnSlimeShot(attack.projectile, context.targetEnemy, attack.projectileSpeed, attack.baseDamage, attack.abilityBuff);
        context.targetEnemy = null;
        context.isAbilityTriggered = false;
        if( !context.animator.inAttack ) 
        { //if animation done, have to add that 
            OnParentExit();
            context.player.GetComponent<PlayerController>().PutOnCD();
            return NodeState.SUCCESS;
        }

        return NodeState.RUNNING;
    }
}
