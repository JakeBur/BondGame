﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    //------------------
    // Player Movement
    //------------------
    [Header("Player Movement")]
    [FMODUnity.EventRef] public string PlayerWalkGrassSFX;
    [FMODUnity.EventRef] public string PlayerRollGrassInitialSFX;
    [FMODUnity.EventRef] public string PlayerRollGrassSecondarySFX;

    //-----------------------
    // Player Attacks Sword
    //-----------------------
    [Header("Player Attacks Sword")]
    [FMODUnity.EventRef] public string PlayerSwordSwingSFX;
    [FMODUnity.EventRef] public string PlayerSwordImpactSFX;
    [FMODUnity.EventRef] public string PlayerSwordCritSFX;

    //-----------------
    // Player Damaged
    //-----------------
    [Header("Player Damaged")]
    [FMODUnity.EventRef] public string PlayerDamagedDonutSFX;

    //----------------------------
    // Creature + Enemy Movement
    //----------------------------
    [Header("Creature + Enemy Movement")]
    [FMODUnity.EventRef] public string Misc3DWalkGrassSFX;

    //-----------
    // Fragaria
    //-----------
    [Header("Fragaria")]
    [FMODUnity.EventRef] public string FragariaSunbeamSFX;
    [FMODUnity.EventRef] public string FragariaSporeTossSFX;

    //----------------
    // General Enemy
    //----------------
    [Header("General Enemy")]
    [FMODUnity.EventRef] public string ArenaSpawnSFX;
    [FMODUnity.EventRef] public string EnemyDeathSFX;

    //----------------------
    // Donut (Melee Enemy)
    //----------------------
    [Header("Donut (Melee Enemy)")]
    [FMODUnity.EventRef] public string DonutSpawnSFX;
    [FMODUnity.EventRef] public string DonutSwipeSFX;

    //---------
    // UI SFX
    //---------
    [Header("UI SFX")]
    [FMODUnity.EventRef] public string MenuOpenSFX;
    [FMODUnity.EventRef] public string ButtonClickSFX;
    [FMODUnity.EventRef] public string CreatureSwapSFX;
}