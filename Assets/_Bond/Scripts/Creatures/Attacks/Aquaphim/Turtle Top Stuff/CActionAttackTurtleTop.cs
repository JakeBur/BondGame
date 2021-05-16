// Jake
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActionAttackTurtleTop : BTLeaf
{
    CreatureAttackMelee attack;
    public CActionAttackTurtleTop(string _name, CreatureAIContext _context ) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    protected override void OnEnter()
    {
        attack = (CreatureAttackMelee) context.creatureStats.abilities[context.lastTriggeredAbility];
        EnemyAIContext enemyAIContext = context.targetEnemy.GetComponent<EnemyAIContext>();
        enemyAIContext.statManager.TakeDamage(attack.baseDmg, ModiferType.MELEE_RESISTANCE);
        enemyAIContext.healthUIUpdate();
        //Play anim
        AquaphimAnimator animator = context.animator as AquaphimAnimator;
        if (animator == null)
        {
            Debug.LogError("animator is not aquaphim animator");
        }
        animator.TurtleTop();
    }

    protected override void OnExit()
    {
        
    }

    public override NodeState Evaluate() 
    {
        context.targetEnemy = null;
        context.isAbilityTriggered = false;
        if( !context.animator.inAbility ) 
        {
            OnParentExit();
            context.player.GetComponent<PlayerController>().PutOnCD();
            context.wentToPlayerForAbility = false;
            return NodeState.SUCCESS;
        }
        
        return NodeState.RUNNING;
    }
}
