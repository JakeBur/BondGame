using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RabbitNothingPersonalAttack", menuName = "ScriptableObjects/BTSubtrees/Attacks/Rabbit/Nothing Personal")]
public class RabbitNothingPersonalAttack : BTSubtree
{
    public override BTSequence BuildSequenceSubtree(CreatureAIContext context) 
    {
        List<BTNode> AbilitySequenceList = new List<BTNode>();

        #region if target already exists selector
            List<BTNode> TargetExistsSelectorList = new List<BTNode>();
            CCheckIfTargetExists checkIfTargetExists = new CCheckIfTargetExists("Check if enemy already targeted", context);
            CActionFindFurthestEnemy findTargetEnemy = new CActionFindFurthestEnemy("Find Closest Enemy in Range", context);
            TargetExistsSelectorList.Add(checkIfTargetExists);
            TargetExistsSelectorList.Add(findTargetEnemy);

            BTSelector TargetExistsSelector = new BTSelector("Target Exists Selector", TargetExistsSelectorList);
        #endregion 


        #region Approach and attack sequence
            CActionDoElusivePrepAnim doElusivePrepAnim = new CActionDoElusivePrepAnim("Prep anim", context);
            CActionAttackNothingPersonal nothingPersonal = new CActionAttackNothingPersonal("Nothing Personal", context);
        #endregion

        AbilitySequenceList.Add(TargetExistsSelector);
        AbilitySequenceList.Add(doElusivePrepAnim);
        AbilitySequenceList.Add(nothingPersonal);
        BTSequence AbilitySequence = new BTSequence("Ability Sequence", AbilitySequenceList);

        return AbilitySequence;

    }
}
