using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EActionStrafe : BTLeaf
{
    int randomDir = 0;
    float movespeed = 5;
    Vector3 movementVec;


    public EActionStrafe(string _name, EnemyAIContext _context ) : base(_name, _context)
    {
        enemyContext = _context;
        name = _name;

    }

    protected override void OnEnter()
    {
        randomDir = Random.Range(0,2);
        
    }

    protected override void OnExit()
    {
        enemyContext.agent.speed = enemyContext.statManager.stats[ModiferType.MOVESPEED].modifiedValue;
    }

    public override NodeState Evaluate() 
    {
        Debug.Log("STRAFING");
        enemyContext.agent.speed = enemyContext.statManager.stats[ModiferType.MOVESPEED].modifiedValue / 2;
        if(Vector3.Distance(enemyContext.transform.position, enemyContext.player.transform.position) > enemyContext.retreatDist)
        {
            if(randomDir == 0)
            { //left
        
                movementVec = Vector3.Cross((enemyContext.enemyTransform.position - enemyContext.player.transform.position), Vector3.up);
                
            } else 
            { //right
                movementVec =  Vector3.Cross((enemyContext.enemyTransform.position - enemyContext.player.transform.position), Vector3.down);
               
            }
        
            enemyContext.agent.SetDestination(enemyContext.enemyTransform.position + movementVec);
            enemyContext.enemyTransform.transform.LookAt(new Vector3(enemyContext.player.transform.position.x,
                                                                    0, 
                                                                    enemyContext.player.transform.position.z),
                                                        Vector3.up);
        }
        OnParentExit();
        return NodeState.SUCCESS;
    }
}
