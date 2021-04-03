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

    public Image xpBar;
    
    /*
    [Header("Player Stats")]
    public TextMeshProUGUI lifeText;
    public TextMeshProUGUI powerText;
    public TextMeshProUGUI bondText;
    public TextMeshProUGUI critText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI remainingXP;
    

    [Header("Creature 1 Stats")]
    public TextMeshProUGUI creaturelifeText;
    public TextMeshProUGUI creaturePowerText;
    public TextMeshProUGUI creatureDexterityText;
    public TextMeshProUGUI creatureUtilityText;
    public TextMeshProUGUI creatureBehaviorText;


    [Header("Creature 2 Stats")]
    public TextMeshProUGUI creature2lifeText;
    public TextMeshProUGUI creature2PowerText;
    public TextMeshProUGUI creature2DexterityText;
    public TextMeshProUGUI creature2UtilityText;
    public TextMeshProUGUI creature2BehaviorText;
*/


    private void Start()
    { 
        UpdatePlayerStats();
        UpdateCreatureStats(1);
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    public void UpdatePlayerStats()
    {
        playerTextLists[0].SetText(stats.getStat(ModiferType.MAX_HEALTH).ToString());
        playerTextLists[1].SetText(stats.getStat(ModiferType.DAMAGE).ToString());
        playerTextLists[2].SetText("0"); //bond aka creature damage
        playerTextLists[3].SetText(stats.getStat(ModiferType.CRIT_CHANCE).ToString() + "%");
        playerTextLists[4].SetText(stats.getStat(ModiferType.MOVESPEED).ToString());

        playerTextLists[5].SetText(levelSys.level.ToString());//Get Level
        playerTextLists[6].SetText(levelSys.GetNextXpForLevel().ToString() + " left");//Get remaining xp

        xpBar.fillAmount = levelSys.PercentToNextLevel();


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

    private void OnEnable() 
    {
        UpdatePlayerStats();
        UpdateCreatureStats(1);
    }
    
}
