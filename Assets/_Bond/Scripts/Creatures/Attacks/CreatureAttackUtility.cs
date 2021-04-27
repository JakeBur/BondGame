﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Creature Utility Ability", menuName = "ScriptableObjects/CreatureAttacks/UtilityAbility", order = 3)]
public class CreatureAttackUtility : CreatureAttackBase
{
    public float maxDistanceToEnemy;
    public GameObject projectile;
    public Animation anims;
    public float baseDmg;
    private void Awake() 
    {
        cooldownDuration = 2f;
    }
    
    new public int Id => id;
    
    new public float CooldownDuration => cooldownDuration;
}
