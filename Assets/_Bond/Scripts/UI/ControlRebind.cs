using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class ControlRebind : MonoBehaviour
{
    private InputActionAsset Actions
    {
        get => PersistentData.Instance.playerInputs;
    }
    public List<TextMeshProUGUI> KeyText = new List<TextMeshProUGUI>();
    public List<Button> KeyButton = new List<Button>();

    private InputActionRebindingExtensions.RebindingOperation RebindOperation;
    [SerializeField] private List<string> ActionsList = new List<string>();

    private void OnEnable()
    {
        //-----------------------------------------
        // update the text on the binds on enable
        //-----------------------------------------
        for (int i = 0; i < 10; ++i)
        {
            PersistentData.Instance.LoadControls();
            if (i < 4)
            {
                InputAction actionBind = Actions.FindAction("movement");

                string direction = ActionsList[i];
                var bindingIndex = actionBind.bindings.IndexOf(x => x.isPartOfComposite && x.name == direction);
                Debug.Log(bindingIndex);
            }
            else
            {
                InputAction actionBind = Actions.FindAction(ActionsList[i]);
                KeyText[i].text = actionBind.GetBindingDisplayString();
            }
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

        PersistentData.Instance.SaveControls();
    }

    private void OnApplicationQuit()
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

        PersistentData.Instance.SaveControls();
    }

    public void RemapControl(int index)
    {
        //----------------------------------------------
        // find the operation to rebind and disable it
        //----------------------------------------------
        InputAction actionToRebind = Actions.FindAction(ActionsList[index]);
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
        /*
        if (index < 4)
        {
            //-------------------------------
            // movement 2D Vector Composite
            //-------------------------------
            RebindOperation = actionToRebind.PerformInteractiveRebinding(index + 2)
                .WithCancelingThrough("<Keyboard>/escape")
                .OnMatchWaitForAnother(0.1f);
        }
        else
        {
            RebindOperation = actionToRebind.PerformInteractiveRebinding()
                .WithCancelingThrough("<Keyboard>/escape")
                .OnMatchWaitForAnother(0.1f);
        }
        */
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

    private void UpdateButton(int index, string currButton, string currButtonDisplay)
    {
        InputAction keyToUpdate = Actions.FindAction(ActionsList[index]);

        //-------------------------------------------------
        // prevent a key from binding to multiple actions
        //-------------------------------------------------
        int bindIndex = keyToUpdate.GetBindingIndexForControl(keyToUpdate.controls[0]);
        string newPath = keyToUpdate.bindings[bindIndex].effectivePath;
        Debug.Log(newPath);
        var checkList = InputSystem.FindControls(newPath);
        Debug.Log(checkList.Count);
        if (checkList.Count > 1)
        {
            Debug.Log("Already bound, reverting to original");
            keyToUpdate.ApplyBindingOverride(currButton);
            KeyText[index].text = currButtonDisplay;

            checkList.Dispose();
            RebindOperation.Dispose();
            keyToUpdate.Enable();
            KeyButton[index].interactable = true;
            return;
        }

        //------------------------------------
        // otherwise update text accordingly
        //------------------------------------
        /*
        if (index < 4)
        {

        }
        else
        {
            KeyText[index].text = keyToUpdate.GetBindingDisplayString();
        }
        */
        keyToUpdate.ApplyBindingOverride(newPath);
        KeyText[index].text = keyToUpdate.GetBindingDisplayString();

        checkList.Dispose();
        RebindOperation.Dispose();
        keyToUpdate.Enable();
        KeyButton[index].interactable = true;
        PersistentData.Instance.SaveControls();
    }
}
