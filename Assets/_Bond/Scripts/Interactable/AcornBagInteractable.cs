using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcornBagInteractable : InteractableBase
{
    public RelicStats relicStats;
    public SpriteRenderer spriteRenderer;
    public int cost = 0;
    public int acornAmount;
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
        acornAmount = Random.Range(5, 20);
        pc.goldCount += acornAmount;
    }
}
