using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionInteractable : InteractableBase
{
    public RelicStats relicStats;
    public SpriteRenderer spriteRenderer;
    public int cost = 1;
    public float healingAmount;
    PlayerController pc;

    private void Awake() 
    {
        showUI = true;
        removeOnInteract = false;
        pc = PersistentData.Instance.Player.GetComponent<PlayerController>();
    }  

    public override void DoInteract()
    {
        
        if(PersistentData.Instance.Player.GetComponent<PlayerController>().goldCount >= cost)
        {
            PersistentData.Instance.Player.GetComponent<PlayerController>().goldCount -= cost;
            removeOnInteract = true;
            ApplyModifiers();
            
            //update interactable objects list, then if we are next to any other relics, display their ui
            pc.interactableObjects.Remove(gameObject);
            if(pc.interactableObjects.Count == 0)
            {
                PersistentData.Instance.hudManager.HideIntereactPrompt();
                PersistentData.Instance.ShopRelicUI.GetComponent<ShopRelicUI>().hideUI();
            }
            
            Destroy(gameObject);
        }
        
    }

    public void ApplyModifiers()
    {
        pc.stats.setStat(ModiferType.CURR_HEALTH, pc.stats.getStat(ModiferType.CURR_HEALTH) + healingAmount);
        if(pc.stats.getStat(ModiferType.CURR_HEALTH) > pc.stats.getStat(ModiferType.MAX_HEALTH))
        {
            pc.stats.setStat(ModiferType.CURR_HEALTH, pc.stats.getStat(ModiferType.MAX_HEALTH));
        }
    }

    // public void updateSprite()
    // {
    //     spriteRenderer.sprite = relicStats.relicSprite;
    // }
}
