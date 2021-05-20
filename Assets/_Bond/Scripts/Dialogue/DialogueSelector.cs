using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSelector : MonoBehaviour
{
    public List<Conversation> farmCompletedRunDialogues;
    public List<Conversation> farmLostRunDialogues;
    public List<Conversation> transitionDialogues;
    public List<Conversation> beatGameDialogues;
    private DialogueManager dialogueManager;

    // Start is called before the first frame update
    void Start()
    {
        dialogueManager = GetComponent<PersistentData>().playerController.dialogueManager;
    }

    public void selectDialogue(int scene, bool lostRun)
    {
        int randomChoice = 0;
        switch(scene)
        {
            case 1: //Went back to farm
                if(lostRun)
                {
                    randomChoice = Random.Range(0, farmLostRunDialogues.Count);
                    dialogueManager.dialogue = farmLostRunDialogues[randomChoice];
                    dialogueManager.StartDialogue();
                } else 
                {
                    if(PlayerPrefs.GetInt("completedRuns") >= 3)
                    {
                        randomChoice = Random.Range(0, beatGameDialogues.Count);
                        dialogueManager.dialogue = beatGameDialogues[randomChoice];
                        dialogueManager.StartDialogue();
                    } else 
                    {
                        randomChoice = Random.Range(0, farmCompletedRunDialogues.Count);
                        dialogueManager.dialogue = farmCompletedRunDialogues[randomChoice];
                        dialogueManager.StartDialogue();
                    }
                }
                break;
            case 2: //Went to proc gen
                    randomChoice = Random.Range(0, transitionDialogues.Count);
                    dialogueManager.dialogue = transitionDialogues[randomChoice];
                    dialogueManager.StartDialogue();
                break;
            default:
                break;
        }
        
    }
}
