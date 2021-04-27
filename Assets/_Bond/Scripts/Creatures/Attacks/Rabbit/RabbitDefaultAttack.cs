using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DefaultAttack", menuName = "ScriptableObjects/BTSubtrees/Attacks/Rabbit/Default Attack")]
public class RabbitDefaultAttack : BTSubtree
{
    public override BTSequence BuildSequenceSubtree(CreatureAIContext context) 
    {
        List<BTNode> AbilitySequenceList = new List<BTNode>();

        #region if target already exists selector
            List<BTNode> TargetExistsSelectorList = new List<BTNode>();
            CCheckIfTargetExists checkIfTargetExists = new CCheckIfTargetExists("Check if enemy already targeted", context);
            CActionFindTargetEnemy findTargetEnemy = new CActionFindTargetEnemy("Find Closest Enemy in Range", context);
            TargetExistsSelectorList.Add(checkIfTargetExists);
            TargetExistsSelectorList.Add(findTargetEnemy);

            BTSelector TargetExistsSelector = new BTSelector("Target Exists Selector", TargetExistsSelectorList);
        #endregion 


        #region Approach and attack sequence
            List<BTNode> RangedApproachSelectorList = new List<BTNode>();
            CActionApproachForBasicRanged approachForAttack = new CActionApproachForBasicRanged("Approach for attack", context);
            BTInverter invertApproachForAttack = new BTInverter("Invert Approach for Attack", approachForAttack);
            CActionAttackThrowKnife attackRanged = new CActionAttackThrowKnife("Throw Knife Default Attack", context);
            RangedApproachSelectorList.Add(invertApproachForAttack);
            RangedApproachSelectorList.Add(attackRanged);
            
            BTSelector RangedApproachAttackSelector = new BTSelector("Water Beam Approach / Attack Sequence", RangedApproachSelectorList);
        #endregion

        AbilitySequenceList.Add(TargetExistsSelector);
        AbilitySequenceList.Add(RangedApproachAttackSelector);
        BTSequence AbilitySequence = new BTSequence("Ability Sequence", AbilitySequenceList);

        return AbilitySequence;

    }
}
