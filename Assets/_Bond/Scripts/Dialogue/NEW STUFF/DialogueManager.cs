﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public Conversation dialogue;

    private Queue<string> sentences = new Queue<string>();
    private DialogueTextManager dialogueTextManager;
    private DialoguePortrait dialoguePortrait;

    public void Start()
    {
        var DialogueCanvas = PersistentData.Instance.UI.transform.Find("CharacterDialogCanvas");
        dialogueTextManager = DialogueCanvas.transform.Find("CharacterDialog").GetComponent<DialogueTextManager>();
        dialoguePortrait = DialogueCanvas.transform.Find("Portrait").GetComponent<DialoguePortrait>();
    }

    public void StartDialogue()
    {
        //--------------------------------------------------
        // Tell PlayerController that dialogue is starting
        //--------------------------------------------------
        PersistentData.Instance.Player.GetComponent<PlayerController>().inCharacterDialog = true;
        PersistentData.Instance.UI.GetComponent<UIUpdates>().ShowCharacterDialogue();


        //----------------------------
        // Clear the sentences queue
        //----------------------------
        sentences.Clear();

        //--------------------------------------------
        // Begin queueing sentences for the dialogue
        //--------------------------------------------
        foreach (string sentence in dialogue.dialog)
        {
            sentences.Enqueue(sentence);
        }

        //----------------------------
        // Begin processing dialogue
        //----------------------------
        NextSentence();
    }

    public void NextSentence()
    {
        //------------------------------------------
        // If no more dialogues, exit the function
        //------------------------------------------
        if (sentences.Count == 0)
        {
            EndConversation();
            return;
        }

        //-------------------------------------------------------------
        // Begin parsing through initial information from a sentence
        // 
        // Sentences begin with "x/y " in front of them
        //      x   -   Who is speaking the sentence
        //      y   -   Determines the portrait used for that speaker
        //-------------------------------------------------------------
        string sentence = sentences.Dequeue();

        //-----------------------------------
        // x - Who is speaking the sentence
        //-----------------------------------
        int index = sentence.IndexOf('/');
        string speaker = sentence.Substring(0, index);

        //----------------------------------------------------
        // y - Determines the portrait used for that speaker
        //----------------------------------------------------
        sentence = sentence.Substring(index + 1);
        index = sentence.IndexOf(' ');
        string portrait = sentence.Substring(0, index);
        
        //--------------------------
        // Get the actual sentence
        //--------------------------
        sentence = sentence.Substring(index + 1);

        //--------------------------
        // Ready to start dialogue
        //--------------------------
        Debug.Log(sentence + ", " + speaker + ", " + portrait);
        dialogueTextManager.ResetSpeed();
        dialogueTextManager.ChangeText(speaker, sentence);
        dialoguePortrait.ChangePortrait(speaker, portrait);
    }

    public void EndConversation()
    {
        PersistentData.Instance.UI.GetComponent<UIUpdates>().HideCharacterDialogue();
        PersistentData.Instance.Player.GetComponent<PlayerController>().inCharacterDialog = false;
        PersistentData.Instance.Player.GetComponent<PlayerController>().dialogueManager = null;
        PersistentData.Instance.Player.GetComponent<PlayerController>().SetStandbyState(false);
    }
}
