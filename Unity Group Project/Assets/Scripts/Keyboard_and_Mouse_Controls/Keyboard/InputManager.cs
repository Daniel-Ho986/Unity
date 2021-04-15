using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    [SerializeField] private KeyBindings keyBindings;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }

    public KeyCode GetKeyForAction(KeyBindingActions keyBindingAction)
    {
        foreach (KeyBindings.KeyBindingCheck keyBindingCheck in keyBindings.keyBindingChecks)
        {
            if (keyBindingCheck.keyBindingAction == keyBindingAction)
            {
                return keyBindingCheck.keyCode;
            }
        }
        return KeyCode.None;
    }

    public bool GetKey(KeyBindingActions key)
    {
        foreach (KeyBindings.KeyBindingCheck keyBindingCheck in keyBindings.keyBindingChecks)
        {
            if (keyBindingCheck.keyBindingAction == key)
            {
                return Input.GetKey(keyBindingCheck.keyCode);
            }
        }
        return false;
    }

    public bool GetKeyDown(KeyBindingActions key)
    {
        foreach (KeyBindings.KeyBindingCheck keyBindingCheck in keyBindings.keyBindingChecks)
        {
            if (keyBindingCheck.keyBindingAction == key)
            {
                return Input.GetKeyDown(keyBindingCheck.keyCode);
            }
        }
        return false;
    }

    public bool GetKeyUp(KeyBindingActions key)
    {
        foreach (KeyBindings.KeyBindingCheck keyBindingCheck in keyBindings.keyBindingChecks)
        {
            if (keyBindingCheck.keyBindingAction == key)
            {
                return Input.GetKeyUp(keyBindingCheck.keyCode);
            }
        }
        return false;
    }
}
