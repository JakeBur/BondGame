// Jake
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActionAttackNothingPersonal : BTLeaf
{
    CreatureAttackMelee attack;
    public CActionAttackNothingPersonal(string _name, CreatureAIContext _context ) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    protected override void OnEnter()
    {
        attack = (CreatureAttackMelee) context.creatureStats.abilities[context.lastTriggeredAbility];

        //Play correct anim once its made
        RabbitAnimator animator = context.animator as RabbitAnimator;
        if (animator == null)
        {
            Debug.LogError("animator is not Lilibun animator");
        }
        animator.ElusiveStepStrike();
    }

    protected override void OnExit()
    {
        
    }

    public override NodeState Evaluate() 
    {

        int layermask = 1 << 8; //only layer 8 will be targeted
        Collider[] enemiesDamaged = Physics.OverlapCapsule(context.creatureTransform.position, context.targetEnemy.transform.position, 4, layermask);

        //Do teleport effect

        //Warp to the furthest enemy (Aka target Enemy)
        context.agent.Warp(context.targetEnemy.transform.position);

        //Do appearing effect
        
        //Damage target enemy + all enemies caught between
        foreach (var hitCollider in enemiesDamaged)
        { 
            EnemyAIContext enemyAIContext = hitCollider.gameObject.transform.GetComponent<EnemyAIContext>();
            enemyAIContext.statManager.TakeDamage(attack.baseDmg, ModiferType.MELEE_RESISTANCE);
            enemyAIContext.healthUIUpdate();
        }

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
