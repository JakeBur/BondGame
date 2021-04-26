using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AquaphimWaterShieldAttack", menuName = "ScriptableObjects/BTSubtrees/Attacks/Aquaphim/Water Shield")]
public class AquaphimWaterShieldAttack : BTSubtree
{
    public override BTSequence BuildSequenceSubtree(CreatureAIContext context) 
    {
        List<BTNode> AbilitySequenceList = new List<BTNode>();

        CActionFollowPlayer followPlayerAction = new CActionFollowPlayer("", context);
        CActionAttackWaterShield shieldBuff = new CActionAttackWaterShield("", context);
        AbilitySequenceList.Add(followPlayerAction);
        AbilitySequenceList.Add(shieldBuff);
        BTSequence AbilitySequence = new BTSequence("Ability Sequence", AbilitySequenceList);

        return AbilitySequence;

    }
}