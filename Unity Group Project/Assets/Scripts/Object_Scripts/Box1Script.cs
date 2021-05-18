using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box1Script : MonoBehaviour, BoxScript
{
    public GameObject leftNeighborBox;
    public GameObject rightNeighborBox;
    public GameObject dataValue;

    private GameObject selectReticle;
    private int number;
    private int prevNum;

    // Start is called before the first frame update
    void Start()
    {
        number = dataValue.GetComponent<NumberScript>().GetNumber();
        prevNum = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (number != dataValue.GetComponent<NumberScript>().GetNumber())
        {
            number = dataValue.GetComponent<NumberScript>().GetNumber();
        }
    }


    public int GetNumber() { return number; }
    public GameObject GetDataValue()
    {
        if (dataValue != null) return dataValue;
        else return null;
    }

    /*
    public void SetLeftNeighborBox(GameObject neighbor) { leftNeighborBox = neighbor; }
    public void SetRightNeighborBox(GameObject neighbor) { rightNeighborBox = neighbor; }
    */

    public GameObject GetLeftNeighborBox() { return leftNeighborBox; }
    public GameObject GetRightNeighborBox() { return rightNeighborBox; }


    //Trigger methods
    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Something entered a box: " + other.tag);
        if (other.gameObject.tag == "Reticle")
        {
            selectReticle = other.gameObject;
            selectReticle.transform.position = gameObject.transform.position;
        }
        if (other.gameObject.tag == "DataValue")
        {
            GameObject newDataValue = other.gameObject;
            if (dataValue.GetComponent<NumberScript>().GetNumber() != null)
            {
                prevNum = number;
                number = newDataValue.GetComponent<NumberScript>().GetNumber();
                Debug.Log("Changed a box's number from: " + prevNum + " to " + number);
            }
            dataValue = newDataValue;
            dataValue.transform.position = gameObject.transform.position;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {

    }

    void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log("Something left a box: " + other.tag);
    }
}
