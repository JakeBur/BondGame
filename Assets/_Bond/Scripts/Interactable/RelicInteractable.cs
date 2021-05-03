using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RelicInteractable : InteractableBase
{
    public RelicStats relicStats;
    public SpriteRenderer spriteRenderer;
    public int cost = 1;

    private void Awake() 
    {
        showUI = true;
        removeOnInteract = false;
    }  

    public override void DoInteract()
    {
        PlayerController pc = PersistentData.Instance.Player.GetComponent<PlayerController>();
        if(PersistentData.Instance.Player.GetComponent<PlayerController>().goldCount >= cost)
        {
            PersistentData.Instance.Player.GetComponent<PlayerController>().goldCount -= cost;
            removeOnInteract = true;
            ApplyModifiers();
            
            //update interactable objects list, then if we are next to any other relics, display their ui
            pc.interactableObjects.Remove(gameObject);
            if(pc.interactableObjects.Count == 0)
            {
                PersistentData.Instance.hudManager.hideIntereactPrompt();
                PersistentData.Instance.ShopRelicUI.GetComponent<ShopRelicUI>().hideUI();
            }
            else
            {
                foreach(KeyValuePair<GameObject, InteractableBase> interactObj in pc.interactableObjects)
                {
                    if(interactObj.Key.transform.tag == "Relic")
                    {
                        PersistentData.Instance.ShopRelicUI.GetComponent<ShopRelicUI>().updateUI(interactObj.Key.GetComponent<RelicInteractable>().relicStats,
                                                                                                interactObj.Key.GetComponent<RelicInteractable>().cost);
                        PersistentData.Instance.ShopRelicUI.GetComponent<ShopRelicUI>().showUI();
                        break;
                    }                
                }
            }


            Destroy(gameObject);
        }
        
    }

    public void ApplyModifiers()
    {
        var pc = PersistentData.Instance.Player.GetComponent<PlayerController>();
       
        pc.stats.AddRelic(relicStats.playerModifiers);
        if(pc.currCreature != null)
        {
            pc.currCreatureContext.creatureStats.statManager.AddRelic(relicStats.creatureModifiers);
        }

        if(pc.swapCreature != null)
        {
            pc.swapCreature.GetComponent<CreatureAIContext>().creatureStats.statManager.AddRelic(relicStats.creatureModifiers);
        }


        pc.Relics.Add(relicStats);

    }

    public void updateSprite()
    {
        spriteRenderer.sprite = relicStats.relicSprite;
    }
}
