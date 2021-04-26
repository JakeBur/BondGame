using System.Collections;
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
        context.animator.DefaultAttack();
    }

    protected override void OnExit()
    {
        
    }

    public override NodeState Evaluate() 
    {
       
        context.abilitySpawner.GetComponent<AbilitySpawner>().SpawnProjectile(attack.projectile, context.targetEnemy, attack.projectileSpeed, attack.baseDmg, attack.isHoming);
        if(Random.Range(0f,1f) < 0.5) 
        {
            context.abilitySpawner.GetComponent<AbilitySpawner>().SpawnProjectile(attack.projectile, context.targetEnemy, attack.projectileSpeed, attack.baseDmg, attack.isHoming);
        }
        context.targetEnemy = null;
        context.isAbilityTriggered = false;
        if( !context.animator.inAttack ) 
        { //if animation done, have to add that
            OnParentExit(); 
            return NodeState.SUCCESS;
        }
        
        return NodeState.RUNNING;
    }
}
