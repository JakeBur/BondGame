using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EActionRetreat : BTLeaf
{
    Vector3 retreatPoint;
    bool goToFarthestPoint;
    public EActionRetreat(string _name, EnemyAIContext _context ) : base(_name, _context)
    {
        enemyContext = _context;
        name = _name;
    }

    protected override void OnEnter()
    {
        goToFarthestPoint = false;

    }

    protected override void OnExit()
    {
        enemyContext.agent.ResetPath();
    }

    public override NodeState Evaluate() 
    {
        // Debug.Log("RETREATING");
        float distToPlayer = Vector3.Distance(enemyContext.enemyTransform.position, enemyContext.player.transform.position);
        if(distToPlayer < enemyContext.retreatDist)
        {
            retreatPoint = (enemyContext.enemyTransform.position - enemyContext.player.transform.position).normalized * 5;
            retreatPoint += enemyContext.enemyTransform.position;

            if(Vector3.Distance(retreatPoint, enemyContext.EncounterManager.transform.position) < enemyContext.EncounterManager.arenaRadius && goToFarthestPoint == false)
            {

                enemyContext.agent.SetDestination(retreatPoint);
            }
            else 
            {
                goToFarthestPoint = true;
                enemyContext.agent.SetDestination(enemyContext.EncounterManager.farthestPointFromPlayer);
            }

            return NodeState.RUNNING;
        }
        
        OnParentExit();
        return NodeState.FAILURE;
    }
}
