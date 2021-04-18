//Jameson Danning
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpSystem : MonoBehaviour
{
    StatManager playerStats => PersistentData.Instance.Player.GetComponent<StatManager>();
    StatUIFunctions statUI =>  PersistentData.Instance.StatUI.GetComponent<StatUIFunctions>();

    [SerializeField]
    public int level = 1;
   

    private int maxXPTotal;
    private int currentXPTotal = 0;

    public int xpNeededForNext = 0;
    public int xpGainedThisLevel = 0;

    [Header("Upgrade Points")]
    public int upgradePoints = 0;
    //how many points are in each
    public int healthPoints { get; private set; } = 0 ;
    public int damagePoints { get; private set; } = 0;
    public int bondPoints { get; private set; } = 0;
    public int critPoints { get; private set; } = 0;
    //max amount for each
    public int healthPointsMax { get; private set; } = 20;
    public int damagePointsMax { get; private set; } = 10;
    public int bondPointsMax { get; private set; } = 10;
    public int critPointsMax { get; private set; } = 10;
    


    [Header("Equation Variables")]
    public float A = 16;
    public float B = 2;
    public float C = 0.2f;



    private void Awake() 
    {
        upgradePoints = 0;
        level = 1;
        healthPoints = 0;
        damagePoints = 0;
        bondPoints = 0;
        critPoints = 0;
    }



    private void Start() 
    {
        //level = 1;// will be adjusted for save data later on
        xpNeededForNext = GetNextXpForLevel();
        xpGainedThisLevel = 0;
        //upgradePoints = 0;
        UpdateStats();

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
        statUI.ButtonsAvailable();
    }



    //Will be called from StatUI updates, and function there will determin if a stat is maxed or not
    public void UseUpgradePoint(int i)
    {
        upgradePoints--;

        switch (i)
        {
            case 1:
                healthPoints++;
                break;
            case 2:
                damagePoints++;
                break;
            case 3:
                bondPoints++;
                break;
            case 4:
                critPoints++;
                break;
        }

        UpdateStats();

        statUI.ButtonsAvailable();
  
    }



    public float PercentToNextLevel()
    {
        return (float)xpGainedThisLevel/xpNeededForNext;
    }


    public void UpdateStats()
    {  
        float healthRatio = playerStats.getStat(ModiferType.CURR_HEALTH)/playerStats.getStat(ModiferType.MAX_HEALTH);

        playerStats.setStat(ModiferType.MAX_HEALTH, 300 + (healthPoints * 5) );
        playerStats.setStat(ModiferType.CURR_HEALTH, Mathf.CeilToInt(playerStats.getStat(ModiferType.MAX_HEALTH) * healthRatio));//keeps health ratio same on level up
        playerStats.setStat(ModiferType.DAMAGE, 20 + damagePoints);
        //playerStats.setStat(ModiferType.CREATURE_POWER, playerStats.getStat(ModiferType.CREATURE_POWER) + bondPoints); //Need stat for player that mods creature, this dont work
        playerStats.setStat(ModiferType.CRIT_CHANCE, 2 + critPoints);

    }




}
