using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-----------
// for FMOD
//-----------
using SFXPlayer = FMODUnity.RuntimeManager;
public class CreatureInteractable : InteractableBase
{
    public GameObject Creature;
    public string creatureType;

    private SFXManager SFX
    {
        get => PersistentData.Instance.SFXManager.GetComponent<SFXManager>();
    }

    private void Awake() 
    {
        showUI = true;
        removeOnInteract = true;
    }    

    public override void DoInteract()
    {
        if(Creature.GetComponent<CreatureAIContext>().isWild && !Creature.GetComponent<CreatureAIContext>().creatureFrozen)
        {
            DoInteractWild();
        } else 
        {
            DoInteractTamed();
        }
    }

    public void DoInteractWild()
    {
        var pc = PersistentData.Instance.Player.GetComponent<PlayerController>();
        pc.wildCreature = Creature;
        pc.befriendCreature();
        pc.wildCreature = null;
        SFX.PlayCreatureBefriendSFX(creatureType, transform.position);
        //showUI = false;

        gameObject.SetActive(false); //THIS IS TEMPORARY

    }

    public void DoInteractTamed()
    {
        // var context = Creature.GetComponent<CreatureAIContext>();
        // if(context.creatureStats.statManager.getStat(ModiferType.CURR_ENTHUSIASM) < 5)
        // {
        //     context.enthusiasmInteracted = true;
        //     gameObject.SetActive(false); //THIS IS TEMPORARY
        // }
        
    }
}
