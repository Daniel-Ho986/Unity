using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStructureManager : MonoBehaviour
{
    private int currentIteration;
    private int iterationStage;
    private int[] iterations;
    private bool isSorted;

    // Start is called before the first frame update
    void Start()
    {
        currentIteration = 1; //The main loop iteration number of the sorting algorithm
        iterationStage = 1; //The nested loop iteration number of the sorting algorithm
        iterations = new int[] { 1, 2, 3, 4 }; //An array of all the main loop iterations
        isSorted = false;
    }

    // Update is called once per frame
    void Update()
    {
        
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
            currentIteration = 4;
        }
        else if (currentNum == 4)
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

    //GetCompletionStatus():
    //  - returns the boolean flag "isSorted" to indicate whether the sorting
    //      problem has been solved or not
    public bool GetCompletionStatus()
    {
        return isSorted;
    }
}
