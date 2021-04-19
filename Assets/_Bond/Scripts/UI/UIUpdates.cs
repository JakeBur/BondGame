//Jameson Danning
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class UIUpdates : MonoBehaviour
{
    [Header("Health")]
    public Slider slider;
    public TextMeshProUGUI maxHealthUI;
    public TextMeshProUGUI currHealthUI;
    public TextMeshProUGUI interactPrompt;

    public TextMeshProUGUI gold;
    public Image goldIcon;

    public Image xpbar;


    [Header("Creature Icons")]
    public Image currCreatureIcon;
    public Image swapCreatureIcon;   
    public Sprite noCreatureIcon;
    public TextMeshProUGUI currCreatureName;
    public TextMeshProUGUI swapCreatureName;

    [Header("Ability Icons")]
    public CanvasGroup abilityGroup;
    public Image ability1Icon;
    public Image ability2Icon;
    public Image ability1BG;
    public Image ability2BG;
    public TextMeshProUGUI ability1Description;
    public TextMeshProUGUI ability2Description;

    public int abilityId1 = 0;
    public int abilityId2 = 5;

    

    [Header("Dialogue")]
    public GameObject CharacterDialogCanvas;
    public TextMeshProUGUI CharacterDialogText;
    public GameObject EnviornmentDialogCanvas;
    public TextMeshProUGUI EnviornmentDialogText;

    public Slider enthusiasmSlider;

    public CooldownSystem cd;

    private StatManager stats => PersistentData.Instance.Player.GetComponent<StatManager>();
    private PlayerController player => PersistentData.Instance.Player.GetComponent<PlayerController>();

    private Color opaque = new Color (255,255,255,1);
    private Color transparent = new Color (255,255,255,0.5f);

    private bool hasCD = false;

    public Image hurtFeedback;

//*****************End of variable declarations**********************//


    // Start is called before the first frame update
    void Start()
    {
        UpdateCreatureUI();
        //HurtFeedback(1.0f);
        HurtFeedback(0f, 0.0f);
        ability1BG.color = transparent;
        ability2BG.color = transparent;

        abilityId1 = 0;
        abilityId2 = 1;
    }


    
    private void FixedUpdate() 
    {
        //Probably change this to only get called on health changes for efficiency
        slider.value = (stats.getStat(ModiferType.CURR_HEALTH) / stats.getStat(ModiferType.MAX_HEALTH)) * 100;

        currHealthUI.SetText((Mathf.Round(stats.getStat(ModiferType.CURR_HEALTH))).ToString());
        maxHealthUI.SetText("/ " + stats.getStat(ModiferType.MAX_HEALTH).ToString());
        gold.SetText(player.goldCount.ToString());
        XpGain();


       
        //In progress, check if any of your curr creatures abilties have active cooldowns
        CooldownUpdate(); 

        
    }

	

    
    //updates both creature icon and the respective ability icons
    public void UpdateCreatureUI()
    {
         if(player.currCreatureContext != null)
        {
            enthusiasmSlider.enabled = true;
            //updateEnthusiasm();
            currCreatureIcon.sprite = player.currCreatureContext.icon;
            

            UpdateAbilityUI();

            currCreatureName.SetText(player.currCreatureContext.creatureStats.name);

            cd = player.currCreatureContext.cooldownSystem;//assign cooldown system

            if(player.swapCreature != null) // player has the swap creature
            {
                swapCreatureIcon.sprite = player.swapCreature.GetComponent<CreatureAIContext>().icon;
                swapCreatureName.SetText(player.currCreatureContext.creatureStats.name);
            }

        }
        else //Player has no creatures equipped
        {
            enthusiasmSlider.enabled = false;
            currCreatureIcon.sprite = noCreatureIcon;
            swapCreatureIcon.sprite = noCreatureIcon;

            ability1Description.SetText("");
            ability2Description.SetText("");

            currCreatureName.SetText("No creature");
            swapCreatureName.SetText("No creature");
            //abilityGroup.alpha = 0;

        }
    }



    public void UpdateAbilityUI()
    {
        var creatureStats = player.currCreatureContext.creatureStats;

        ability1Icon.sprite = creatureStats.abilities[0].abilityIcon;
        ability1BG.sprite =  creatureStats.abilities[0].abilityIcon;

        ability2Icon.sprite = creatureStats.abilities[1].abilityIcon;
        ability2BG.sprite = creatureStats.abilities[1].abilityIcon;
        
        

        ability1Description.SetText(creatureStats.abilities[0].abilityDescription);
        ability2Description.SetText(creatureStats.abilities[1].abilityDescription);
    }

   
    public void CooldownUpdate()
    {
        //Debug.Log("cd update");
       
        //called every tick while cooldown is active
        //get specific creatures cooldown
        if(player.cooldownSystem.GetRemainingDuration(abilityId1) != 0)
        {
            ability1Icon.fillAmount = (player.cooldownSystem.GetTotalDuration(abilityId1) - player.cooldownSystem.GetRemainingDuration(abilityId1) / player.cooldownSystem.GetTotalDuration(abilityId1))- 1;

        }
        else
        {
            ability1Icon.fillAmount = 1;
        }

        if(player.cooldownSystem.GetRemainingDuration(abilityId2) != 0)
        {
            ability2Icon.fillAmount = (player.cooldownSystem.GetTotalDuration(abilityId2) - player.cooldownSystem.GetRemainingDuration(abilityId2) / player.cooldownSystem.GetTotalDuration(abilityId2)) - 1;
        }
        else
        {
            ability2Icon.fillAmount = 1;
        }

        // else
        // {
        //     hasCD = false;
        // }
        
        // ability1Icon.fillAmount += (1.0f / 7f) * Time.deltaTime;
        // ability2Icon.fillAmount += (1.0f / 7f) * Time.deltaTime;
        
    }

    public void OnAbilityFail()
    {
        Debug.Log("OUT OF COMBAT ABILITY NO GO");
    }





    
    public void showInteractPrompt()
    {
        interactPrompt.enabled = true;
    }

    public void hideIntereactPrompt()
    {
        interactPrompt.enabled = false;
    }






    public void ShowCharacterDialogue()
    {
        CharacterDialogCanvas.SetActive(true);
    }

    public void HideCharacterDialogue()
    {
        CharacterDialogCanvas.SetActive(false);
    }






    public void HurtFeedback(float amount, float time)
    {
        //Debug.Log("hurt");
        //hurtFeedback.color = opaque;
        hurtFeedback.CrossFadeAlpha(amount, time, false);       
    }

    public void XpGain()
    {
        xpbar.fillAmount = player.GetComponent<LevelUpSystem>().PercentToNextLevel();
    }


}
