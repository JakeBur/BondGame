using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AquaphimRainAttack", menuName = "ScriptableObjects/BTSubtrees/Attacks/Aquaphim/Rain")]
public class AquaphimRainAttack : BTSubtree
{
    public override BTSequence BuildSequenceSubtree(CreatureAIContext context) 
    {
        List<BTNode> AbilitySequenceList = new List<BTNode>();

        CActionAttackRain attackRain = new CActionAttackRain("", context);

        AbilitySequenceList.Add(attackRain);
        BTSequence AbilitySequence = new BTSequence("Ability Sequence", AbilitySequenceList);

        return AbilitySequence;

    }
}
