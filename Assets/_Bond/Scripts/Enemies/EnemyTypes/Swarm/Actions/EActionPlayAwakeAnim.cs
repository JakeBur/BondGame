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

        
        //enemyContext.animator.Spawn(); //uncomment once finished
        enemyContext.attackCD = Random.Range(1, enemyContext.EncounterManager.currEnemyCount + 4);
    }

    protected override void OnExit()
    {

    }

    public override NodeState Evaluate() 
    {
        // FOR HERMAN
        // if(enemyContext.animator.inSpawn)
        // {
        //     return NodeState.RUNNING;
        // }
        OnParentExit();
        return NodeState.SUCCESS;
    }
}
