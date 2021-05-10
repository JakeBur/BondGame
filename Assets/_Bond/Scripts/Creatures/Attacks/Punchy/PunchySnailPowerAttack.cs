using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PunchySnailPowerAttack", menuName = "ScriptableObjects/BTSubtrees/Attacks/Punchy/Snail Power")]
public class PunchySnailPowerAttack : BTSubtree
{
    public override BTSequence BuildSequenceSubtree(CreatureAIContext context) 
    {
        List<BTNode> AbilitySequenceList = new List<BTNode>();

        CActionFollowPlayer followPlayerAction = new CActionFollowPlayer("", context);
        CActionAttackSnailPower snailPowerBuff = new CActionAttackSnailPower("", context);
        AbilitySequenceList.Add(followPlayerAction);
        AbilitySequenceList.Add(snailPowerBuff);
        BTSequence AbilitySequence = new BTSequence("Ability Sequence", AbilitySequenceList);

        return AbilitySequence;

    }
}