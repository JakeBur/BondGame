using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SwarmAttack", menuName = "ScriptableObjects/BTEnemySubtrees/Swarm/Attack")]
public class SwarmAttack : BTEnemySubtree
{
    public override BTSelector BuildSelectorSubtree(EnemyAIContext context) 
    {
        List<BTNode> attackList = new List<BTNode>();
        EActionApproachPlayer approachPlayer = new EActionApproachPlayer("Approach Player", context);
        BTInverter inverter = new BTInverter("Inverter", approachPlayer);
        EActionSwarmAttackPlayer attackPlayer = new EActionSwarmAttackPlayer("Attack player", context);
        attackList.Add(inverter);
        attackList.Add(attackPlayer);
        BTSelector attackSelector = new BTSelector("Attack Selector", attackList);

        return attackSelector;
    }
}
