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

    [Header("Creature vars")]
    StatManager creatureStats;// => playerController.currCreatureContext.GetComponent<StatManager>();
    public List<TextMeshProUGUI> creatureTextLists; //Order: Name[0], abil1[1], abil2[2]

    public Image ability1;
    public Image ability2;

    public Image button1;
    public Image button2;

    public Sprite noAbility;
    public Sprite noCreature;

    
    [Header("Player vars")]
    public List<TextMeshProUGUI> playerTextLists; //ORDER: life[0], power[1], crit[2], bond[3],  level[4], xp to next[5]
    public List<Button> levelUpButtons;

    public Image xpBar;
    public TextMeshProUGUI pointsAvail;

    public Sprite buttonAvail;
    public Sprite buttonNotAvail;

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
        playerTextLists[2].SetText(stats.getStat(ModiferType.CRIT_CHANCE).ToString() + "%");
        playerTextLists[3].SetText("0"); //bond aka creature damage
        
//        playerTextLists[4].SetText(stats.getStat(ModiferType.MOVESPEED).ToString());

        playerTextLists[4].SetText(levelSys.level.ToString());//Get Level
        playerTextLists[5].SetText(levelSys.GetNextXpForLevel().ToString() + " left");//Get remaining xp

        xpBar.fillAmount = levelSys.PercentToNextLevel();
        pointsAvail.SetText("Points Available: " + levelSys.upgradePoints);




    }


    public void UpdateCreatureStats(int i)
    {
        ActiveCreatureData context = null;
        if(playerController.currCreatureContext != null)
        {         
            if(playerController.swapCreature != null) //Do this first so 2nd creature buttons shows without click
            {
                button2.sprite = playerController.swapCreature.GetComponent<CreatureAIContext>().icon;
            }   

            if(i == 1) //first creature / button
            {
                creatureStats = playerController.currCreature.GetComponent<CreatureAIContext>().creatureStats.statManager;
                context = playerController.currCreatureContext.creatureStats;  

                button1.sprite = playerController.currCreatureContext.icon;
            }
            else //second creature
            {   
                if(playerController.swapCreature != null) 
                {
                    creatureStats = playerController.swapCreature.GetComponent<CreatureAIContext>().creatureStats.statManager;
                    context = playerController.swapCreature.GetComponent<CreatureAIContext>().creatureStats;

                    //button2.sprite = playerController.swapCreature.GetComponent<CreatureAIContext>().icon;
                }                   
            }

            creatureTextLists[0].SetText(creatureStats.name);
            creatureTextLists[1].SetText(context.abilities[0].abilityDescription);
            creatureTextLists[2].SetText(context.abilities[1].abilityDescription);

            ability1.sprite = context.abilities[0].abilityIcon;
            ability2.sprite = context.abilities[1].abilityIcon;

            

            
        }
        else //no creatures
        {
            creatureTextLists[0].SetText("No Creature");//name
            creatureTextLists[1].SetText("No Ability");
            creatureTextLists[2].SetText("No Ability");

            ability1.sprite = noAbility;
            ability2.sprite = noAbility;
            button1.sprite = noCreature;
            button2.sprite = noCreature;
        }        
    }



    public void ButtonsAvailable()
    {
        if(levelSys.upgradePoints > 0)
        {
            if(levelSys.healthPoints <= levelSys.healthPointsMax)
            {
                levelUpButtons[0].interactable = true;
                levelUpButtons[0].GetComponent<Image>().sprite = buttonAvail;
            }
            if(levelSys.damagePoints <= levelSys.damagePointsMax)
            {
                levelUpButtons[1].interactable = true;
                levelUpButtons[1].GetComponent<Image>().sprite = buttonAvail;
            }
            if(levelSys.critPoints <= levelSys.critPointsMax)
            {
                levelUpButtons[2].interactable = true;
                levelUpButtons[2].GetComponent<Image>().sprite = buttonAvail;
            }
            if(levelSys.bondPoints <= levelSys.bondPointsMax)
            {
                levelUpButtons[3].interactable = true;
                levelUpButtons[3].GetComponent<Image>().sprite = buttonAvail;
            }
        }
        else
        {
            foreach (Button b in levelUpButtons)
            {
                b.interactable = false;
                b.GetComponent<Image>().sprite = buttonNotAvail;
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
