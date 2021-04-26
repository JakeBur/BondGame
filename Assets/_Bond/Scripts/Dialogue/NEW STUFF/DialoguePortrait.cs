using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialoguePortrait : MonoBehaviour
{
    public Sprite[] MCFaces;
    public Sprite[] ShopkeeperFaces;

    private Dictionary<string, int> emotionIndex = new Dictionary<string, int>();
    private Image image;

    private void Awake()
    {
        //-------------------------------------------------------------------------------------------
        // Populate the emotionIndex dictionary with string-int pairs to use with the Sprite arrays
        // Keep in mind the same index in all Sprite arrays will reference the same emotion
        //-------------------------------------------------------------------------------------------
        emotionIndex.Add("Happy", 0);
        emotionIndex.Add("Surprised", 1);
        emotionIndex.Add("Sad", 2);
        emotionIndex.Add("Cheerful", 3);
        emotionIndex.Add("Angry", 4);

        image = GetComponent<Image>();
    }

    public void ChangePortrait(string speaker, string emotion)
    {
        if (speaker == "MC")
        {
            image.sprite = MCFaces[emotionIndex[emotion]];
        }

        if (speaker == "Shopkeeper")
        {
            image.sprite = ShopkeeperFaces[emotionIndex[emotion]];
        }
    }
}