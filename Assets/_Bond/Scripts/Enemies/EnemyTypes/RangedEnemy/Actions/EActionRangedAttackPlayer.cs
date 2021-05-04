using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EActionRangedAttackPlayer : BTLeaf
{
    public EActionRangedAttackPlayer(string _name, EnemyAIContext _context ) : base(_name, _context)
    {
        enemyContext = _context;
        name = _name;
    }

    protected override void OnEnter()
    {
        // Debug.Log("Attacking player");
        //Play attack anim
        enemyContext.animator.Attack();
        //Spawn the ranged attack
        enemyContext.attackSpawner.GetComponent<EnemyRangedAttackSpawner>().SpawnProjectile(enemyContext.player.gameObject, 10, enemyContext.statManager.getStat(ModiferType.DAMAGE), false);
    }

    protected override void OnExit()
    {
        enemyContext.attackCD = enemyContext.EncounterManager.currEnemyCount;
        enemyContext.EncounterManager.numberOfCurrRangedAttackers--;
        enemyContext.attacking = false;
    }

    public override NodeState Evaluate() 
    {
        // Debug.Log("ATTACKING");
        if(enemyContext.animator.inAttack)
        {
            return NodeState.RUNNING;
        } 
        OnParentExit();
        return NodeState.SUCCESS;
    }
}
