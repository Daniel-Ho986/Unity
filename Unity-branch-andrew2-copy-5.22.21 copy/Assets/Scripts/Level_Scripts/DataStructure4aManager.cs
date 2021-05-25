using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStructure4aManager : MonoBehaviour, DataStructureManager
{
    //Box Objects (memory spaces)--
    public GameObject tempSpace;
    public GameObject space0;
    public GameObject space1;
    public GameObject space2;
    public GameObject space3;
    public GameObject space4;


    //Variables for tracking state of data structure:
    private int currentIteration;
    private int iterationStage;
    private int[] iterations;
    private bool[] iterationStatuses;
    private bool isSorted;
    private bool isVisible;
    private bool isBubbled0;
    private bool isBubbled1;
    private bool isBubbled2;
    private bool isBubbled3;
    private bool isBubbled4;


    //Question-Answer Arrays and Positions:
    private int[] question1and3Answers;//Question1/Question3: Which index do we start/continue swapping with?
    private int[] question2Answers;    //Question2: Which index do we compare with?
    private bool[] question4Answers;   //Question4: Should we swap these numbers or leave them as is?
    private bool[] question5Answers;   //Question5: Do we continue swapping or do we end this iteration?
                                       //           i.e. Is the value we started with in it's final position?

    private int question1and3Position; //The position within the list of question1and3Answers that needs to be
                                       //  correctly answered before advancing onward
    private int question2Position;     //The position within the list of question2Answers that needs to be
                                       //  correctly answered before advancing onward
    private int question4Position;     //The position within the list of question4Answers that needs to be
                                       //  correctly answered before advancing onward
    private int question5Position;     //The position within the list of question5Answers that needs to be
                                       //  correctly answered before advancing onward


    //Current and final states of the data structure:
    private int[] solutionValues;
    private int[] currentValues;

    private int[,] iterationPhases2D;
    private int iterationPhasesPosition;




    // Start is called before the first frame update
    void Start()
    {
        currentIteration = 1; //The main loop iteration number of the sorting algorithm
        iterationStage = 1; //The nested loop iteration number of the sorting algorithm
        iterations = new int[] { 1, 2, 3 }; //An array of all the main loop iterations
        iterationStatuses = new bool[] { false, false, false }; //Iteration solved states
        isSorted = false;
        isVisible = true;

        isBubbled0 = false;
        isBubbled1 = false;
        isBubbled2 = false;
        isBubbled3 = false;
        isBubbled4 = false;


        question1and3Answers = new int[] { 11, 11, 12, 13, 10, 11, 12, 10, 11 };
        question2Answers = new int[] {     10, 12, 13, 20, 11, 12, 20, 11, 20 };
        question4Answers = new bool[] { true, false, false, true, false, false, true, false, true };
        question5Answers = new bool[] { true, true, true, false, true, true, false, true, false };

        question1and3Position = 0;
        question2Position = 0;
        question4Position = 0;
        question5Position = 0;



        solutionValues = new int[] { 10, 20, 11, 12, 13 }; // { 0, 0, 1, 1, 1} <-- Solution
        currentValues = new int[] { 11, 10, 12, 13, 20 };  // { 1, 0, 1, 1, 0} <-- Start

        //iterationPhases2D has 9 rows and 5 columns
        //  --> row 0, column 0 would be a[0,0], which equals 4
        //  --> row 0, column 1 would be a[0,1], which equals 5
        //  --> row 5, column 3 would be a[5,3], which equals 1
        iterationPhases2D = new int[,]
        { { 10, 11, 12, 13, 20 }, //Bubbled 10 to space 0
          { 10, 11, 12, 13, 20 },
          { 10, 11, 12, 13, 20 },
          { 10, 11, 12, 20, 13 }, //Bubbled 13 to space 4
          { 10, 11, 12, 20, 13 },
          { 10, 11, 12, 20, 13 },
          { 10, 11, 20, 12, 13 }, //Bubbled 12 to space 3
          { 10, 11, 20, 12, 13 }, 
          { 10, 20, 11, 12, 13 }  //Bubbled 20 to space 1, and bubbled 11 to space 2
        };
        iterationPhasesPosition = 0;
    }


    //Swapping Methods:
    public void PerformSwap(GameObject numToBubbleSpace, GameObject numToSwapSpace, GameObject tempNumSpace)
    {
        if (numToBubbleSpace != null && numToSwapSpace != null)
        {
            if (numToBubbleSpace.GetComponent<BoxScript>() != null
            && numToSwapSpace.GetComponent<BoxScript>() != null)
            {
                GameObject dataValueBubble = numToBubbleSpace.GetComponent<BoxScript>().GetDataValue();
                GameObject dataValueSwap = numToSwapSpace.GetComponent<BoxScript>().GetDataValue();
                GameObject dataValueTemp = tempNumSpace.GetComponent<BoxScript>().GetDataValue();
                Debug.Log("Now performing the swap...");
                StartCoroutine(SwapStep1Coroutine(numToBubbleSpace, numToSwapSpace, dataValueBubble, dataValueSwap, tempNumSpace, dataValueTemp));
                StartCoroutine(SwapStep2Coroutine(numToBubbleSpace, numToSwapSpace, dataValueBubble, dataValueSwap, tempNumSpace, dataValueTemp));
                StartCoroutine(SwapStep3Coroutine(numToBubbleSpace, numToSwapSpace, dataValueBubble, dataValueSwap, tempNumSpace, dataValueTemp));
                StartCoroutine(SwapStep4Coroutine(numToBubbleSpace, numToSwapSpace, dataValueBubble, dataValueSwap, tempNumSpace, dataValueTemp));
                StartCoroutine(SwapStep5Coroutine(numToBubbleSpace, numToSwapSpace, dataValueBubble, dataValueSwap, tempNumSpace, dataValueTemp));
                StartCoroutine(SwapStep6Coroutine(numToBubbleSpace, numToSwapSpace, dataValueBubble, dataValueSwap, tempNumSpace, dataValueTemp));
            }
        }
    }

    /* --> NOTE: NOT NECESSARY, THEREFORE NOT USED!!!
    IEnumerator NumberSwapCoroutine(GameObject numToBubbleSpace, GameObject numToSwapSpace,
                                    GameObject dataValueBubble, GameObject dataValueSwap, GameObject tempNumSpace)
    {
        Debug.Log("Now performing the swap...");
        yield return new WaitForSeconds(0.5f);

        tempSpace.GetComponent<BoxScript>().GetDataValue().transform.position = numToBubbleSpace.transform.position;
        Debug.Log("Step 1: Moved tempValue to numBubbleSpace...");
        yield return new WaitForSeconds(0.5f);
        dataValueBubble.transform.position = tempSpace.transform.position;
        Debug.Log("Step 2: Moved dataValueBubble to tempSpace...");
        yield return new WaitForSeconds(0.5f);

        tempSpace.GetComponent<BoxScript>().GetDataValue().transform.position = numToSwapSpace.transform.position;
        Debug.Log("Step 3: Moved tempValue to numSwapSpace...");
        yield return new WaitForSeconds(0.5f);
        dataValueSwap.transform.position = numToBubbleSpace.transform.position;
        Debug.Log("Step 4: Moved dataValueSwap to numBubbleSpace...");
        yield return new WaitForSeconds(0.5f);

        tempSpace.GetComponent<BoxScript>().GetDataValue().transform.position = tempNumSpace.transform.position;
        Debug.Log("Step 5: Moved tempValue back to it's original position of tempSpace...");
        yield return new WaitForSeconds(0.5f);
        dataValueBubble.transform.position = numToSwapSpace.transform.position;
        Debug.Log("Step 6: Moved dataValueBubble to numSwapSpace...");
        yield return new WaitForSeconds(0.5f);

        Debug.Log("Swap has been performed!!!");
    }
    */

    IEnumerator SwapStep1Coroutine(GameObject numToBubbleSpace, GameObject numToSwapSpace,
                                    GameObject dataValueBubble, GameObject dataValueSwap,
                                    GameObject tempNumSpace, GameObject dataValueTemp)
    {
        yield return new WaitForSeconds(0.5f);

        dataValueTemp.transform.position = numToBubbleSpace.transform.position;
        Debug.Log("Step 1: Moved tempValue to numBubbleSpace...");
    }
    IEnumerator SwapStep2Coroutine(GameObject numToBubbleSpace, GameObject numToSwapSpace,
                                    GameObject dataValueBubble, GameObject dataValueSwap,
                                    GameObject tempNumSpace, GameObject dataValueTemp)
    {
        yield return new WaitForSeconds(1.0f);

        dataValueBubble.transform.position = tempNumSpace.transform.position;
        Debug.Log("Step 2: Moved dataValueBubble to tempSpace...");
    }
    IEnumerator SwapStep3Coroutine(GameObject numToBubbleSpace, GameObject numToSwapSpace,
                                    GameObject dataValueBubble, GameObject dataValueSwap,
                                    GameObject tempNumSpace, GameObject dataValueTemp)
    {
        yield return new WaitForSeconds(1.5f);

        dataValueTemp.transform.position = numToSwapSpace.transform.position;
        Debug.Log("Step 3: Moved tempValue to numSwapSpace...");
    }
    IEnumerator SwapStep4Coroutine(GameObject numToBubbleSpace, GameObject numToSwapSpace,
                                    GameObject dataValueBubble, GameObject dataValueSwap,
                                    GameObject tempNumSpace, GameObject dataValueTemp)
    {
        yield return new WaitForSeconds(2.0f);

        dataValueSwap.transform.position = numToBubbleSpace.transform.position;
        Debug.Log("Step 4: Moved dataValueSwap to numBubbleSpace...");
    }
    IEnumerator SwapStep5Coroutine(GameObject numToBubbleSpace, GameObject numToSwapSpace,
                                    GameObject dataValueBubble, GameObject dataValueSwap,
                                    GameObject tempNumSpace, GameObject dataValueTemp)
    {
        yield return new WaitForSeconds(2.5f);

        dataValueBubble.transform.position = numToSwapSpace.transform.position;
        Debug.Log("Step 5: Moved dataValueBubble back to numSwapSpace...");
    }
    IEnumerator SwapStep6Coroutine(GameObject numToBubbleSpace, GameObject numToSwapSpace,
                                    GameObject dataValueBubble, GameObject dataValueSwap,
                                    GameObject tempNumSpace, GameObject dataValueTemp)
    {
        yield return new WaitForSeconds(3.0f);

        dataValueTemp.transform.position = tempNumSpace.transform.position;
        Debug.Log("Step 6: Moved dataValueBubble to numSwapSpace...");
        yield return new WaitForSeconds(0.5f);


        Debug.Log("Swap has been performed!!!");
    }

    /* --> NOTE: NOT NECESSARY, THEREFORE NOT USED!!!
    IEnumerator SwapStep7Coroutine(GameObject numToBubbleSpace, GameObject numToSwapSpace,
                                    GameObject dataValueBubble, GameObject dataValueSwap,
                                    GameObject tempNumSpace, GameObject dataValueTemp)
    {
        yield return new WaitForSeconds(4.5f);
        dataValueTemp.transform.position = numToSwapSpace.transform.position;
        yield return new WaitForSeconds(0.5f);
        dataValueTemp.transform.position = tempSpace.transform.position;

        Debug.Log("Swap has been performed!!!");
    }
    */


    // Update is called once per frame
    void Update()
    {
        if (space0 != null && space0.GetComponent<BoxScript>().GetNumber() != null)
        {
            if (currentValues[0] == space0.GetComponent<BoxScript>().GetNumber())
            {
                //Do nothing
            }
            else if (currentValues[0] != space0.GetComponent<BoxScript>().GetNumber())
            {
                Debug.Log("Changing the value of index 0 in currentValues[]...");
                currentValues[0] = space0.GetComponent<BoxScript>().GetNumber();
            }
        }
        if (space1 != null && space1.GetComponent<BoxScript>().GetNumber() != null)
        {
            if (currentValues[1] == space1.GetComponent<BoxScript>().GetNumber())
            {
                //Do nothing
            }
            else if (currentValues[1] != space1.GetComponent<BoxScript>().GetNumber())
            {
                Debug.Log("Changing the value of index 1 in currentValues[]...");
                currentValues[1] = space1.GetComponent<BoxScript>().GetNumber();
            }
        }
        if (space2 != null && space2.GetComponent<BoxScript>().GetNumber() != null)
        {
            if (currentValues[2] == space2.GetComponent<BoxScript>().GetNumber())
            {
                //Do nothing
            }
            else if (currentValues[2] != space2.GetComponent<BoxScript>().GetNumber())
            {
                Debug.Log("Changing the value of index 2 in currentValues[]...");
                currentValues[2] = space2.GetComponent<BoxScript>().GetNumber();
            }
        }
        if (space3 != null && space3.GetComponent<BoxScript>().GetNumber() != null)
        {
            if (currentValues[3] == space3.GetComponent<BoxScript>().GetNumber())
            {
                //Do nothing
            }
            else if (currentValues[3] != space3.GetComponent<BoxScript>().GetNumber())
            {
                Debug.Log("Changing the value of index 3 in currentValues[]...");
                currentValues[3] = space3.GetComponent<BoxScript>().GetNumber();
            }
        }
        if (space4 != null && space4.GetComponent<BoxScript>().GetNumber() != null)
        {
            if (currentValues[4] == space4.GetComponent<BoxScript>().GetNumber())
            {
                //Do nothing
            }
            else if (currentValues[4] != space4.GetComponent<BoxScript>().GetNumber())
            {
                Debug.Log("Changing the value of index 4 in currentValues[]...");
                currentValues[4] = space4.GetComponent<BoxScript>().GetNumber();
            }
        }


        //When a number is bubbled to its correct position, set a boolean flag true
        //  to indicate is has reached the correct position--
        if (isSorted == false && space1.tag == "MemorySpace" && isBubbled1 == false)
        {
            if (space1.GetComponent<BoxScript>().GetNumber() == solutionValues[1])
            {
                int size = 5;
                int[] phaseArray = new int[size];
                for (int i = 0; i < size; i++)
                {
                    //Note: The first index of iterationPhases2D is the solved state when
                    //        20 is bubbled to the end of the data structure
                    //        --> Therefore, 8 is the first index
                    phaseArray[i] = iterationPhases2D[8, i];
                }

                bool correctlyBubbled = true;
                for (int i = 0; i < size; i++)
                {
                    if (phaseArray[i] != currentValues[i]) { correctlyBubbled = false; }
                }

                if (correctlyBubbled)
                {
                    Debug.Log("NUMBER HAS BEEN BUBBLED!!! - Iteration Completed...");
                    isBubbled1 = true;
                    isSorted = true;
                    iterationStatuses[2] = true;
                }

                
            }
        }

        if (iterationStatuses[0] == false && space0.tag == "MemorySpace" && isBubbled0 == false)
        {
            if (space0.GetComponent<BoxScript>().GetNumber() == solutionValues[0])
            {
                int size = 5;
                int[] phaseArray = new int[size];
                for (int i = 0; i < size; i++)
                {
                    //Note: The first index of iterationPhases2D is the solved state when
                    //        0 (1) is bubbled to the end of the data structure
                    //        --> Therefore, 0 is the first index
                    phaseArray[i] = iterationPhases2D[0, i];
                }

                bool correctlyBubbled = true;
                for (int i = 0; i < size; i++)
                {
                    if (phaseArray[i] != currentValues[i]) { correctlyBubbled = false; }
                }

                if (correctlyBubbled)
                {
                    Debug.Log("NUMBER HAS BEEN BUBBLED!!! - number 0 (1) at space 0");
                    isBubbled0 = true;
                }
            }
        }

        if (iterationStatuses[0] == false && space4.tag == "MemorySpace" && isBubbled4 == false)
        {
            if (space4.GetComponent<BoxScript>().GetNumber() == solutionValues[4])
            {
                int size = 5;
                int[] phaseArray = new int[size];
                for (int i = 0; i < size; i++)
                {
                    //Note: The first index of iterationPhases2D is the solved state when
                    //        1 (3) is bubbled to the end of the data structure
                    //        --> Therefore, 3 is the first index
                    phaseArray[i] = iterationPhases2D[3, i];
                }

                bool correctlyBubbled = true;
                for (int i = 0; i < size; i++)
                {
                    if (phaseArray[i] != currentValues[i]) { correctlyBubbled = false; }
                }

                if (correctlyBubbled)
                {
                    Debug.Log("NUMBER HAS BEEN BUBBLED!!! - Iteration Completed...");
                    isBubbled4 = true;
                    iterationStatuses[0] = true;
                }
            }
        }

        if (iterationStatuses[1] == false && space3.tag == "MemorySpace" && isBubbled3 == false)
        {
            if (space3.GetComponent<BoxScript>().GetNumber() == solutionValues[3])
            {
                int size = 5;
                int[] phaseArray = new int[size];
                for (int i = 0; i < size; i++)
                {
                    //Note: The first index of iterationPhases2D is the solved state when
                    //        1 (2) is bubbled to the end of the data structure
                    //        --> Therefore, 6 is the first index
                    phaseArray[i] = iterationPhases2D[6, i];
                }

                bool correctlyBubbled = true;
                for (int i = 0; i < size; i++)
                {
                    if (phaseArray[i] != currentValues[i]) { correctlyBubbled = false; }
                }

                if (correctlyBubbled)
                {
                    Debug.Log("NUMBER HAS BEEN BUBBLED!!! - Iteration Completed...");
                    isBubbled3 = true;
                    iterationStatuses[1] = true;
                }
            }
        }

        if (iterationStatuses[2] == false && space2.tag == "MemorySpace" && isBubbled2 == false)
        {
            if (space2.GetComponent<BoxScript>().GetNumber() == solutionValues[2])
            {
                int size = 5;
                int[] phaseArray = new int[size];
                for (int i = 0; i < size; i++)
                {
                    //Note: The first index of iterationPhases2D is the solved state when
                    //        1 (1) is bubbled to the end of the data structure
                    //        --> Therefore, 8 is the first index
                    phaseArray[i] = iterationPhases2D[8, i];
                }

                bool correctlyBubbled = true;
                for (int i = 0; i < size; i++)
                {
                    if (phaseArray[i] != currentValues[i]) { correctlyBubbled = false; }
                }

                if (correctlyBubbled)
                {
                    Debug.Log("NUMBER HAS BEEN BUBBLED!!! - number 1 (1) at space 2");
                    isBubbled2 = true;
                    iterationStatuses[2] = true;
                }
            }
        }


    }//End of Update()

    
    public int[] GetCurrentPhaseValues()
    {
        int size = 5;
        int[] phaseValues = new int[size];
        for (int i = 0; i < 5; i++)
        {
            //ERROR THROWN: IndexOutOfRangeException: Index was outside the bounds of the array.
            //  - DataStructureManager.GetCurrentPhaseValues () (at DataStructureManager.cs:219)
            //  - LevelStartCoroutine:Update() (at LevelStartCoroutine.cs:893)
            phaseValues[i] = iterationPhases2D[iterationPhasesPosition, i];
        }
        return phaseValues;
    }
    

    public void SetInvisible()
    {
        isVisible = false;
        SpriteRenderer[] children = gameObject.GetComponentsInChildren<SpriteRenderer>();
        int i = 0;
        while (i < children.Length)
        {
            if (children[i].TryGetComponent(typeof(SpriteRenderer),
                                                                out Component component) != null)
            {
                SpriteRenderer child = children[i];
                child.enabled = false;
            }
            i++;
        }
        //gameObject.GetComponent<SpriteRenderer>().enabled = false;
    }

    public void SetVisible()
    {
        isVisible = true;
        SpriteRenderer[] children = gameObject.GetComponentsInChildren<SpriteRenderer>();
        int i = 0;
        while (i < children.Length)
        {
            if (children[i].TryGetComponent(typeof(SpriteRenderer),
                                                                out Component component) != null)
            {
                SpriteRenderer child = children[i];
                child.enabled = true;
            }
            i++;
        }
        //gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }


    //TO_DO: Need to implement methods for allowing the player to interact
    //  with and select the numbers in the data structure
    //  --> A method for selecting the index we want to be swaped (the start index)
	//  --> A method for selecting the index we want to swap with (the swap index)
    //  --> A method for allowing the moving of the selected indices and finalization
    //        of the swap for that step.
    //
    //  - We also need to implement methods for identifying correct answers and
    //      incorrect answers
    //    --> Then, we need methods for showing the correct answer and completing
    //          the solution when the player chooses the wrong answer
    //    -->In addition, we need methods for rewarding or punishing the player
    //         when they answer correctly or incorrectly respectively

    //GetAnswerForComparison(int):
    public bool GetAnswerForComparison(int answer, int subStep)
    {
        if (subStep == iterationStage)
        {
            //Compare player's answer choice with the answers for question1and3:
            if (iterationStage == 1 || iterationStage == 3)
            {
                //If answer if correct, return true--
                if (answer == question1and3Answers[question1and3Position])
                {
                    Debug.Log("CORRECT ANSWER!!! - " + answer + " = "
                                + question1and3Answers[question1and3Position]);

                    //Move down the list of question1and3Answers in preparation for when
                    //  the next question1 or question3 is asked and answered
                    if (question1and3Position < question1and3Answers.Length)
                    {
                        question1and3Position++;
                    }
                    else
                    {
                        question1and3Position = -1;
                    }

                    return true;
                }
                //If answer is incorrect, return false--
                else
                {
                    Debug.Log("INCORRECT ANSWER - " + answer + " != "
                                    + question1and3Answers[question1and3Position]);

                    //Do not increment the question1and3Position since the player
                    //  did not answer correctly yet

                    return false;
                }
            }
            //Compare player's answer choice with the answer for question2:
            else if (iterationStage == 2)
            {
                //If answer is correct, return true--
                if (answer == question2Answers[question2Position])
                {
                    Debug.Log("CORRECT ANSWER!!! - " + answer + " = "
                                    + question2Answers[question2Position]);
                    //Move down the list of question2Answers in preparation for when
                    //  the next question2 is asked and answered
                    if (question2Position < question2Answers.Length)
                    {
                        question2Position++;
                    }
                    else
                    {
                        question2Position = -1;
                    }
                    return true;
                }
                //If answer is incorrect, return false--
                else
                {
                    Debug.Log("INCORRECT ANSWER - " + answer + " != "
                                    + question2Answers[question2Position]);

                    //Do not increment the question2Position since the player
                    //  did not answer correctly yet

                    return false;
                }
            }
            //Return false if the iteration stage is a number other than 1, 2, or 3:
            else { return false; }
        }
        //Return false if the subStep != iterationStage:
        else { return false; }
    }//End of GetAnswerForComparison(int)

    
    //GetAnswerForComparison(bool):
    public bool GetAnswerForComparison(bool answer, int subStep)
    {
        if (subStep == iterationStage)
        {
            //Compare player's answer choice with the answers for question4:
            if (iterationStage == 3)
            {
                //If answer is correct, return true--
                if (answer == question4Answers[question4Position])
                {
                    Debug.Log("CORRECT ANSWER!!! - " + answer + " = "
                                + question4Answers[question4Position]);

                    //Move down the list of question4Answers in preparation for when
                    //  the next question4 is asked and answered
                    if (question4Position < question4Answers.Length)
                    {
                        question4Position++;
                    }
                    else
                    {
                        question4Position = -1;
                    }

                    return true;
                }
                //If answer is incorrect, return false--
                else
                {
                    Debug.Log("INCORRECT ANSWER - " + answer + " != "
                                + question4Answers[question4Position]);

                    //Do not increment the question4Position since the player
                    //  did not answer correctly yet

                    return false;
                }
            }
            else //Compare player's answer choice with the answers for question5:
            if (iterationStage == 5)
            {
                //If answer is correct, return true--
                if (answer == question5Answers[question5Position])
                {
                    Debug.Log("CORRECT ANSWER!!! - " + answer + " = "
                                + question5Answers[question5Position]);

                    //Move down the list of question5Answers in preparation for when
                    //  the next question5 is asked and answered
                    if (question5Position < question5Answers.Length)
                    {
                        question5Position++;
                    }
                    else
                    {
                        question5Position = -1;
                    }

                    return true;
                }
                //If answer is incorrect, return false--
                else
                {
                    Debug.Log("INCORRECT ANSWER - " + answer + " != "
                                + question5Answers[question5Position]);

                    //Do not increment the question5Position since the player
                    //  did not answer correctly yet

                    return false;
                }
            }
            //Return false if the iterationStage is a number other than 4:
            else { return false; }
        }
        //Return false if the subStep != iterationStage:
        else { return false; }
    }//End of GetAnswerForComparison(bool)
    

    
    //GetAnswerForComparison(int[]):
    public bool GetAnswerForComparison(int[] answerArray, int subStep)
    {
        if (subStep == iterationStage)
        {
            //Compare player's answer choice with the answers for question4:
            if (iterationStage == 4)
            {
                bool isCorrect = true;
                //If answer is correct, return true--
                for (int i = 0; i < currentValues.Length; i++)
                {
                    if (answerArray[i] != currentValues[i])
                    {
                        isCorrect = false;
                    }
                }
                if (isCorrect)
                {
                    

                    //Move down the list of iterationPhasesPosition in preparation for when
                    //  the next iterationPhase is asked for and matches correctly
                    if (iterationPhasesPosition < iterationPhases2D.Length)
                    {
                        iterationPhasesPosition++;
                    }
                    else
                    {
                        iterationPhasesPosition = -1;
                    }

                    return isCorrect;
                }
                //If answer is incorrect, return false--
                else
                {
                    

                    //Do not increment the iterationPhasesPosition since the player
                    //  did not answer correctly yet

                    return false;
                }
            }
            //Return false if the iterationPhase's state is not correct:
            else { return false; }
        }
        //Return false if the subStep != iterationStage:
        else { return false; }
    }//End of GetAnswerForComparison(bool)
    


    //SetIterationStageTo(int):
    //  - Makes the "iterationStage" equal to the int parameter that is passed
    public void SetIterationStageTo(int stageNum) { iterationStage = stageNum; }


    //IncrementIterationStageFrom():
    //  - Moves the int variable "iterationStage" up by 1 when a stage in the
    //      current iteration is completed
    public void IncrementIterationStageFrom(int stageNum)
    {
        if (stageNum == 1)
        {
            iterationStage = 2;
        }
        else if (stageNum == 2)
        {
            iterationStage = 3;
        }
        else if (stageNum == 3)
        {
            iterationStage = 4;
        }
        else if (stageNum == 4)
        {
            iterationStage = 5;
        }
        else if (stageNum == 5)
        {
            iterationStage = 0;
        }
        else if (stageNum == 0)
        {
            iterationStage = 1;
        }
    }
    

    //IncrementCurrentIterationFrom():
    //  - Moves the int variable "iterationNum" up by 1 when an iteration of
    //      the sorting problem is solved
    //    - i.e. the min or max value has been "bubbled" to the end of the
    //        data structure
    public void IncrementCurrentIterationFrom(int currentNum)
    {
        if (currentNum == 1)
        {
            currentIteration = 2;
        }
        else if (currentNum == 2)
        {
            currentIteration = 3;
        }
        else if (currentNum == 3)
        {
            currentIteration = 0; //When iterationNum is 0, this indicates that
                                  //  the sorting problem is solved
        }
    }
    


    //Other Behaviour (Get Methods)

    //GetIterationNum():
	//  - returns the iteration (current step of solving the sorting problem)
    public int GetIterationNum()
    {
        return currentIteration;
    }

    //GetIterationStage():
    //  - returns the iteration stage (current sub-step of solving the sorting
	//      problem's main iteration)
    public int GetIterationStage()
    {
        return iterationStage;
    }

    //GetIterationList():
    //  - returns an array of ints representing the total number of iterations needed
    //      to solve the sorting problem
    public int[] GetIterationList()
    {
        return iterations;
    }

    //GetIterationPhaseStatus():
    //  - returns the boolean flag "isBubbled" to indicate whether the current
    //      iteration phase has been completed or not
    public bool GetIterationStatus(int iteration)
    {
        //Check if a valid iteration num was passed--
        bool validNum = false;
        for (int i = 0; i < iterations.Length; i++)
        {
            if (iteration == iterations[i]) { validNum = true; }
        }

        //If a valid iteration num was passed, check if the iteration is solved--
        if (validNum && iterationStatuses[iteration - 1] == true)
        {
            Debug.Log("A valid number for current iteration was provided: " + iteration);
            return iterationStatuses[iteration - 1];
        }
        //If not a valid iteration num OR iteration not solved yet, return false
        else
        {
            Debug.Log("INVALID iteration number provided; GetIterationStatus() returns false...");
            return false;
        }
    }

    //GetCompletionStatus():
    //  - returns the boolean flag "isSorted" to indicate whether the sorting
    //      problem has been solved or not
    public bool GetCompletionStatus()
    {
        return isSorted;
    }
}
