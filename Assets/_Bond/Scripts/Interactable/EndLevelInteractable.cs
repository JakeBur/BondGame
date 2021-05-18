using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelInteractable : InteractableBase
{

    public EncounterManager encounterManager;


    private void Awake() 
    {
        showUI = false;
        removeOnInteract = true;
    }

    private void Update() 
    {
        if(encounterManager.encounterFinished)
        {
            showUI = true;
        }
    }

    public override void DoInteract()
    {
        // Debug.Log("End Level Interact");
        // if(!encounterManager.encounterFinished && !encounterManager.encounterTriggered)
        // {
        //     encounterManager.startEncounter();
        //     removeOnInteract = true;
        // } 
        if(encounterManager.encounterFinished)
        {
            if(SceneManager.GetActiveScene().name == "Tutorial")
            {
                PersistentData.Instance.LoadScene(1);
            } else if(PersistentData.Instance.currRunLevel < 3)
            {
                Debug.Log("try load");
                PersistentData.Instance.LoadScene(2);
                PersistentData.Instance.currRunLevel++;
            } else {
                PersistentData.Instance.LoadScene(1);
            }
        }

    }
}
