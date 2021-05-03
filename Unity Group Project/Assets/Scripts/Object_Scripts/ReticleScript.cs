using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReticleScript : MonoBehaviour
{
    private GameObject leftMemorySpace;
    private GameObject rightMemorySpace;
    private GameObject currentMemorySpace;

    private bool madeFinalChoice;
    private bool confirmChoice;
    private bool declineChoice;

    private int answerInt;
    private bool answerBool;
    private bool answerSaveChanges;

    // Start is called before the first frame update
    void Start()
    {
        madeFinalChoice = false;
        confirmChoice = false;
        declineChoice = false;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void SetAnswerInt(int num) { answerInt = num; }
    public void SetAnswerBool(bool truthValue) { answerBool = truthValue; }
    public int GetAnswerInt() { return answerInt; }
    public bool GetAnswerBool() { return answerBool; }

    public void SetAnswerSave(bool truthValue) { answerSaveChanges = truthValue; }
    public bool GetAnswerSave() { return answerSaveChanges; }

    public void SetLeftMemorySpace(GameObject space) { leftMemorySpace = space; }
    public void SetRightMemorySpace(GameObject space) { rightMemorySpace = space; }
    public void SetCurrentMemorySpace(GameObject space) { currentMemorySpace = space; }
    public void SetFinalChoice(bool choice) { madeFinalChoice = choice; }
    public void SetConfirm(bool booleanValue) { confirmChoice = booleanValue; }
    public void SetDecline(bool booleanValue) { declineChoice = booleanValue; }

    public GameObject GetLeftMemorySpace() { return leftMemorySpace; }
    public GameObject GetRighMemorySpace() { return rightMemorySpace; }
    public GameObject GetCurrentMemorySpace() { return currentMemorySpace; }
    public bool GetFinalChoice() { return madeFinalChoice; }
    public bool GetConfirm() { return confirmChoice; }
    public bool GetDecline() { return declineChoice; }


    public void ConfirmFinalChoice()
    {
        Debug.Log("Confirmed to finalized answer choice");
        confirmChoice = true; //Makes the temporary choice the finalized choice via confirmation
        madeFinalChoice = false; //Resets the boolean flag indicating a temporary choice has been made
    }

    public void DeclineFinalChoice()
    {
        Debug.Log("Declined to finalize answer choice.");
        declineChoice = true; //Does not make the temporary choice the finalized choice via decline
        madeFinalChoice = false; //Resets the boolean flag indicating a temporary choice has been made
    }

    public void AnswerContinueIteration()
    {
        Debug.Log("An answer was chosen: Continue Iteration");
        madeFinalChoice = true;
        answerBool = true; //A value of "true" corresponds with a decision to continue the iteration
    }

    public void AnswerEndIteration()
    {
        Debug.Log("An answer was chosen: End Iteration");
        madeFinalChoice = true;
        answerBool = false; //A value of "false" corresponds with a decision to end the iteration
    }

    public void AnswerSaveChanges()
    {
        Debug.Log("An answer was chosen: Save Changes");
        madeFinalChoice = true;
        answerSaveChanges = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log("Reticle just entered the following: " + other.tag);
        currentMemorySpace = other.gameObject;
        if (currentMemorySpace.tag == "MemorySpace")
        {
            if (currentMemorySpace.GetComponent<BoxScript>().GetNumber() != null)
            {
                answerInt = currentMemorySpace.GetComponent<BoxScript>().GetNumber();
            }
        }

        if (other.gameObject.tag == "MemorySpace")
        {
            if (other.GetComponent<BoxScript>().leftNeighborBox)
            {
                leftMemorySpace = other.GetComponent<BoxScript>().leftNeighborBox;
            }
            if (other.GetComponent<BoxScript>().rightNeighborBox)
            {
                rightMemorySpace = other.GetComponent<BoxScript>().rightNeighborBox;
            }
        }

    }

    void OnTriggerStay2D(Collider2D other)
    {
        //Debug.Log("Reticle is staying in the following: " + other.tag);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        //Debug.Log("Reticle just left the following: " + other.tag);
    }
}
