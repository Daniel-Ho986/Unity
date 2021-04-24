using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager instance;

    [SerializeField] private KeyBindings keyBindings;

    private bool mouseControlsActive;

    private Camera mainCamera;
    private GameObject dataValue;

    void Start()
    {
        mouseControlsActive = false;

        mainCamera = Camera.main;
    }

    void Update()
    {
        if (mouseControlsActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Left mouse button is clicked...");
                if (dataValue != null)
                {
                    if (dataValue.tag == "DataValue")
                    {
                        dataValue.GetComponent<NumberScript>().SetLeftMouseDown(false);
                        dataValue.GetComponent<NumberScript>().onDropAction(Input.mousePosition.x,
                                                                            Input.mousePosition.y);
                        dataValue = null;
                    }

                }
                else { DetectObject(); }
            }
            if (dataValue != null)
            {
                if (dataValue.tag == "DataValue")
                {
                    if (dataValue.GetComponent<NumberScript>().GetLeftMouseDown() == true)
                    {
                        Debug.Log("Left mouse button is down on a DataValue...");
                        dataValue.GetComponent<NumberScript>().onHoldAction(Input.mousePosition.x,
                                                                            Input.mousePosition.y);
                    }
                }
            }
            else
            {
                //Do nothing
            }
        }
    }


    public void EnableMouseControls() { mouseControlsActive = true; }
    public void DisableMouseControls() { mouseControlsActive = false; }

    private void DetectObject()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit2D hits2D = Physics2D.GetRayIntersection(ray);
        if (hits2D.collider != null)
        {
            Debug.Log("Hit 2D Collider" + hits2D.collider.tag);
            if (hits2D.collider.tag == "DataValue")
            {
                dataValue = hits2D.collider.gameObject;
                dataValue.GetComponent<NumberScript>().SetLeftMouseDown(true);
                Debug.Log("DataValue was clicked: " + dataValue.GetComponent<NumberScript>().GetNumber());
            }
        }
    }

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
