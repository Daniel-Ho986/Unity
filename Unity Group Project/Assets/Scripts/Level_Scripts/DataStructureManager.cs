using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface DataStructureManager
{
    /* ***State Variables of a DataStructureManager:***
    
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
    private int[] question2Answers;    //Question2: Which index do we swap with?
    private bool[] question4Answers;   //Question4: Do we continue swapping or do we end this iteration?
                                       //           i.e. Is the value we started with in it's final position?

    private int question1and3Position; //The position within the list of question1and3Answers that needs to be
                                       //  correctly answered before advancing onward
    private int question2Position;     //The position within the list of question2Answers that needs to be
                                       //  correctly answered before advancing onward
    private int question4Position;     //The position within the list of question4Answer that needs to be
                                       //  correctly answered before advancing onward


    //Current and final states of the data structure:
    private int[] solutionValues;
    private int[] currentValues;

    private int[,] iterationPhases2D;
    private int iterationPhasesPosition;
    */




    //Swapping Methods:
    void PerformSwap(GameObject numToBubbleSpace, GameObject numToSwapSpace, GameObject tempNumSpace);
    /*
    IEnumerator SwapStep1Coroutine(GameObject numToBubbleSpace, GameObject numToSwapSpace,
                                    GameObject dataValueBubble, GameObject dataValueSwap,
                                    GameObject tempNumSpace, GameObject dataValueTemp);

    IEnumerator SwapStep2Coroutine(GameObject numToBubbleSpace, GameObject numToSwapSpace,
                                    GameObject dataValueBubble, GameObject dataValueSwap,
                                    GameObject tempNumSpace, GameObject dataValueTemp);

    IEnumerator SwapStep3Coroutine(GameObject numToBubbleSpace, GameObject numToSwapSpace,
                                    GameObject dataValueBubble, GameObject dataValueSwap,
                                    GameObject tempNumSpace, GameObject dataValueTemp);

    IEnumerator SwapStep4Coroutine(GameObject numToBubbleSpace, GameObject numToSwapSpace,
                                    GameObject dataValueBubble, GameObject dataValueSwap,
                                    GameObject tempNumSpace, GameObject dataValueTemp);

    IEnumerator SwapStep5Coroutine(GameObject numToBubbleSpace, GameObject numToSwapSpace,
                                    GameObject dataValueBubble, GameObject dataValueSwap,
                                    GameObject tempNumSpace, GameObject dataValueTemp);

    IEnumerator SwapStep6Coroutine(GameObject numToBubbleSpace, GameObject numToSwapSpace,
                                    GameObject dataValueBubble, GameObject dataValueSwap,
                                    GameObject tempNumSpace, GameObject dataValueTemp);
    */


    int[] GetCurrentPhaseValues();

    void SetInvisible();

    void SetVisible();

    //GetAnswerForComparison(int):
    bool GetAnswerForComparison(int answer, int subStep);

    //GetAnswerForComparison(bool):
    bool GetAnswerForComparison(bool answer, int subStep);

    //GetAnswerForComparison(int[]):
    bool GetAnswerForComparison(int[] answerArray, int subStep);

    //SetIterationStageTo(int):
    //  - Makes the "iterationStage" equal to the int parameter that is passed
    void SetIterationStageTo(int stageNum);

    //IncrementIterationStageFrom():
    //  - Moves the int variable "iterationStage" up by 1 when a stage in the
    //      current iteration is completed
    void IncrementIterationStageFrom(int stageNum);

    //IncrementCurrentIterationFrom():
    //  - Moves the int variable "iterationNum" up by 1 when an iteration of
    //      the sorting problem is solved
    //    - i.e. the min or max value has been "bubbled" to the end of the
    //        data structure
    void IncrementCurrentIterationFrom(int currentNum);


    //Other Behaviour (Get Methods)

    //GetIterationNum():
    //  - returns the iteration (current step of solving the sorting problem)
    int GetIterationNum();

    //GetIterationStage():
    //  - returns the iteration stage (current sub-step of solving the sorting
    //      problem's main iteration)
    int GetIterationStage();

    //GetIterationList():
    //  - returns an array of ints representing the total number of iterations needed
    //      to solve the sorting problem
    int[] GetIterationList();

    //GetIterationPhaseStatus():
    //  - returns the boolean flag "isBubbled" to indicate whether the current
    //      iteration phase has been completed or not
    bool GetIterationStatus(int iteration);

    //GetCompletionStatus():
    //  - returns the boolean flag "isSorted" to indicate whether the sorting
    //      problem has been solved or not
    bool GetCompletionStatus();

}
