using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BefriendCreatureUI : MonoBehaviour
{


    [Header("UI Assets")]
    public GameObject BefriendCreatureUIPanel;

    public Image newCreaturePortrait;
    public Image newCreatureAbility1;
    public Image newCreatureAbility2;
    public TextMeshProUGUI newCreatureName;

    public Image old1CreaturePortrait;
    public Image old1CreatureAbility1;
    public Image old1CreatureAbility2;
    public TextMeshProUGUI old1CreatureName;
    
    public Image old2CreaturePortrait;
    public Image old2CreatureAbility1;
    public Image old2CreatureAbility2;
    public TextMeshProUGUI old2CreatureName;

    private PlayerController pc;
    private CreatureAIContext newCreatureContext;
    private string creatureType;

    public void SwapCreatureOne()
    {
        PersistentData.Instance.playerController.swapCreatureOne(newCreatureContext, creatureType);
        hideUI();
    }

    public void SwapCreatureTwo()
    {
        PersistentData.Instance.playerController.swapCreatureTwo(newCreatureContext, creatureType);
        hideUI();
    }

    public void updateBefriendUI(CreatureAIContext _newCreatureContext, string _creatureType)
    {
        pc = PersistentData.Instance.playerController;
        newCreatureContext = _newCreatureContext;
        creatureType = _creatureType;
        //new creature
        newCreatureName.text = newCreatureContext.name;
        newCreaturePortrait.sprite = newCreatureContext.icon;
        newCreatureAbility1.sprite = newCreatureContext.ability1Icon;
        newCreatureAbility2.sprite = newCreatureContext.ability2Icon;
        //old creature 1
        old1CreatureName.text = pc.currCreatureContext.name;
        old1CreaturePortrait.sprite = pc.currCreatureContext.icon;
        old1CreatureAbility1.sprite = pc.currCreatureContext.ability1Icon;
        old1CreatureAbility2.sprite = pc.currCreatureContext.ability2Icon;
        //old creature 2
        CreatureAIContext old2CreatureContext = pc.swapCreature.GetComponent<CreatureAIContext>();
        old2CreatureName.text = old2CreatureContext.name;
        old2CreaturePortrait.sprite = old2CreatureContext.icon;
        old2CreatureAbility1.sprite = old2CreatureContext.ability1Icon;
        old2CreatureAbility2.sprite = old2CreatureContext.ability2Icon;


    }

    public void showUI(CreatureAIContext _context, string creatureType)
    {
        updateBefriendUI(_context, creatureType);
        BefriendCreatureUIPanel.SetActive(true);
    }

    public void hideUI()
    {
        BefriendCreatureUIPanel.SetActive(false);
    }
    

}
