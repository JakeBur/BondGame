using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EActionPlayAwakeAnim : BTLeaf
{
    public EActionPlayAwakeAnim(string _name, EnemyAIContext _context ) : base(_name, _context)
    {
        name = _name;
        enemyContext = _context;
    }

    protected override void OnEnter()
    {
        enemyContext.animator.Spawn();
        enemyContext.attackCD = Random.Range(1, enemyContext.EncounterManager.currEnemyCount);
    }

    protected override void OnExit()
    {

    }

    public override NodeState Evaluate() 
    {
        if(enemyContext.animator.inSpawn)
        {
             return NodeState.RUNNING;
        }
        OnParentExit();
        return NodeState.SUCCESS;
    }
}
