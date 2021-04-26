// Jake
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CActionAttackSporeToss : BTLeaf
{
    CreatureAttackUtility attack;
    private NavMeshAgent agent;
    public CActionAttackSporeToss(string _name, CreatureAIContext _context ) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    protected override void OnEnter()
    {
        attack = (CreatureAttackUtility) context.creatureStats.abilities[context.lastTriggeredAbility];
        
        //Play anim
        FragariaAnimator animator = context.animator as FragariaAnimator;
        if (animator == null)
        {
            Debug.LogError("animator is not fragaria animator");
        }
        animator.SporeToss();
    }

    protected override void OnExit()
    {
        
    }

    public override NodeState Evaluate() 
    {
        //Spawn the sun beam
        context.abilitySpawner.GetComponent<AbilitySpawner>().SpawnSporeToss(attack.projectile, attack.baseDmg, attack.abilityBuff);
        
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
