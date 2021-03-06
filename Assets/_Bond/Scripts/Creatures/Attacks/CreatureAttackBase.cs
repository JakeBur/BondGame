//Author : Colin
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureAttackBase : ScriptableObject, HasCooldown
{
    public int id;
    public float cooldownDuration;

    public int Id => id;
    public float CooldownDuration => cooldownDuration;

    public Sprite abilityIcon;

    public BTSubtree abilityBehavior;

    public Buff abilityBuff;

    public string abilityDescription;
    
}



