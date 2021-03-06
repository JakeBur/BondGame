//Author : Colin
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Creature Melee Attack", menuName = "ScriptableObjects/CreatureAttacks/MeleeAttack", order = 2)]
public class CreatureAttackMelee : CreatureAttackBase, HasCooldown
{
    public float maxDistanceToEnemy;
    public float moveSpeed;
    public Animation anims;
    public string animTrigger;
    public float baseDmg;
    public float hitRadius;
    private void Awake() 
    {
        cooldownDuration = 2f;
    }
    new public int Id => id;
    new public float CooldownDuration => cooldownDuration;
}
