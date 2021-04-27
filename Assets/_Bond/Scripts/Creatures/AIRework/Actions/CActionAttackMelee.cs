using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActionAttackMelee : BTLeaf
{

    CreatureAttackMelee attack;
    public CActionAttackMelee(string _name, CreatureAIContext _context ) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    protected override void OnEnter()
    {
        attack = (CreatureAttackMelee) context.basicCreatureAttack;
        context.targetEnemy.GetComponent<EnemyAIContext>().statManager.TakeDamage(attack.baseDmg, ModiferType.MELEE_RESISTANCE);
        //Play amim
        // Debug.Log("Attacking");
        context.animator.DefaultAttack();
    }

    protected override void OnExit()
    {
        
    }

    public override NodeState Evaluate() 
    {
        context.targetEnemy = null;
        context.isAbilityTriggered = false;
        if( !context.animator.inAttack )
        {
      
            context.lastTriggeredAbility = 10;
            context.player.GetComponent<PlayerController>().PutBasicOnCD();
            // Debug.Log("Ability Id: ");
            // Debug.Log(context.creatureStats.abilities[context.lastTriggeredAbility].id);
            OnParentExit();
            return NodeState.SUCCESS;
        }
        
        return NodeState.RUNNING;
    }

}
