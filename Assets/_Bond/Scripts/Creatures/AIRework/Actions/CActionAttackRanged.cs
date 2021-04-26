using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActionAttackRanged : BTLeaf
{
    CreatureAttackRanged attack;
    public CActionAttackRanged(string _name, CreatureAIContext _context ) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    protected override void OnEnter()
    {
        attack = (CreatureAttackRanged) context.creatureStats.abilities[context.lastTriggeredAbility];
        //Play amim
        // Debug.Log("Attacking");
        context.animator.DefaultAttack();
    }

    protected override void OnExit()
    {
        
    }

    public override NodeState Evaluate() 
    {
        //Debug.Log("ATTACK RANGED");
        context.abilitySpawner.GetComponent<AbilitySpawner>()
            .SpawnProjectile(attack.projectile, context.targetEnemy, attack.flyTime, attack.baseDamage);
        
        context.targetEnemy = null;
        context.isAbilityTriggered = false;
        if( !context.animator.inAttack ) 
        {
            OnParentExit();
            context.player.GetComponent<PlayerController>().PutOnCD();
            // Debug.Log("Ability Id: ");
            // Debug.Log(context.creatureStats.abilities[context.lastTriggeredAbility].id);
            return NodeState.SUCCESS;
        }
        
        return NodeState.RUNNING;
    }
}
