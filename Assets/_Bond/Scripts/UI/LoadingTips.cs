using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadingTips : MonoBehaviour
{
    public List<string> tipsList;

    // Start is called before the first frame update
    void Start()
    {
        TextMeshProUGUI textComponent = GetComponent<TextMeshProUGUI>();
        textComponent.text = tipsList[Random.Range(0, tipsList.Count)];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
