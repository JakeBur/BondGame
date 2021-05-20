using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-----------
// for FMOD
//-----------
using SFXPlayer = FMODUnity.RuntimeManager;

public class RelicInteractable : InteractableBase
{
    public RelicStats relicStats;
    public SpriteRenderer spriteRenderer;
    public int cost = 1;

    //-----------
    // for FMOD
    //-----------
    private SFXManager SFX
    {
        get => PersistentData.Instance.SFXManager.GetComponent<SFXManager>();
    }

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
                PersistentData.Instance.hudManager.HideIntereactPrompt();
                PersistentData.Instance.ShopRelicUI.GetComponent<ShopRelicUI>().hideUI();
            }
           

            SFXPlayer.PlayOneShot(SFX.RelicPickupSFX);
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
