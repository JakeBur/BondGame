using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActionBarbaricMeleeAttack : BTLeaf
{
    CreatureAttackMelee attack;
    public CActionBarbaricMeleeAttack(string _name, CreatureAIContext _context ) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    protected override void OnEnter()
    {
        attack = (CreatureAttackMelee) context.creatureStats.abilities[context.lastTriggeredAbility];
        //Play amim
        context.animator.DefaultAttack();
   
    }

    protected override void OnExit()
    {
    }

    public override NodeState Evaluate() 
    {
        // context.targetEnemy.GetComponent<EnemyStats>().takeDamage(attack.baseDmg);
        if(Random.Range(0f,1f) < 0.5)
        {
            // context.targetEnemy.GetComponent<EnemyStats>().takeDamage(attack.baseDmg);
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
