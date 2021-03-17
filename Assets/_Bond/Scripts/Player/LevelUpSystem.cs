﻿//Jameson Danning
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpSystem : MonoBehaviour
{
    private int level;

    private int maxXPTotal;
    private int currentXPTotal;

    public int xpNeededForNext;
    public int xpGainedThisLevel;

    public int upgradePoints;

    [Header("Equation Variables")]
    public float A = 16;
    public float B = 2;
    public float C = 0.2f;




    private void Start() 
    {
        level = 1;// will be adjusted for save data later on
        xpNeededForNext = GetNextXpForLevel();
        xpGainedThisLevel = 0;
        upgradePoints = 0;
    }




    public int GetNextXpForLevel()//takes current level, runs through equation to get xp needed for the next level
    {
        int xp = Mathf.FloorToInt( A + (B * (float)level) + (C * (float)level * (float)level) ); // A + Bx + Cx^2
        return xp;
    }




    public void GainXp(int xp)
    {
        xpGainedThisLevel += xp;

        if(xpGainedThisLevel >= xpNeededForNext)
        {
            LevelUp();
        }
    }




    public void LevelUp()
    {
        level++;
        upgradePoints++;

        xpGainedThisLevel = xpGainedThisLevel - xpNeededForNext; //carries over surplus
        xpNeededForNext = GetNextXpForLevel();

        if(xpGainedThisLevel >= xpNeededForNext) LevelUp(); //cases where you may level up multiple times from one source, prob never

    }




    public void UseUpgradePoint()
    {
        upgradePoints--;
    }




}
