using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Number2Script : MonoBehaviour, NumberScript
{
    public int number;

    private bool isLeftMouseDown;
    private bool isRightMouseDown;
    private bool isMiddleMouseDown;


    void Start()
    {
        number = gameObject.GetComponent<Number2Script>().GetNumber();

        isLeftMouseDown = false;
        isRightMouseDown = false;
        isMiddleMouseDown = false;
    }

    void Update()
    {
        //isLeftMouseDown = Input.GetMouseButtonDown(0);
        //isRightMouseDown = Input.GetMouseButtonDown(1);
        //isMiddleMouseDown = Input.GetMouseButtonDown(2);
    }
    

    public int GetNumber() { return number; }
    public bool GetLeftMouseDown() { return isLeftMouseDown; }
    public bool GetRightMouseDown() { return isRightMouseDown; }
    public bool GetMiddleMouseDown() { return isMiddleMouseDown; }
    public void SetLeftMouseDown(bool truthValue) { isLeftMouseDown = truthValue; }
    public void SetRightMouseDown(bool truthValue) { isRightMouseDown = truthValue; }
    public void SetMiddleMouseDown(bool truthValue) { isMiddleMouseDown = truthValue; }

    public void onClickAction()
    {
        Debug.Log("Clicked on a number: " + number);
    }

    public void onHoldAction(float xPos, float yPos)
    {
        Debug.Log("Holding on to a number: " + number);

        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(xPos, yPos, 10f));

    }

    public void onDropAction(float xPos, float yPos)
    {
        Debug.Log("Dropped a number: " + number);

        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(xPos, yPos, 10f));
    }

}
