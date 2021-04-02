using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EActionRetreat : BTLeaf
{
    public EActionRetreat(string _name, EnemyAIContext _context ) : base(_name, _context)
    {
        enemyContext = _context;
        name = _name;
    }

    protected override void OnEnter()
    {
     

    }

    protected override void OnExit()
    {
        enemyContext.agent.ResetPath();
    }

    public override NodeState Evaluate() 
    {
        Debug.Log("RETREATING");
        if(Vector3.Distance(enemyContext.enemyTransform.position, enemyContext.player.transform.position) < enemyContext.retreatDist)
        {
            enemyContext.agent.SetDestination(enemyContext.EncounterManager.farthestPointFromPlayer);
            return NodeState.RUNNING;
        }
        
        OnParentExit();
        return NodeState.FAILURE;
    }
}
