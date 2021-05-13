using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CActionAttackSnailPower : BTLeaf
{
    private float damage;
    private Buff shield;
    private bool onRain;
    CreatureAttackUtility attack;

    public CActionAttackSnailPower(string _name, CreatureAIContext _context ) : base(_name, _context)
    {
        name = _name;
        context = _context;
    }

    protected override void OnEnter()
    {
        //Look at the player before playing animation
        context.creatureTransform.LookAt(context.player.transform.position);

        attack = (CreatureAttackUtility) context.creatureStats.abilities[context.lastTriggeredAbility];

        SnailAnimator animator = context.animator as SnailAnimator;
        if (animator == null)
        {
            Debug.LogError("animator is not Slugger animator");
        }
        animator.SnailStand();
    }

    protected override void OnExit()
    {
        
    }

    public override NodeState Evaluate() 
    {
        PlayerController pc = PersistentData.Instance.Player.GetComponent<PlayerController>();
        pc.stats.AddBuff(attack.abilityBuff);
        context.targetEnemy = null;
        context.isAbilityTriggered = false;
        if( !context.animator.inAbility ) 
        { //if animation done, have to add that 
            OnParentExit();
            context.player.GetComponent<PlayerController>().PutOnCD();
            context.wentToPlayerForAbility = false;
            return NodeState.SUCCESS;
        }

        return NodeState.RUNNING;
    }
}
