using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatUIFunctions : MonoBehaviour
{

    PlayerController player => PersistentData.Instance.Player.GetComponent<PlayerController>();
    LevelUpSystem levelSys => PersistentData.Instance.Player.GetComponent<LevelUpSystem>();
    public StatManager stats => PersistentData.Instance.Player.GetComponent<StatManager>();

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
        if(i == 1)
        {
            creature1TextLists[0].SetText("0");
            creature1TextLists[1].SetText("0");
            creature1TextLists[2].SetText("0");
            creature1TextLists[3].SetText("0");
            creature1TextLists[4].SetText("0");

            //creature1TextLists[5].SetText("0");//behavior
            //creature1TextLists[6].SetText("0");//name
        }
        else if (i == 2)
        {
            creature2TextLists[0].SetText("0");
            creature2TextLists[1].SetText("0");
            creature2TextLists[2].SetText("0");
            creature2TextLists[3].SetText("0");
            creature2TextLists[4].SetText("0");

            //creature2TextLists[5].SetText("0");//behavior
            //creature2TextLists[6].SetText("0");//name
        }
        
    }
}
