﻿using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using TMPro;

public class DialogueTextManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueBox;
    public DialoguePortrait dialoguePortrait;
    public bool sentenceFinished = true;

    private IEnumerator typeText;
    private float textSpeed = 0.0375f;
    private float textSpeedMult = 1f;
    private float textSpeedBackup = 0.0375f;
    private string speaker;
    private string sentence;

    public void ResetSpeed()
    {
        textSpeedBackup = 0.0375f;
        textSpeed = textSpeedBackup;
    }

    public void FinishSentence()
    {
        if (typeText != null)
        {
            StopCoroutine(typeText);
        }
        string strippedSentence = StripSentence(sentence);
        dialogueBox.text = strippedSentence;
        sentenceFinished = true;
    }

    private string StripSentence(string _sentence)
    {
        string newSentence = _sentence;
    
        //----------------------------------------------------
        // Replace all newline tags with actual newline char
        //----------------------------------------------------
        newSentence = newSentence.Replace("\\n\\", "\n");

        //--------------------------------------------
        // Get last portrait change call information
        //--------------------------------------------
        int lastPortraitChangeIndex = newSentence.LastIndexOf("\\f");
        if (lastPortraitChangeIndex >= 0)
        {
            string temp = newSentence.Substring(lastPortraitChangeIndex + 3);
            int closingTagIndex = temp.IndexOf("\\");
            string newPortrait = temp.Substring(0, closingTagIndex);
            Debug.Log(newPortrait);
            dialoguePortrait.ChangePortrait(speaker, newPortrait);
        }

        //-----------------------------------------
        // Strip the sentence of tags using regex
        //-----------------------------------------
        newSentence = Regex.Replace(newSentence, @"\\.*?\\", "").Trim();

        return newSentence;
    }

    public void ChangeText(string _speaker, string _sentence)
    {
        //-----------------------------------------------
        // If a dialogue is skipped, stop the coroutine
        //-----------------------------------------------
        if (typeText != null)
        {
            StopCoroutine(typeText);
        }
        dialogueBox.text = "";
        speaker = _speaker;
        sentence = _sentence;
        sentenceFinished = false;
        typeText = LetterByLetter(sentence);
        StartCoroutine(typeText);
    }

    public IEnumerator LetterByLetter(string sentence)
    {
        while (sentence.Length > 0)
        {
            //----------------------------------------------
            // Parse through the sentence letter by letter
            //----------------------------------------------
            string currLetter = sentence.Substring(0, 1);
            sentence = sentence.Substring(1);

            //------------------------------
            // Ignore whole rich text tags
            //------------------------------
            if (currLetter == "<")
            {
                //--------------------------------------------------------
                // Find closing rich text tag and add entire tag to text
                // Don't worry, TMPro will not add it to the actual text
                //--------------------------------------------------------
                int index = sentence.IndexOf(">");
                currLetter += sentence.Substring(0, index + 1);
                sentence = sentence.Substring(index + 1);
                textSpeed = 0;
            }
            else
            {
                //----------------------------------------------------------
                // Browse for user-defined tags; these are marked with '\'
                //----------------------------------------------------------
                if (currLetter == "\\")
                {
                    currLetter = sentence.Substring(0, 1);
                    sentence = sentence.Substring(2);

                    //---------------------------------------------
                    // "\n\" -   makes a newline w/o delay in text
                    //---------------------------------------------
                    if (currLetter == "n")
                    {
                        currLetter = "\n";
                        textSpeed = 0;
                    }

                    //-------------------------------------
                    // "\f=x\" -    changes portrait to x
                    //-------------------------------------
                    if (currLetter == "f")
                    {
                        int index = sentence.IndexOf("\\");
                        string newPortrait = sentence.Substring(0, index);
                        dialoguePortrait.ChangePortrait(speaker, newPortrait);

                        currLetter = sentence.Substring(index + 1, 1);
                        sentence = sentence.Substring(index + 2);
                    }

                    //-------------------------------------------------
                    // "\d=x\" -    delays next letter by factor of x
                    //-------------------------------------------------
                    if (currLetter == "d")
                    {
                        int index = sentence.IndexOf("\\");
                        float factor = float.Parse(sentence.Substring(0, index));
                        textSpeed *= factor;

                        currLetter = sentence.Substring(index +1, 1);
                        sentence = sentence.Substring(index + 2);
                    }

                    //--------------------------------------------
                    // "\s=x\" -    changes base text speed to x
                    //--------------------------------------------
                    if (currLetter == "s")
                    {
                        int index = sentence.IndexOf("\\");
                        float newSpeed = float.Parse(sentence.Substring(0, index));
                        textSpeedBackup = newSpeed;
                        textSpeed = textSpeedBackup;

                        currLetter = sentence.Substring(index + 1, 1);
                        sentence = sentence.Substring(index + 2);
                    }
                }
                else
                {
                    //---------------------------
                    // Reserved for beep speech
                    //---------------------------
                }
            }
            
            //------------------------------
            // Add next letter to the text
            //------------------------------
            yield return new WaitForSeconds(textSpeed * textSpeedMult);
            dialogueBox.text += currLetter;
            textSpeed = textSpeedBackup;
        }

        //-----------------------------------------------------------
        // Once sentence is over, reset the speed values to default
        //-----------------------------------------------------------
        sentenceFinished = true;
        textSpeedBackup = 0.0375f;
        textSpeed = textSpeedBackup;
    }
}
