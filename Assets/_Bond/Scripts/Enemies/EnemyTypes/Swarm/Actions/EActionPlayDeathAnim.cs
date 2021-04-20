// Jake
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EActionPlayDeathAnim : BTLeaf
{
    public EActionPlayDeathAnim(string _name, EnemyAIContext _context ) : base(_name, _context)
    {
        name = _name;
        enemyContext = _context;
    }

    protected override void OnEnter()
    {
        //Play death anim
        enemyContext.animator.Death();
    }

    protected override void OnExit()
    {
        
    }

    public override NodeState Evaluate() 
    {
        if( enemyContext.animator.inDeath )
        {
            return NodeState.RUNNING;
        }
        //Spawn gold
        enemyContext.dropGold();

        //tell encounter that i died
        enemyContext.EncounterManager.enemyKilled();
        if(enemyContext.attacking)
        {
            switch(enemyContext.enemyType)
            {
                case "MeleeEnemy":
                    enemyContext.EncounterManager.numberOfCurrMeleeAttackers--;
                    break;
                case "RangedEnemy":
                    enemyContext.EncounterManager.numberOfCurrRangedAttackers--;
                    break;
                default:
                    Debug.Log("Couldn't find enemy type in death anim");
                    enemyContext.EncounterManager.numberOfCurrMeleeAttackers--;
                    break;
            }
        }

        //Destroy enemy
        enemyContext.DestroyEnemy();
        return NodeState.SUCCESS;
    }
}
