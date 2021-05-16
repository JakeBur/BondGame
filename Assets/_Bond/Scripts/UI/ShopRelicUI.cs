using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopRelicUI : MonoBehaviour
{
    public GameObject ui;
    
    public TextMeshProUGUI relicName;
    public Image relicSprite;
    public Image moneySprite;
    public TextMeshProUGUI flavorText;
    public TextMeshProUGUI statsText;
    public TextMeshProUGUI costText;


    public void showUI()
    {
        ui.SetActive(true);
    }

    public void hideUI()
    {
        ui.SetActive(false);
    }

    public void updateUI(RelicStats _stats, int _cost)
    {
        relicName.text = _stats.relicName;
        relicSprite.sprite = _stats.relicSprite;
        flavorText.text  = _stats.relicInfo;
        if(_cost <= 0)
        {
            costText.enabled = false;
            moneySprite.enabled = false;
        } else 
        {
            costText.enabled = true;
            moneySprite.enabled = true;
            costText.text = _cost.ToString();
        }
        statsText.text = "";
        foreach(Modifier mod in _stats.playerModifiers)
        {
            if(mod.modifierIdentifier != ""){
                statsText.text += "-";
                statsText.text += mod.modifierIdentifier;
                statsText.text += "\n";
            }
            
        }
        foreach(Modifier mod in _stats.creatureModifiers)
        {
            if(mod.modifierIdentifier != ""){
                statsText.text += "-";
                statsText.text += mod.modifierIdentifier;
                statsText.text += "\n";
            }
        }

    }

}
