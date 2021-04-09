using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EActionPlayHitstunAnim : BTLeaf
{
    float stunTimer = 0;

    public EActionPlayHitstunAnim(string _name, EnemyAIContext _context ) : base(_name, _context)
    {
        name = _name;
        enemyContext = _context;
    }

    protected override void OnEnter()
    {
        if(enemyContext.lastDamageTaken > 25)
        {
            stunTimer = 0;
            //Play hitstun anim
            enemyContext.animator.Hitstun();
        }
    }

    protected override void OnExit()
    {
        stunTimer = 0;
    }

    public override NodeState Evaluate() 
    {
        stunTimer += Time.deltaTime;
        // return NodeState.SUCCESS;
        if( stunTimer >= enemyContext.hitstunDuration )
        {
            enemyContext.tookDamage = false;
            OnParentExit();
            enemyContext.animator.HitstunDone();
            return NodeState.SUCCESS;
        }
        else 
        {
            return NodeState.RUNNING;
        }
    }
}
