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
            if (i < 4)
            {
                InputAction actionBind = playerInput.actions.FindAction("movement");

                var binding = actionBind.bindings[i + 1];
                string displayString = binding.ToDisplayString();
                if (displayString.StartsWith("\"") && displayString.EndsWith("\"") && displayString.Length > 1)
                {
                    KeyText[i].text = displayString.Substring(1, displayString.Length - 2);
                }
                else
                {
                    KeyText[i].text = displayString;
                }           
            }
            else
            {
                InputAction actionBind = playerInput.actions.FindAction(ActionsList[i]);
                KeyText[i].text = actionBind.GetBindingDisplayString();
            }

            //--------------------------------------------------------------
            // form a list of already bound keys to prevent multiple binds
            //--------------------------------------------------------------
            currentlyBound.Add(KeyText[i].text);
        }

        playerInput.actions.FindAction("pause").Disable();
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
        playerInput.actions.FindAction("pause").Enable();
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
            KeyText[index].text = currButtonDisplay;
        }
        else
        {
            //------------------------------------
            // otherwise update text accordingly
            //------------------------------------
            KeyText[index].text = keyToUpdate.GetBindingDisplayString();
            currentlyBound[index] = KeyText[index].text;
        }

        //------------------------------------------------
        // reset values to default and dispose operation
        //------------------------------------------------
        RebindOperation.Dispose();
        keyToUpdate.Enable();
        KeyButton[index].interactable = true;
        SaveControls();
    }

    private void UpdateButtonComposite(int index, string currButton, string currButtonDisplay)
    {
        InputAction keyToUpdate = playerInput.actions.FindAction("movement");

        var binding = keyToUpdate.bindings[index + 1];
        string newKey = binding.ToDisplayString();

        if (CheckIfBound(newKey))
        {
            //-------------------------------------------------
            // prevent a key from binding to multiple actions
            //-------------------------------------------------
            Debug.Log("Already bound, reverting to original");
            keyToUpdate.ApplyBindingOverride(index + 1, currButton);
            if (currButtonDisplay.StartsWith("\"") && currButtonDisplay.EndsWith("\"") && currButtonDisplay.Length > 1)
            {
                KeyText[index].text = currButtonDisplay.Substring(1, currButtonDisplay.Length - 2);
            }
            else
            {
                KeyText[index].text = currButtonDisplay;
            }
        }
        else
        {
            //------------------------------------
            // otherwise update text accordingly
            //------------------------------------
            KeyText[index].text = newKey;
            currentlyBound[index] = KeyText[index].text;
        }

        //------------------------------------------------
        // reset values to default and dispose operation
        //------------------------------------------------
        RebindOperation.Dispose();
        keyToUpdate.Enable();
        KeyButton[index].interactable = true;
        SaveControls();
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
}
