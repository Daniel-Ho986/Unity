using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStartCoroutine : MonoBehaviour
{
    public PromptManager promptManager;
    public DataStructureManager dataManager;

    public int mainStep;
    public int mainSubStep;


    private bool introFinished;
    private bool sortStarted;

    private bool subStepIsStarting;
    private bool subStepIsEnding;
    private bool firstTimeSortCoroutine;
    private bool firstTimeBattleCoroutine;
    private bool isTimeToFight;
    private bool isTimeToSolve;

    private bool isFirstSwap;
    private bool isSubStep1;
    private bool isSubStep2;
    private bool isSubStep3;
    private bool isSubStep4;
    private bool isSubStep0; //FINISH HIM!!!

    private bool currentMainStepSolved;
    private bool problemSolved; //Becomes true when the data has been completely sorted




    //DisplayIntroCoroutine():
	//  - Plays out the sequence of events that display various intro text prompts
    IEnumerator DisplayIntroCoroutine()
    {
        promptManager.ShowTextBoxIntro();
        yield return new WaitForSeconds(2.0f); //Wait and then continue
        promptManager.HideTextBoxIntro();
        yield return new WaitForSeconds(0.5f); //Wait and then continue
        promptManager.ShowTextBoxInstructions();
        yield return new WaitForSeconds(1.0f); //Wait and then continue
        introFinished = true;

    }


    //DisplayMainCoroutine():
    //  - Plays out the sequence of events that display various text prompts
    //      during the sorting and battle phases of the level
    //
    // *TO_DO_LIST*
    //  --> need to implement some of the methods in DataStructureManager
    //  --> double check that all static variables in this class are initialized at start
    //  --> check for other stuff to get done (such as animations and timing)
    IEnumerator SortCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        mainStep = dataManager.GetIterationNum(); //Iteration number
        mainSubStep = dataManager.GetIterationStage(); //The current step in iteration

        if (firstTimeSortCoroutine)
        {
            firstTimeSortCoroutine = false;
            isSubStep1 = true;
        }
        else if (mainSubStep == 1)
        {
            if (subStepIsStarting) //Display question 1 (this is done once)
            {
                Debug.Log("Now in step 1 of current iteration...");
                subStepIsStarting = false;
                promptManager.ShowTextBoxQuestion1();
            }

            //TO_DO: Enable the player to answer question1
            //  -->IF they answer correctly, continue to sub step 2
            //  -->IF they answer incorrectly, enemy gets opportunity to attack
            //       - After the enemy attacks (and the player possibly dodges),
            //           show the correct answer (the right action to make)
            //       - After the correct answer is shown/performed, then the
            //           player continues to sub step 2
            //  --> Somewhere, we will need to make "subStepIsEnding" = true

            if (subStepIsEnding) //Hide question 1 (this is done once after
            {                    //  sub step 1 is answered)
                Debug.Log("Ending step 1 of current iteration...");
                subStepIsEnding = false;
                //promptManager.HideTextBoxQuestion1();
                //dataManager.IncrementIterationStageFrom(mainSubStep);

                subStepIsStarting = true;
                isSubStep2 = true;
            }
            else //Keep updating as sub step 1 until the step is completed
            {
                isSubStep1 = true;
            }
            
        }

        //NOTE: Make the same changes to the below else if statements as in
        //        the sub step 1 if statement
        else if (isFirstSwap == true && mainSubStep == 2)
        {
            if (subStepIsStarting) //Display question 2 (this is done once)
            {
                Debug.Log("Now in step 2 of current iteration...");
                subStepIsStarting = false;
                promptManager.ShowTextBoxQuestion2();
            }

            //TO_DO: Enable the player to answer question2
            //  -->IF they answer correctly, continue to sub step 3
            //  -->IF they answer incorrectly, enemy gets opportunity to attack
            //       - After the enemy attacks (and the player possibly dodges),
            //           show the correct answer (the right action to make)
            //       - After the correct answer is shown/performed, then the
            //           player continues to sub step 3
            //  --> Somewhere, we will need to make "subStepIsEnding" = true

            if (subStepIsEnding) //Hide question 2 (this is done once after
            {                    //  sub step 2 is answered)
                Debug.Log("Ending step 2 of current iteration...");
                subStepIsEnding = false;
                //promptManager.HideTextBoxQuestion2();
                //dataManager.IncrementIterationStageFrom(mainSubStep);
                isSubStep3 = true;
                isFirstSwap = false;
            }
            else //Keep updating as sub step 2 until the step is completed
            {
                isSubStep2 = true;
            }

        }

        else if (isFirstSwap == false && mainSubStep == 2)
        {
            if (subStepIsStarting) //Display question 3 (this is done once)
            {
                Debug.Log("Now in step 2 of current iteration...");
                subStepIsStarting = false;
                promptManager.ShowTextBoxQuestion3();
            }

            //TO_DO: Enable the player to answer question2
            //  -->IF they answer correctly, continue to sub step 3
            //  -->IF they answer incorrectly, enemy gets opportunity to attack
            //       - After the enemy attacks (and the player possibly dodges),
            //           show the correct answer (the right action to make)
            //       - After the correct answer is shown/performed, then the
            //           player continues to sub step 3
            //  --> Somewhere, we will need to make "subStepIsEnding" = true

            if (subStepIsEnding) //Hide question 3 (this is done once after
            {                    //  sub step 3 is answered)
                Debug.Log("Ending step 2 of current iteration...");
                subStepIsEnding = false;
                //promptManager.HideTextBoxQuestion3();
                //dataManager.IncrementIterationStageFrom(mainSubStep);
                isSubStep3 = true;
            }
            else //Keep updating as sub step 2 until the step is completed
            {
                isSubStep2 = true;
            }
        }

        else if (mainSubStep == 3) 
        {
            if (subStepIsStarting) // Display swap prompt(this is done once)
            {
                Debug.Log("Now in step 3 of current iteration...");
                subStepIsStarting = false;
                //promptManager.ShowTextBoxSwapIt();
            }

            //TO_DO: Enable the player to swap the two indices
            //  -->IF they answer correctly, end sort phase and enter battle phase
            //  -->IF they answer incorrectly, enemy gets opportunity to attack
            //       - After the enemy attacks (and the player possibly dodges),
            //           show the correct answer (the right action to make)
            //       - After the correct answer is shown/performed, then the
            //           sort phase ends and the player enters the battle phase

            if (subStepIsEnding)
            {
                Debug.Log("Ending step 3 of current iteration...");
                subStepIsEnding = false;
                //promptManager.HideTextBoxSwapIt();

                //NOTE: The sub step should increment from 3 to 4, but really
                //        there is no sub step 4. Sub step 4 acts as a placed holder
                //        to prevent any of the sub steps from repeating over again.
                //dataManager.IncrementIterationStageFrom(mainSubStep);

                currentMainStepSolved = true;
            }
            else //Keep updating as sub step 3 until the step is completed
            {
                isSubStep3 = true;
            }
        }

        else if (currentMainStepSolved)
        {
            Debug.Log("Current iteration has been solved! Moving on to battle phase...");
            currentMainStepSolved = false;
            isTimeToSolve = false;

            //dataManager.ShowTextBoxCombatStart();
            //dataManager.HideTextBoxCombatStart();
            isTimeToFight = true;
            
        }

        //NOTE: Need to set mainSubStep to 0 after the battle phase ends
        else if (mainSubStep == 0)
        {
            //dataManager.IncrementIterationStageFrom(mainSubStep);

            //dataManager.IncrementCurrentIterationFrom(mainStep);
            isSubStep1 = true;
        }
        

    }//End of DisplayMainCoroutine()




    // Start is called before the first frame update
    void Start()
    {
        introFinished = false;
        sortStarted = false;

        subStepIsStarting = false;
        subStepIsEnding = false;
        firstTimeSortCoroutine = true;
        firstTimeBattleCoroutine = true;
        isTimeToFight = false;
        isTimeToSolve = true;

        mainStep = 0;
        mainSubStep = 0;

        isFirstSwap = true;
        isSubStep1 = false;
        isSubStep2 = false;
        isSubStep3 = false;
        isSubStep4 = false;
        isSubStep0 = false;

        currentMainStepSolved = false;
        problemSolved = false;

        StartCoroutine(DisplayIntroCoroutine());
    }




    // Update is called once per frame
    void Update()
    {
        //Only performed once at the start of the level
        if (introFinished)
        {
            //Only performed once after the intro and instructions prompts are shown
            if (sortStarted != true)
            {
                sortStarted = true; //Disable this loop permanently
                subStepIsStarting = true;
                StartCoroutine(SortCoroutine()); //Begin main phase of level
            }
        }

        //Begin sorting phase--
        if (isTimeToSolve)
        {
            //Checking for the current sub-step of the main iteration
            if (isSubStep1)
            {
                isSubStep1 = false;
                StartCoroutine(SortCoroutine());
            }
            else if (isSubStep2)
            {
                isSubStep2 = false;
                StartCoroutine(SortCoroutine());
            }
            else if (isSubStep3)
            {
                isSubStep3 = false;
                StartCoroutine(SortCoroutine());
            }
            else if (isSubStep4) //There really isn't a sub step 4, but it
            {                    //  transitions from the sort phase to the battle phase.
                isSubStep4 = false;
                StartCoroutine(SortCoroutine());
            }
            else if (isSubStep0) //Acts as the means of transitioning from battle phase
            {                    //  to the sort phase once again.
                isSubStep0 = false;
                StartCoroutine(SortCoroutine());
            }

        }//End of sorting phase--

        //Begin of battle phase--
        else if (isTimeToFight)
        {

        }//End of battle phase--


        //We might want to put this above the "isTimeToSolve" if statement, and
        // make that statement an else if statement instead.
        else if (problemSolved)
        {
            //if (enemyAlive) { }
                //Shift permanetly to battle phase if enemy is still alive
            //else if (enemyDefeated) { }
                //Display victory screen with results/rewards earned
        }
    }//End of Update()

    
}//End of LevelStartCoroutine
