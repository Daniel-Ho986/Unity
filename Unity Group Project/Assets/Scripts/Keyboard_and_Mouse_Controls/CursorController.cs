using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class CursorController : MonoBehaviour
{
    public Texture2D cursor;
    public Texture2D cursorHold;
    public Texture2D cursorClick;
    private CursorControls controls;
    private Camera mainCamera;

    //Space bar boolean flags--
    bool isPressedDown;//Used to detect space bar of keyboard being held down
    bool isReleased;//Used to detect space bar of keyboard being released

    RaycastHit2D heldObject;

    private void Awake()
    {
        controls = new CursorControls();
        ChangeCursor(cursor);
        Cursor.lockState = CursorLockMode.Confined;
        mainCamera = Camera.main;

        //Initializing the space bar boolean flags--
        isPressedDown = false;//For space bar of keyboard
        isReleased = true;//For space bar of keyboard
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    // Start is called before the first frame update
    private void Start()
    {
        controls.Mouse.Click.started += _ => StartedClick();
        controls.Mouse.Click.performed += _ => EndedClick();

        controls.Mouse.Hold.started += _ => StartedHold();
        controls.Mouse.Hold.canceled += _ => EndedHold();
    }

    private void StartedClick()
    {
        ChangeCursor(cursorClick);
    }

    private void EndedClick()
    {
        ChangeCursor(cursor);
        DetectObject();
    }

    private void StartedHold()
    {
        ChangeCursor(cursorHold);
        HoldObject();
        DetectObject();
    }

    private void EndedHold()
    {
        ChangeCursor(cursor);
        DropObject();
    }

    private void DetectObject()
    {
        Ray ray = mainCamera.ScreenPointToRay(controls.Mouse.Position.ReadValue<Vector2>());

        RaycastHit2D hits2D = Physics2D.GetRayIntersection(ray);
        if (hits2D.collider != null)
        {
            Debug.Log("Hit 2D Collider" + hits2D.collider.tag);
        }
    }

    private void HoldObject()
    {
        Vector2 mousePosition = controls.Mouse.Position.ReadValue<Vector2>();
        mousePosition = mainCamera.ScreenToWorldPoint(mousePosition);
        float xPos = mousePosition.x;
        float yPos = mousePosition.y;

        Ray ray = mainCamera.ScreenPointToRay(controls.Mouse.Position.ReadValue<Vector2>());

        RaycastHit2D hits2D = Physics2D.GetRayIntersection(ray);
        if (hits2D.collider != null)
        {
            //hits2D.transform.position = new Vector2(mousePosition.x, mousePosition.y);
            if (isPressedDown == false)
            {
                heldObject = hits2D;//Keeps track of what object is being held on to
                isPressedDown = true;
            }
            IClick click = hits2D.collider.GetComponent<IClick>();
            if (click != null)
                click.onHoldAction(xPos, yPos, isPressedDown);
        }

        if (isPressedDown)//When the space bar is held down
        {
            xPos = mousePosition.x;
            yPos = mousePosition.y;
            //heldObject.transform.position = new Vector2(mousePosition.x, mousePosition.y);
            IClick click = heldObject.collider.GetComponent<IClick>();
            if (click != null)
                click.onHoldAction(xPos, yPos, isPressedDown);
        }


    }

    private void DropObject()
    {
        Vector2 mousePosition = controls.Mouse.Position.ReadValue<Vector2>();
        mousePosition = mainCamera.ScreenToWorldPoint(mousePosition);
        float xPos = mousePosition.x;
        float yPos = mousePosition.y;

        Ray ray = mainCamera.ScreenPointToRay(controls.Mouse.Position.ReadValue<Vector2>());

        RaycastHit2D hits2D = Physics2D.GetRayIntersection(ray);
        if (hits2D.collider != null)
        {
            //hits2D.transform.position = new Vector2(mousePosition.x, mousePosition.y);
            IClick click = hits2D.collider.GetComponent<IClick>();
            if (click != null)
                click.onDropAction(xPos, yPos);
        }
        isPressedDown = false;//No longer holding down the space bar
        isReleased = true;//The space bar has been released
    }

    private void ChangeCursor(Texture2D cursorType)
    {
        Vector2 hotspot = new Vector2(cursorType.width / 2, cursorType.height / 2);
        Cursor.SetCursor(cursorType, hotspot, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        if (isPressedDown)//As long as the space bar is held down:
        {
            HoldObject();
        }
        if (isReleased)//Once the space bar is released:
        {
            DropObject();
            isReleased = false;
        }

    }
}