using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AquaphimKnockbackWing", menuName = "ScriptableObjects/BTSubtrees/Attacks/Aquaphim/Knockback Wing")]
public class AquaphimKnockbackWingAttack : BTSubtree
{
    public override BTSequence BuildSequenceSubtree(CreatureAIContext context) 
    {
        List<BTNode> AbilitySequenceList = new List<BTNode>();

        CActionKnockbackWing knockbackWing = new CActionKnockbackWing("", context);
        AbilitySequenceList.Add(knockbackWing);
        BTSequence AbilitySequence = new BTSequence("Ability Sequence", AbilitySequenceList);

        return AbilitySequence;

    }
}