﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopRelicUI : MonoBehaviour
{
    public GameObject ui;
    
    public TextMeshProUGUI relicName;
    public Image relicSprite;
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
        costText.text = _cost.ToString();
        statsText.text = "";
        foreach(Modifier mod in _stats.playerModifiers)
        {
            if(mod.modifierIdentifier != ""){
                statsText.text += mod.modifierIdentifier;
            }
            
        }
        foreach(Modifier mod in _stats.creatureModifiers)
        {
            if(mod.modifierIdentifier != ""){
                statsText.text += mod.modifierIdentifier;
            }
        }

    }

}
