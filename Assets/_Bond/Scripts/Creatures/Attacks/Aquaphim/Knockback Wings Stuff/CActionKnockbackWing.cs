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
        context.animator.Attack1();
    }
    protected override void OnExit()
    {
        
    }

    public override NodeState Evaluate() 
    {
        context.abilitySpawner.GetComponent<AbilitySpawner>().SpawnKnockbackSource(attack.projectile);
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
