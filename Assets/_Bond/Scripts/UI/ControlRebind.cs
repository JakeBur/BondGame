using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class ControlRebind : MonoBehaviour
{
    [SerializeField]
    private PlayerInput playerInput;
    public List<TextMeshProUGUI> KeyText = new List<TextMeshProUGUI>();
    public List<Button> KeyButton = new List<Button>();
    public List<string> actionNames = new List<string>();

    private InputActionRebindingExtensions.RebindingOperation RebindOperation;
    [SerializeField] private List<string> ActionsList = new List<string>();
    private List<string> currentlyBound = new List<string>();

    private void OnEnable()
    {
        if (PersistentData.Instance != null)
        {
            playerInput = PersistentData.Instance.playerInputs;
        }

        //-----------------------------------------
        // update the text on the binds on enable
        //-----------------------------------------
        for (int i = 0; i < 10; ++i)
        {
            string toAdd;

            if (i < 4)
            {
                InputAction actionBind = playerInput.actions.FindAction("movement");

                var binding = actionBind.bindings[i + 1];
                toAdd = StripQuotes(binding.ToDisplayString()); 
            }
            else
            {
                InputAction actionBind = playerInput.actions.FindAction(ActionsList[i]);
                toAdd = StripQuotes(actionBind.GetBindingDisplayString());
            }

            //--------------------------------------------------------------
            // form a list of already bound keys to prevent multiple binds
            //--------------------------------------------------------------
            KeyText[i].text = MouseButton(toAdd);
            currentlyBound.Add(KeyText[i].text);
        }
    }
    
    private void OnDisable()
    {
        //-------------------------------------------------------------
        // check if a rebind operation is still active and dispose it
        //-------------------------------------------------------------
        if(RebindOperation != null)
        {
            if(RebindOperation.started)
            {
                RebindOperation.Dispose();
            }
        }

        currentlyBound.Clear();

        if(PersistentData.Instance != null)
        {
            PersistentData.Instance.currBinds = BuildDictionary();
        }

        SaveControls();
    }

    public void RemapControl(int index)
    {
        //----------------------------------------------
        // find the operation to rebind and disable it
        //----------------------------------------------
        InputAction actionToRebind = playerInput.actions.FindAction(ActionsList[index]);
        actionToRebind.Disable();
        KeyButton[index].interactable = false;

        //----------------------------------------
        // save the old button data just in case
        //----------------------------------------
        Debug.Log(actionToRebind.controls.Count);
        int bindIndex = actionToRebind.GetBindingIndexForControl(actionToRebind.controls[0]);
        string currButton = actionToRebind.bindings[bindIndex].effectivePath;
        string currButtonDisplay = actionToRebind.GetBindingDisplayString();
        KeyText[index].text = "---";

        //---------------------------------------------
        // define rebinding operation before starting
        //---------------------------------------------
        RebindOperation = actionToRebind.PerformInteractiveRebinding()
            .WithCancelingThrough("<Keyboard>/escape")
            .OnMatchWaitForAnother(0.1f);
        
        RebindOperation.OnCancel(operation => {
            UpdateButton(index, currButton, currButtonDisplay);
        });

        RebindOperation.OnComplete(operation => {
            UpdateButton(index, currButton, currButtonDisplay);
        });

        //--------------------------------
        // all set, ask player to rebind
        //--------------------------------
        RebindOperation.Start();
        playerInput.actions.FindAction("pause").Disable();
    }

    public void RemapControlComposite(int index)
    {
        //----------------------------------------------
        // find the operation to rebind and disable it
        //----------------------------------------------
        InputAction actionToRebind = playerInput.actions.FindAction("movement");
        actionToRebind.Disable();
        KeyButton[index].interactable = false;

        //----------------------------------------
        // save the old button data just in case
        //----------------------------------------
        var binding = actionToRebind.bindings[index + 1];
        string currButton = binding.effectivePath;
        string currButtonDisplay = binding.ToDisplayString();
        KeyText[index].text = "---";

        //---------------------------------------------
        // define rebinding operation before starting
        //---------------------------------------------
        RebindOperation = actionToRebind.PerformInteractiveRebinding(index + 1)
            .WithCancelingThrough("<Keyboard>/escape")
            .OnMatchWaitForAnother(0.1f);

        RebindOperation.OnCancel(operation => {
            UpdateButtonComposite(index, currButton, currButtonDisplay);
        });

        RebindOperation.OnComplete(operation => {
            UpdateButtonComposite(index, currButton, currButtonDisplay);
        });

        //--------------------------------
        // all set, ask player to rebind
        //--------------------------------
        RebindOperation.Start();
        playerInput.actions.FindAction("pause").Disable();
    }

    private void UpdateButton(int index, string currButton, string currButtonDisplay)
    {
        InputAction keyToUpdate = playerInput.actions.FindAction(ActionsList[index]);
      
        if (CheckIfBound(keyToUpdate.GetBindingDisplayString()))
        {
            //-------------------------------------------------
            // prevent a key from binding to multiple actions
            //-------------------------------------------------
            Debug.Log("Already bound, reverting to original");
            keyToUpdate.ApplyBindingOverride(currButton);
            KeyText[index].text = StripQuotes(currButtonDisplay);
        }
        else
        {
            //------------------------------------
            // otherwise update text accordingly
            //------------------------------------
            currentlyBound[index] = StripQuotes(keyToUpdate.GetBindingDisplayString());
            KeyText[index].text = MouseButton(currentlyBound[index]);
        }

        //------------------------------------------------
        // reset values to default and dispose operation
        //------------------------------------------------
        RebindOperation.Dispose();
        keyToUpdate.Enable();
        KeyButton[index].interactable = true;
        SaveControls();

        playerInput.actions.FindAction("pause").Enable();
    }

    private void UpdateButtonComposite(int index, string currButton, string currButtonDisplay)
    {
        InputAction keyToUpdate = playerInput.actions.FindAction("movement");

        var binding = keyToUpdate.bindings[index + 1];
        string newKey = StripQuotes(binding.ToDisplayString());

        if (CheckIfBound(newKey))
        {
            //-------------------------------------------------
            // prevent a key from binding to multiple actions
            //-------------------------------------------------
            Debug.Log("Already bound, reverting to original");
            keyToUpdate.ApplyBindingOverride(index + 1, currButton);
            KeyText[index].text = StripQuotes(currButtonDisplay);
        }
        else
        {
            //------------------------------------
            // otherwise update text accordingly
            //------------------------------------
            currentlyBound[index] = newKey;
            KeyText[index].text = MouseButton(currentlyBound[index]);
        }

        //------------------------------------------------
        // reset values to default and dispose operation
        //------------------------------------------------
        RebindOperation.Dispose();
        keyToUpdate.Enable();
        KeyButton[index].interactable = true;
        SaveControls();

        playerInput.actions.FindAction("pause").Enable();
    }

    private bool CheckIfBound(string key)
    {
        //--------------------------------
        // check if key is already bound
        //--------------------------------
        foreach(var i in currentlyBound)
        {
            if (key == i)
            {
                return true;
            }
        }
        return false;
    }

    private void SaveControls()
    {
        string bindings = playerInput.actions.ToJson();
        PlayerPrefs.SetString("Bindings", bindings);
    }

    private string StripQuotes(string toStrip)
    {
        if (toStrip.StartsWith("\"") && toStrip.EndsWith("\"") && toStrip.Length > 1)
        {
            toStrip = toStrip.Substring(1, toStrip.Length - 2);
        }

        return toStrip;
    }

    private string MouseButton(string bind)
    {
        switch(bind)
            {
                case "LMB":
                    return "Left Mouse";
                case "RMB":
                    return "Right Mouse";
                default:
                    return bind;
            }
    }

    public Dictionary<string, string> BuildDictionary()
    {
        //-----------------------------------------------
        // Names for actions to refer to in dictionary:
        // Move Up
        // Move Left
        // Move Down
        // Move Right
        // Roll
        // Attack
        // Interact
        // Creature Ability 1
        // Creature Ability 2
        // Swap Creature
        //------------------------------------------------
        Dictionary<string, string> toReturn = new Dictionary<string, string>();
        string currBind;

        if (PersistentData.Instance != null)
        {
            playerInput = PersistentData.Instance.playerInputs;
        }

        for (int i = 0; i < 10; ++i)
        {
            if (i < 4)
            {
                InputAction actionBind = playerInput.actions.FindAction("movement");

                var binding = actionBind.bindings[i + 1];
                string displayString = binding.ToDisplayString();
                currBind = StripQuotes(displayString);           
            }
            else
            {
                InputAction actionBind = playerInput.actions.FindAction(ActionsList[i]);
                currBind = StripQuotes(actionBind.GetBindingDisplayString());
            }

            //-----------------------------------------------
            // change mouse buttons into more readable text
            //-----------------------------------------------
            switch(currBind)
            {
                case "LMB":
                    currBind = "Left Mouse Button";
                    break;
                case "RMB":
                    currBind = "Right Mouse Button";
                    break;
                default:
                    break;
            }

            //--------------------------------
            // add the key to the dictionary
            //--------------------------------
            toReturn.Add(actionNames[i], currBind);
        }

        return toReturn;
    }
}
