using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "MeleeAttack", menuName = "ScriptableObjects/BTEnemySubtrees/Melee/Attack")]
public class SmarterMeleeAttack : BTEnemySubtree
{
    public override BTSelector BuildSelectorSubtree(EnemyAIContext context) 
    {

        #region Strafe Attack Selector
            List<BTNode> strafeAttackSelectorList = new List<BTNode>();

            #region check if attack on cd sequence
                List<BTNode> ifOnCDSequenceList = new List<BTNode>();
                ECheckIfMeleeOnCD checkIfMeleeOnCD = new ECheckIfMeleeOnCD("check if on cd", context);
                #region strafe/retreat selector
                    List<BTNode> strafeRetreatSelectorList = new List<BTNode>();
                    EActionRetreat retreat = new EActionRetreat("retreat", context);
                    EActionStrafe strafe = new EActionStrafe("strafe", context);
                    strafeRetreatSelectorList.Add(retreat);
                    strafeRetreatSelectorList.Add(strafe);
                    BTSelector strafeRetreatSelector = new BTSelector("Strafe Retreat Selector", strafeRetreatSelectorList);
                #endregion

                ifOnCDSequenceList.Add(checkIfMeleeOnCD);
                ifOnCDSequenceList.Add(strafeRetreatSelector);
                BTSequence ifOnCDSequence = new BTSequence("if on cd sequence", ifOnCDSequenceList);
            #endregion


            #region Attack Selector
                List<BTNode> attackList = new List<BTNode>();
                EActionApproachPlayer approachPlayer = new EActionApproachPlayer("Approach Player", context);
                BTInverter inverter = new BTInverter("Inverter", approachPlayer);
                EActionMeleeAttackPlayer attackPlayer = new EActionMeleeAttackPlayer("Attack player", context);
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
