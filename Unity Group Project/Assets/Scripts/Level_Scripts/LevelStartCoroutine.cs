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
    private bool isWaitingForChoice;
    private bool isWaitingForConfirm;

    private bool isFirstSwap;
    private bool isSubStep1;
    private bool isSubStep2;
    private bool isSubStep3;
    private bool isSubStep4;
    private bool isSubStep5;
    private bool isSubStep0; //FINISH HIM!!!

    private bool currentMainStepSolved;
    private bool iterationPhaseSolved;
    private bool problemSolved; //Becomes true when the data has been completely sorted

    private GameObject selectReticle;




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


//SortCoroutine():
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

/*subStep1*/
        else if (mainSubStep == 1)
        {
            if (subStepIsStarting) //Display question 1 (this is done once)
            {
                Debug.Log("Now in step 1 of current iteration...");
                subStepIsStarting = false;
                promptManager.ShowTextBoxQuestion1();
                yield return new WaitForSeconds(0.5f);
                dataManager.GetComponent<ReticleController>().SetActive();
                isWaitingForChoice = true;
                isWaitingForConfirm = false;
            }


            //TO_DO: Enable the player to answer question1
            //  -->IF they answer correctly, continue to sub step 2
            //  -->IF they answer incorrectly, enemy gets opportunity to attack
            //       - After the enemy attacks (and the player possibly dodges),
            //           show the correct answer (the right action to make)
            //       - After the correct answer is shown/performed, then the
            //           player continues to sub step 2
            //  --> Somewhere, we will need to make "subStepIsEnding" = true
            if (selectReticle.GetComponent<ReticleScript>().GetConfirm())
            {
                selectReticle.GetComponent<ReticleScript>().SetFinalChoice(false);
                selectReticle.GetComponent<ReticleScript>().SetConfirm(false);
                //removed statement: dataManager.GetComponent<ReticleController>().SetInactive();

                //Check if the confirmed final choice is correct or wrong:
                //  - If the choice is correct, show that the choice was correct
                //      with some visual and sound feedback
                //    --> Checking for the correctness needs to be implemented
                //  - Increment the player's power meter as a result of answering
                //      the question correctly
                //  - Then, make "subStepIsEnding" = true so that the next question
                //      can be viewed and answered
                int answerToCheck = selectReticle.GetComponent<ReticleScript>().GetAnswerInt();
                bool isCorrectAnswer = dataManager.GetAnswerForComparison(answerToCheck, mainSubStep);
                if (isCorrectAnswer == true)
                {
                    subStepIsEnding = true;
                }

                //  - If the choice is incorrect, show that the choice was incorrect
                //      with some visual and sound feedback
                //    --> Checking for correctness needs to be implemented
                //  - Make the enemy attack the player and allow the player to dodge
                //  - Then, "subStepIsStarting" = true so that the current question
                //      can be viewed and answered again
                else if (isCorrectAnswer == false)
                {
                    dataManager.GetComponent<ReticleController>().SetInactive();
                    dataManager.SetInvisible();
                }
                
            }
            else if (selectReticle.GetComponent<ReticleScript>().GetDecline())
            {
                selectReticle.GetComponent<ReticleScript>().SetFinalChoice(false);
                selectReticle.GetComponent<ReticleScript>().SetDecline(false);
                //Do nothing and allow the player to continue making a decision as to
                //  what their final choice should be
            }


            //When the player makes a temporary choice via using the selectReticle, show the
            //  text prompt that asks for confirmation of final answer choice:
            if (selectReticle.GetComponent<ReticleScript>().GetFinalChoice() == true
                    && isWaitingForChoice == true)
            {
                isWaitingForConfirm = true; //Prevents update from calling repeatedly
                isWaitingForChoice = false;
                promptManager.ShowTextBoxActionConfirm();
            }
            //When the player decides not to make their temporary choice the final answer choice,
            //  then make the confirmation text prompt disappear (so that they may choose a
            //  different temporary choice):
            else if (selectReticle.GetComponent<ReticleScript>().GetFinalChoice() == false
                    && isWaitingForConfirm == true)
            {
                isWaitingForChoice = true; //Prevents update from calling repeatedly
                isWaitingForConfirm = false;
                promptManager.HideTextBoxActionConfirm();
            }


            if (subStepIsEnding) //Hide question 1 (this is done once after
            {                    //  sub step 1 is answered)
                Debug.Log("Ending step 1 of current iteration...");
                subStepIsEnding = false;
                promptManager.HideTextBoxQuestion1();
                dataManager.IncrementIterationStageFrom(mainSubStep);

                subStepIsStarting = true;
                isSubStep2 = true;
            }
            else //Keep updating as sub step 1 until the step is completed
            {
                isSubStep1 = true;
            }
            
        }

/*subStep2*/
        //NOTE: Make the same changes to the below else if statements as in
        //        the sub step 1 if statement
        //UPDATE NOTE: Copy, paste, and edit the statements for checking answer correctness,
        //               as well as making other necessary changes
        else if (isFirstSwap == true && mainSubStep == 2)
        {
            if (subStepIsStarting) //Display question 2 (this is done once)
            {
                Debug.Log("Now in step 2 of current iteration...");
                subStepIsStarting = false;
                promptManager.ShowTextBoxQuestion2();
                yield return new WaitForSeconds(0.5f);
                dataManager.GetComponent<ReticleController>().SetActive();
                isWaitingForChoice = true;
                isWaitingForConfirm = false;
            }


            //TO_DO: Enable the player to answer question2
            //  -->IF they answer correctly, continue to sub step 3
            //  -->IF they answer incorrectly, enemy gets opportunity to attack
            //       - After the enemy attacks (and the player possibly dodges),
            //           show the correct answer (the right action to make)
            //       - After the correct answer is shown/performed, then the
            //           player continues to sub step 3
            //  --> Somewhere, we will need to make "subStepIsEnding" = true
            if (selectReticle.GetComponent<ReticleScript>().GetConfirm())
            {
                selectReticle.GetComponent<ReticleScript>().SetFinalChoice(false);
                selectReticle.GetComponent<ReticleScript>().SetConfirm(false);
                //removed statement: dataManager.GetComponent<ReticleController>().SetInactive();

                //Check if the confirmed final choice is correct or wrong:
                //  - If the choice is correct, show that the choice was correct
                //      with some visual and sound feedback
                //    --> Checking for the correctness needs to be implemented
                //  - Increment the player's power meter as a result of answering
                //      the question correctly
                //  - Then, make "subStepIsEnding" = true so that the next question
                //      can be viewed and answered
                int answerToCheck = selectReticle.GetComponent<ReticleScript>().GetAnswerInt();
                bool isCorrectAnswer = dataManager.GetAnswerForComparison(answerToCheck, mainSubStep);
                if (isCorrectAnswer == true)
                {
                    subStepIsEnding = true;
                }

                //  - If the choice is incorrect, show that the choice was incorrect
                //      with some visual and sound feedback
                //    --> Checking for correctness needs to be implemented
                //  - Make the enemy attack the player and allow the player to dodge
                //  - Then, "subStepIsStarting" = true so that the current question
                //      can be viewed and answered again
                else if (isCorrectAnswer == false)
                {
                    dataManager.GetComponent<ReticleController>().SetInactive();
                    dataManager.SetInvisible();
                }

            }
            else if (selectReticle.GetComponent<ReticleScript>().GetDecline())
            {
                selectReticle.GetComponent<ReticleScript>().SetFinalChoice(false);
                selectReticle.GetComponent<ReticleScript>().SetDecline(false);
                //Do nothing and allow the player to continue making a decision as to
                //  what their final choice should be
            }


            //When the player makes a temporary choice via using the selectReticle, show the
            //  text prompt that asks for confirmation of final answer choice:
            if (selectReticle.GetComponent<ReticleScript>().GetFinalChoice() == true
                    && isWaitingForChoice == true)
            {
                isWaitingForConfirm = true; //Prevents update from calling repeatedly
                isWaitingForChoice = false;
                promptManager.ShowTextBoxActionConfirm();
            }
            //When the player decides not to make their temporary choice the final answer choice,
            //  then make the confirmation text prompt disappear (so that they may choose a
            //  different temporary choice):
            else if (selectReticle.GetComponent<ReticleScript>().GetFinalChoice() == false
                    && isWaitingForConfirm == true)
            {
                isWaitingForChoice = true; //Prevents update from calling repeatedly
                isWaitingForConfirm = false;
                promptManager.HideTextBoxActionConfirm();
            }


            if (subStepIsEnding) //Hide question 2 (this is done once after
            {                    //  sub step 2 is answered)
                Debug.Log("Ending step 2 of current iteration...");
                subStepIsEnding = false;
                promptManager.HideTextBoxQuestion2();
                dataManager.IncrementIterationStageFrom(mainSubStep);

                subStepIsStarting = true;
                isSubStep3 = true;
                isFirstSwap = false;
            }
            else //Keep updating as sub step 2 until the step is completed
            {
                isSubStep2 = true;
            }

        }

/*subStep2(Question3)*/
        //CONTINUE PASTING CODE HERE!!!
        else if (isFirstSwap == false && mainSubStep == 2)
        {
            if (subStepIsStarting) //Display question 3 (this is done once)
            {
                Debug.Log("Now in step 2 of current iteration...");
                subStepIsStarting = false;
                promptManager.ShowTextBoxQuestion3();
                yield return new WaitForSeconds(0.5f);
                dataManager.GetComponent<ReticleController>().SetActive();
                isWaitingForChoice = true;
                isWaitingForConfirm = false;
            }

            //TO_DO: Enable the player to answer question2
            //  -->IF they answer correctly, continue to sub step 3
            //  -->IF they answer incorrectly, enemy gets opportunity to attack
            //       - After the enemy attacks (and the player possibly dodges),
            //           show the correct answer (the right action to make)
            //       - After the correct answer is shown/performed, then the
            //           player continues to sub step 3
            //  --> Somewhere, we will need to make "subStepIsEnding" = true
            if (selectReticle.GetComponent<ReticleScript>().GetConfirm())
            {
                selectReticle.GetComponent<ReticleScript>().SetFinalChoice(false);
                selectReticle.GetComponent<ReticleScript>().SetConfirm(false);
                //removed statement: dataManager.GetComponent<ReticleController>().SetInactive();

                //Check if the confirmed final choice is correct or wrong:
                //  - If the choice is correct, show that the choice was correct
                //      with some visual and sound feedback
                //    --> Checking for the correctness needs to be implemented
                //  - Increment the player's power meter as a result of answering
                //      the question correctly
                //  - Then, make "subStepIsEnding" = true so that the next question
                //      can be viewed and answered
                int answerToCheck = selectReticle.GetComponent<ReticleScript>().GetAnswerInt();
                bool isCorrectAnswer = dataManager.GetAnswerForComparison(answerToCheck, mainSubStep);
                if (isCorrectAnswer == true)
                {
                    subStepIsEnding = true;
                }

                //  - If the choice is incorrect, show that the choice was incorrect
                //      with some visual and sound feedback
                //    --> Checking for correctness needs to be implemented
                //  - Make the enemy attack the player and allow the player to dodge
                //  - Then, "subStepIsStarting" = true so that the current question
                //      can be viewed and answered again
                else if (isCorrectAnswer == false)
                {
                    dataManager.GetComponent<ReticleController>().SetInactive();
                    dataManager.SetInvisible();
                }

            }
            else if (selectReticle.GetComponent<ReticleScript>().GetDecline())
            {
                selectReticle.GetComponent<ReticleScript>().SetFinalChoice(false);
                selectReticle.GetComponent<ReticleScript>().SetDecline(false);
                //Do nothing and allow the player to continue making a decision as to
                //  what their final choice should be
            }


            //When the player makes a temporary choice via using the selectReticle, show the
            //  text prompt that asks for confirmation of final answer choice:
            if (selectReticle.GetComponent<ReticleScript>().GetFinalChoice() == true
                    && isWaitingForChoice == true)
            {
                isWaitingForConfirm = true; //Prevents update from calling repeatedly
                isWaitingForChoice = false;
                promptManager.ShowTextBoxActionConfirm();
            }
            //When the player decides not to make their temporary choice the final answer choice,
            //  then make the confirmation text prompt disappear (so that they may choose a
            //  different temporary choice):
            else if (selectReticle.GetComponent<ReticleScript>().GetFinalChoice() == false
                    && isWaitingForConfirm == true)
            {
                isWaitingForChoice = true; //Prevents update from calling repeatedly
                isWaitingForConfirm = false;
                promptManager.HideTextBoxActionConfirm();
            }


            if (subStepIsEnding) //Hide question 3 (this is done once after
            {                    //  sub step 3 is answered)
                Debug.Log("Ending step 2 of current iteration...");
                subStepIsEnding = false;
                promptManager.HideTextBoxQuestion3();
                dataManager.IncrementIterationStageFrom(mainSubStep);

                subStepIsStarting = true;
                isSubStep3 = true;
            }
            else //Keep updating as sub step 2 until the step is completed
            {
                isSubStep2 = true;
            }
        }

/*subStep3*/ //NEEDS TO BE EDITED FOR SWAPPING W/ MOUSE!!! (OR WITH ARROW KEYS AND E)
        else if (mainSubStep == 3) 
        {
            if (subStepIsStarting) // Display swap prompt(this is done once)
            {
                Debug.Log("Now in step 3 of current iteration...");
                subStepIsStarting = false;
                promptManager.ShowTextBoxSwapIt();
                yield return new WaitForSeconds(0.5f);
                //We don't want the player to have control over the select reticle here...
                dataManager.GetComponent<ReticleController>().SetInactive();
                //removed statement: dataManager.GetComponent<ReticleController>().SetActive();
                isWaitingForChoice = true;
                isWaitingForConfirm = false;
            }

            //TO_DO: Enable the player to swap the two indices
            //  -->IF they answer correctly, end sort phase and enter battle phase
            //  -->IF they answer incorrectly, enemy gets opportunity to attack
            //       - After the enemy attacks (and the player possibly dodges),
            //           show the correct answer (the right action to make)
            //       - After the correct answer is shown/performed, then the
            //           sort phase ends and the player enters the battle phase
            if (selectReticle.GetComponent<ReticleScript>().GetConfirm())
            {
                selectReticle.GetComponent<ReticleScript>().SetFinalChoice(false);
                selectReticle.GetComponent<ReticleScript>().SetConfirm(false);
                //removed statement: dataManager.GetComponent<ReticleController>().SetInactive();

                //Check if the confirmed final choice is correct or wrong:
                //  - If the choice is correct, show that the choice was correct
                //      with some visual and sound feedback
                //    --> Checking for the correctness needs to be implemented
                //  - Increment the player's power meter as a result of answering
                //      the question correctly
                //  - Then, make "subStepIsEnding" = true so that the next question
                //      can be viewed and answered
                int[] answerArray = dataManager.GetCurrentPhaseValues();
                bool isCorrectAnswer = dataManager.GetAnswerForComparison(answerArray, mainSubStep);
                if (isCorrectAnswer == true)
                {
                    subStepIsEnding = true;
                }

                //  - If the choice is incorrect, show that the choice was incorrect
                //      with some visual and sound feedback
                //    --> Checking for correctness needs to be implemented
                //  - Make the enemy attack the player and allow the player to dodge
                //  - Then, "subStepIsStarting" = true so that the current question
                //      can be viewed and answered again
                else if (isCorrectAnswer == false)
                {
                    //not necessary: dataManager.GetComponent<ReticleController>().SetInactive();
                    dataManager.SetInvisible();
                }

            }
            else if (selectReticle.GetComponent<ReticleScript>().GetDecline())
            {
                selectReticle.GetComponent<ReticleScript>().SetFinalChoice(false);
                selectReticle.GetComponent<ReticleScript>().SetDecline(false);
                //Do nothing and allow the player to continue making a decision as to
                //  what their final choice should be
            }


            //When the player makes a temporary choice via using the selectReticle, show the
            //  text prompt that asks for confirmation of final answer choice:
            if (selectReticle.GetComponent<ReticleScript>().GetFinalChoice() == true
                    && isWaitingForChoice == true)
            {
                isWaitingForConfirm = true; //Prevents update from calling repeatedly
                isWaitingForChoice = false;
                promptManager.ShowTextBoxActionConfirm();
            }
            //When the player decides not to make their temporary choice the final answer choice,
            //  then make the confirmation text prompt disappear (so that they may choose a
            //  different temporary choice):
            else if (selectReticle.GetComponent<ReticleScript>().GetFinalChoice() == false
                    && isWaitingForConfirm == true)
            {
                isWaitingForChoice = true; //Prevents update from calling repeatedly
                isWaitingForConfirm = false;
                promptManager.HideTextBoxActionConfirm();
            }


            if (subStepIsEnding)
            {
                Debug.Log("Ending step 3 of current iteration...");
                subStepIsEnding = false;
                promptManager.HideTextBoxSwapIt();
                dataManager.IncrementIterationStageFrom(mainSubStep);

                subStepIsStarting = true;
                isSubStep4 = true;
            }
            else //Keep updating as sub step 3 until the step is completed
            {
                isSubStep3 = true;
            }
        }

/*subStep4 */
        else if (mainSubStep == 4)
        {
            if (subStepIsStarting) // Display continue or end iteration prompt (this is done once)
            {
                Debug.Log("Now in step 4 of current iteration...");
                subStepIsStarting = false;
                promptManager.ShowTextBoxQuestion4();
                yield return new WaitForSeconds(0.5f);
                //We don't want the player to have control over the reticle for this question...
                dataManager.GetComponent<ReticleController>().SetInactive();
                //removed statement: dataManager.GetComponent<ReticleController>().SetActive();
                isWaitingForChoice = true;
                isWaitingForConfirm = false;
            }

            //TO_DO: Allow the player to click either button: "Continue Iteration" or "End Iteration"
            //  -->IF they answer correctly, end sort phase and enter battle phase
            //  -->IF they answer incorrectly, enemy gets opportunity to attack
            //       - After the enemy attacks (and the player possibly dodges),
            //           show the correct answer (the right action to make)
            //       - After the correct answer is shown/performed, then the
            //           sort phase ends and the player enters the battle phase
            if (selectReticle.GetComponent<ReticleScript>().GetConfirm())
            {
                selectReticle.GetComponent<ReticleScript>().SetFinalChoice(false);
                selectReticle.GetComponent<ReticleScript>().SetConfirm(false);
                //removed statement: dataManager.GetComponent<ReticleController>().SetInactive();

                //Check if the confirmed final choice is correct or wrong:
                //  - If the choice is correct, show that the choice was correct
                //      with some visual and sound feedback
                //    --> Checking for the correctness needs to be implemented
                //  - Increment the player's power meter as a result of answering
                //      the question correctly
                //  - Then, make "subStepIsEnding" = true so that the next question
                //      can be viewed and answered
                bool answerToCheck = selectReticle.GetComponent<ReticleScript>().GetAnswerBool();
                bool isCorrectAnswer = dataManager.GetAnswerForComparison(answerToCheck, mainSubStep);
                if (isCorrectAnswer == true)
                {
                    subStepIsEnding = true;
                }

                //  - If the choice is incorrect, show that the choice was incorrect
                //      with some visual and sound feedback
                //    --> Checking for correctness needs to be implemented
                //  - Make the enemy attack the player and allow the player to dodge
                //  - Then, "subStepIsStarting" = true so that the current question
                //      can be viewed and answered again
                else if (isCorrectAnswer == false)
                {
                    dataManager.GetComponent<ReticleController>().SetInactive();
                    dataManager.SetInvisible();
                }

            }
            else if (selectReticle.GetComponent<ReticleScript>().GetDecline())
            {
                selectReticle.GetComponent<ReticleScript>().SetFinalChoice(false);
                selectReticle.GetComponent<ReticleScript>().SetDecline(false);
                //Do nothing and allow the player to continue making a decision as to
                //  what their final choice should be
            }


            //When the player makes a temporary choice via using the selectReticle, show the
            //  text prompt that asks for confirmation of final answer choice:
            if (selectReticle.GetComponent<ReticleScript>().GetFinalChoice() == true
                    && isWaitingForChoice == true)
            {
                isWaitingForConfirm = true; //Prevents update from calling repeatedly
                isWaitingForChoice = false;
                promptManager.ShowTextBoxActionConfirm();
            }
            //When the player decides not to make their temporary choice the final answer choice,
            //  then make the confirmation text prompt disappear (so that they may choose a
            //  different temporary choice):
            else if (selectReticle.GetComponent<ReticleScript>().GetFinalChoice() == false
                    && isWaitingForConfirm == true)
            {
                isWaitingForChoice = true; //Prevents update from calling repeatedly
                isWaitingForConfirm = false;
                promptManager.HideTextBoxActionConfirm();
            }


            if (subStepIsEnding)
            {
                Debug.Log("Ending step 4 of current iteration...");
                subStepIsEnding = false;
                promptManager.HideTextBoxQuestion4();


                //Check if the iteration phase has been solved (i.e. the number we were swapping has
                //  finally been "bubbled" to its final position in the data structure
                //  --> If iteration phase is solved, move on to the battle phase

                //NOTE: NEED TO IMPLEMENT!!!!
                //Checking if the iteration phased is solved or not...
                    //Setting "iterationPhaseSolved" variable to the truth value...


                if (iterationPhaseSolved == true)
                {
                    Debug.Log("ITERATION PHASE SOLVED!!! (i.e. currentMainStepSolved = true)");
                    //NOTE: The sub step should increment from 4 to 5, but really
                    //        there is no sub step 5. Sub step 5 acts as a placed holder
                    //        to prevent any of the sub steps from repeating over again.
                    dataManager.IncrementIterationStageFrom(mainSubStep);

                    //NOTE: "subStepIsStarting" should be set true somewhere in the BattleCoroutine after
                    //         a battle has ended and we need to get to "mainSubStep0", where another
                    //         solve phase can possibly begin.
                    //subStepIsStarting = true;
                    currentMainStepSolved = true;
                    isSubStep5 = true; //No sub step 5 exists, however, this allows us to reenter
                                       //  the SortCoroutine() method so we can transition from
                                       //  the sorting phase to the battle phase
                }
                //  --> If iteration phase is not solved yet, continue swapping the number that
                //        still needs to be "bubbled" to the end
                else if (iterationPhaseSolved == false)
                {
                    Debug.Log("Iteration phase is not solved yet. Starting step 1 again...");
                    dataManager.SetIterationStageTo(1); //Reset "mainSubStep" to equal 1

                    subStepIsStarting = true;
                    isSubStep1 = true;
                }
                
            }
            else //Keep updating as sub step 4 until the step is completed
            {
                isSubStep4 = true;
            }
        }

/*mainStepSolved*/
        else if (currentMainStepSolved)
        {
            Debug.Log("Current iteration has been solved! Moving on to battle phase...");
            currentMainStepSolved = false;
            isTimeToSolve = false;

            //dataManager.ShowTextBoxCombatStart();
            //dataManager.HideTextBoxCombatStart();
            isTimeToFight = true;
            
        }

/*subStep0*/
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
        selectReticle = dataManager.GetComponent<ReticleController>().GetReticle();

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
        isSubStep5 = false;
        isSubStep0 = false;

        currentMainStepSolved = false;
        iterationPhaseSolved = false;
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
            else if (isSubStep4)
            {
                isSubStep4 = false;
                StartCoroutine(SortCoroutine());
            }
            else if (isSubStep5) //There really isn't a sub step 5, but it
            {                    //  transitions from the sort phase to the battle phase.
                isSubStep5 = false;
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
            Debug.Log("TIME TO FIGHT!!! Entering Battle Phase...");
            isTimeToFight = false; //Only temporarily placed here. Really, it should be
                                   //  in the BattleCoroutine when the battle ends
            isTimeToSolve = true;  //Only temporarily placed here. This should be triggered
                                   //  once the battle ends and if the problem isn't solved yet
            isSubStep0 = true;     //This should be triggered once the battle ends and
                                   //  isTimeToSolve is true again
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
