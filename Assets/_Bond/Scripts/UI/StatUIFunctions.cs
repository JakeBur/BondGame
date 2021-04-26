using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatUIFunctions : MonoBehaviour
{

    PlayerController playerController => PersistentData.Instance.Player.GetComponent<PlayerController>();
    LevelUpSystem levelSys => PersistentData.Instance.Player.GetComponent<LevelUpSystem>();

    StatManager stats => PersistentData.Instance.Player.GetComponent<StatManager>();
    StatManager creatureStats;// => playerController.currCreatureContext.GetComponent<StatManager>();

    public List<TextMeshProUGUI> playerTextLists; //ORDER: life[0], power[1], bond[2], crit[3], speed[4], level[5], xp to next[6]
    public List<TextMeshProUGUI> creature1TextLists; //Order: life[0], power[1], Dex[2], utility[4], behavior[5], name[6]
    public List<TextMeshProUGUI> creature2TextLists; //Order: life[0], power[1], Dex[2], utility[4], behavior[5], name[6]
    public List<Button> levelUpButtons;

    public Image xpBar;
    public TextMeshProUGUI pointsAvail;

    //relics
    public List<GameObject> relicCells;
    

   
    private void Start()
    { 
        UpdatePlayerStats();
        UpdateCreatureStats(1);
        ButtonsAvailable();
    }

    

    private void OnEnable() 
    {
        UpdatePlayerStats();
        UpdateCreatureStats(1);
        ButtonsAvailable();
        UpdateRelicsUI();
    }



    public void UpdatePlayerStats()
    {
        //levelSys.UpdateStats();

        
        playerTextLists[0].SetText(stats.getStat(ModiferType.MAX_HEALTH).ToString());
        playerTextLists[1].SetText(stats.getStat(ModiferType.DAMAGE).ToString());
        playerTextLists[2].SetText("0"); //bond aka creature damage
        playerTextLists[3].SetText(stats.getStat(ModiferType.CRIT_CHANCE).ToString() + "%");
        playerTextLists[4].SetText(stats.getStat(ModiferType.MOVESPEED).ToString());

        playerTextLists[5].SetText(levelSys.level.ToString());//Get Level
        playerTextLists[6].SetText(levelSys.GetNextXpForLevel().ToString() + " left");//Get remaining xp

        xpBar.fillAmount = levelSys.PercentToNextLevel();
        pointsAvail.SetText("Points Available: " + levelSys.upgradePoints);




    }


    public void UpdateCreatureStats(int i)
    {
        if(playerController.currCreatureContext != null)
        {
           
            if(i == 1)
            {
                creatureStats = playerController.currCreature.GetComponent<CreatureAIContext>().creatureStats.statManager;

                creature1TextLists[0].SetText(creatureStats.getStat(ModiferType.MAX_ENTHUSIASM).ToString());
                creature1TextLists[1].SetText(creatureStats.getStat(ModiferType.DAMAGE).ToString());
                creature1TextLists[2].SetText(creatureStats.getStat(ModiferType.CREATURE_DEXTERITY).ToString());
                creature1TextLists[3].SetText(creatureStats.getStat(ModiferType.CREATURE_UTILITY).ToString());
                creature1TextLists[4].SetText("behavior");//behavior

                creature1TextLists[5].SetText(creatureStats.name);//name
            
            }
            else if (i == 2)
            {   
                if(playerController.swapCreature != null) 
                {
                    creatureStats = playerController.swapCreature.GetComponent<CreatureAIContext>().creatureStats.statManager;

                    creature1TextLists[0].SetText(creatureStats.getStat(ModiferType.MAX_ENTHUSIASM).ToString());
                    creature1TextLists[1].SetText(creatureStats.getStat(ModiferType.DAMAGE).ToString());
                    creature1TextLists[2].SetText(creatureStats.getStat(ModiferType.CREATURE_DEXTERITY).ToString());
                    creature1TextLists[3].SetText(creatureStats.getStat(ModiferType.CREATURE_UTILITY).ToString());
                    creature1TextLists[4].SetText("behavior");//behavior

                    creature1TextLists[5].SetText(creatureStats.name);//name
                }                   
            }
        }
        else //no creatures
        {
            foreach(TextMeshProUGUI text in creature1TextLists)
            {
                text.SetText("-");
            }
        }        
    }



    public void ButtonsAvailable()
    {
        if(levelSys.upgradePoints > 0)
        {
            if(levelSys.healthPoints <= levelSys.healthPointsMax)
            {
                levelUpButtons[0].interactable = true;
            }
            if(levelSys.damagePoints <= levelSys.damagePointsMax)
            {
                levelUpButtons[1].interactable = true;
            }
            if(levelSys.bondPoints <= levelSys.bondPointsMax)
            {
                levelUpButtons[2].interactable = true;
            }
            if(levelSys.critPoints <= levelSys.critPointsMax)
            {
                levelUpButtons[3].interactable = true;
            }
        }
        else
        {
            foreach (Button b in levelUpButtons)
            {
                b.interactable = false;
            }
        }
    }


    public void UseUpgrade(int i)
    {
        levelSys.UseUpgradePoint(i);
        UpdatePlayerStats();
    }
    

    public void UpdateRelicsUI()
    {
        for(int i = 0; i < playerController.Relics.Count; i++)
        {
            if(playerController.Relics[i] != null)
            {
                relicCells[i].GetComponent<Image>().sprite = playerController.Relics[i].relicSprite;
                relicCells[i].GetComponent<Image>().color = new Color(255,255,255,1);
            }
            else
            {
                 relicCells[i].GetComponent<Image>().sprite = null;
                 relicCells[i].GetComponent<Image>().color = new Color(255,255,255,0);
            }
        }
    }
}
