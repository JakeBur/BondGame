using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "RangedAttack", menuName = "ScriptableObjects/BTEnemySubtrees/Ranged/Attack")]
public class RangedAttack : BTEnemySubtree
{
    public override BTSelector BuildSelectorSubtree(EnemyAIContext context) 
    {
        #region Strafe Attack Selector
            List<BTNode> strafeAttackSelectorList = new List<BTNode>();

            #region check if attack on cd sequence
                List<BTNode> ifOnCDSequenceList = new List<BTNode>();
                ECheckIfRangedOnCD checkIfRangedOnCD = new ECheckIfRangedOnCD("check if on cd", context);
                #region strafe/retreat selector
                    List<BTNode> strafeRetreatSelectorList = new List<BTNode>();
                    EActionRetreat retreat = new EActionRetreat("retreat", context);
                    EActionStrafe strafe = new EActionStrafe("strafe", context);
                    strafeRetreatSelectorList.Add(retreat);
                    strafeRetreatSelectorList.Add(strafe);
                    BTSelector strafeRetreatSelector = new BTSelector("Strafe Retreat Selector", strafeRetreatSelectorList);
                #endregion

                ifOnCDSequenceList.Add(checkIfRangedOnCD);
                ifOnCDSequenceList.Add(strafeRetreatSelector);
                BTSequence ifOnCDSequence = new BTSequence("if on cd sequence", ifOnCDSequenceList);
            #endregion


            #region Attack Selector
                List<BTNode> attackList = new List<BTNode>();
                EActionRangedApproachPlayer approachPlayer = new EActionRangedApproachPlayer("Approach Player", context);
                BTInverter inverter = new BTInverter("Inverter", approachPlayer);
                EActionRangedAttackPlayer attackPlayer = new EActionRangedAttackPlayer("Attack player", context);
                attackList.Add(inverter);
                attackList.Add(attackPlayer);
                BTSelector attackSelector = new BTSelector("Attack Selector", attackList);
            #endregion

            strafeAttackSelectorList.Add(ifOnCDSequence);
            strafeAttackSelectorList.Add(attackSelector);
            BTSelector strafeAttackSelector = new BTSelector("Strafe Attack Selector", strafeAttackSelectorList);
        #endregion
        return strafeAttackSelector;
    }
}
