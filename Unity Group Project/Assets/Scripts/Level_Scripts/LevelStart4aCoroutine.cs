using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStart4aCoroutine : MonoBehaviour
{
    public PromptManager promptManager;
    public DataStructure4aManager dataManager;
    public GameObject dataStructure;
    public InputManager inputManager;
    public BattleManager battleManager;
    public GameObject selectHand;
    public GameObject reward1;
    public GameObject reward2;
    public GameObject reward3;

    public GameObject bubbleNumSpace;
    public GameObject swapNumSpace;
    public GameObject tempNumSpace;

    public int mainStep;
    public int mainSubStep;


    private bool introFinished;
    private bool sortStarted;

    private bool subStepIsStarting;
    private bool subStepIsEnding;
    private bool firstTimeSortCoroutine;
    private bool firstTimeBattleCoroutine;
    private bool firstTimeVictoryCoroutine;
    private bool isTimeToFight;
    private bool isTimeToSolve;
    private bool isTimeToCelebrate;
    private bool isWaitingForChoice;
    private bool isWaitingForConfirm;
    private bool hasCheckedAnswer;
    private bool givingFeedback;
    private bool hasPerformedSwap;
    private bool readyToSwap;

    private bool endResultsExited;
    private bool hasExitedEndResults;
    private bool isChoosingReward;
    private bool exitingLevel;

    private bool isFirstSwap;
    private bool isSubStep1;
    private bool isSubStep2;
    private bool isSubStep3;
    private bool isSubStep4;
    private bool isSubStep5;
    private bool isSubStep6;
    private bool isSubStep0; //FINISH HIM!!!

    private bool currentMainStepSolved;
    private bool iterationPhaseSolved;
    private bool problemSolved; //Becomes true when the data has been completely sorted

    public GameObject selectReticle;
    
    private bool selectHandActive;
    private bool playerBattleTurnOver;
    private bool enemyBattleTurnOver;
    private bool enemyAlive;
    private bool enemyDefeated;
    private bool showingGameOver;

    public GameObject endOfLevelMenu;




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
        if (firstTimeSortCoroutine)
        {
            mainStep = dataManager.GetIterationNum(); //Iteration number
            mainSubStep = dataManager.GetIterationStage(); //The current step in iteration
            firstTimeSortCoroutine = false;

            //Disable player movement after the enemy's punish attack ends--
            //  --> This HAS to occur before the below statements in order to check if the player
            //        was defeated or is still alive.
            battleManager.playerCharacter.GetComponent<PlayerController>().MovementEnabled(false);

            //When the player is defeated (currentHealth < 1), signal the player's defeat--
            if (battleManager.playerCharacter.GetComponent<PlayerController>().GetPlayerDefeated() == true
                && showingGameOver == false)
            {
                showingGameOver = true; //Prevents these statements from repeatedly being called
                subStepIsStarting = false;
                dataStructure.GetComponent<ReticleController>().SetInactive();
                dataManager.SetInvisible(); //Permanently hide the dataStructure
                promptManager.HideTextBoxQuestion1(); //Permanently hide the question1 prompt

                StartCoroutine(DefeatCoroutine()); //Begin the sequence of events following the player's defeat
            }
            else
            {
                isSubStep1 = true; //Continue the sorting phase as normal since the player has not lost
                subStepIsStarting = true;
            }
        }

/*subStep1*/
        else if (mainSubStep == 1)
        {
            if (subStepIsStarting)
            {
                Debug.Log("Now in step 1 of current iteration...");
                subStepIsStarting = false;
                promptManager.ShowTextBoxQuestion1();
                yield return new WaitForSeconds(0.5f);
                dataManager.SetVisible();
                dataStructure.GetComponent<ReticleController>().SetActive();
                dataStructure.GetComponent<ReticleController>().EnableControls();
                battleManager.playerCharacter.GetComponent<PlayerController>().EndEmote("Exclaim");
                battleManager.playerCharacter.GetComponent<PlayerController>().StartEmote("Ellipses");

                //promptManager.swapIt_animator.gameObject.SetActive(false);
                //promptManager.question4_animator.gameObject.SetActive(false);
                //promptManager.actionConfirm_animator.gameObject.SetActive(false);

                isWaitingForChoice = true;
                isWaitingForConfirm = false;
                hasCheckedAnswer = false;
                givingFeedback = false;
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

                //Save the player's answer choice for later (when the numbers need to be swapped)--
                bubbleNumSpace = selectReticle.GetComponent<ReticleScript>().GetCurrentMemorySpace();

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
                if (isCorrectAnswer == true && hasCheckedAnswer == false)
                {
                    hasCheckedAnswer = true;
                    battleManager.playerCharacter.GetComponent<PlayerController>().EndEmote("Ellipses");
                    givingFeedback = true;
                    StartCoroutine(CorrectAnswer1Coroutine());
                    //included in above statement: subStepIsEnding = true;
                }

                //  - If the choice is incorrect, show that the choice was incorrect
                //      with some visual and sound feedback
                //    --> Checking for correctness needs to be implemented
                //  - Make the enemy attack the player and allow the player to dodge
                //  - Then, "subStepIsStarting" = true so that the current question
                //      can be viewed and answered again
                else if (isCorrectAnswer == false && hasCheckedAnswer == false)
                {
                    hasCheckedAnswer = true;
                    battleManager.playerCharacter.GetComponent<PlayerController>().EndEmote("Ellipses");
                    givingFeedback = true;
                    StartCoroutine(WrongAnswer1Coroutine());
                    /*included in above statement:
                    dataManager.GetComponent<ReticleController>().SetInactive();
                    inputManager.DisableMouseControls();
                    dataManager.SetInvisible();
                    promptManager.HideTextBoxQuestion1();
                    if ((enemyAlive == true) && (enemyDefeated == false))
                    {
                        PunishWrongAnswer();
                        enemyBattleTurnOver = true;
                    }
                    else if ((enemyAlive == false))
                    {
                        subStepIsStarting = true;
                    }
                    */
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
                //Temporarily disable control of the select reticle's movement and selection--
                dataStructure.GetComponent<ReticleController>().DisableControls();

                //promptManager.actionConfirm_animator.gameObject.SetActive(true);
                //yield return new WaitForSeconds(0.1f);
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
                //Give the player control over the select reticle once again--
                if (givingFeedback == false)
                {
                    dataStructure.GetComponent<ReticleController>().EnableControls();
                }
                promptManager.HideTextBoxActionConfirm();
            }


            if (subStepIsEnding) //Hide question 1 (this is done once after
            {                    //  sub step 1 is answered)
                Debug.Log("Ending step 1 of current iteration...");
                subStepIsEnding = false;

                //promptManager.swapIt_animator.gameObject.SetActive(true);
                //promptManager.question4_animator.gameObject.SetActive(true);
                //promptManager.actionConfirm_animator.gameObject.SetActive(true);

                promptManager.HideTextBoxQuestion1();
                dataManager.IncrementIterationStageFrom(mainSubStep);
                mainSubStep = dataManager.GetIterationStage(); //The current step in iteration

                subStepIsStarting = true;
                isSubStep2 = true;
            }
            else //Keep updating as sub step 1 until the step is completed
            {
                isSubStep1 = true;
                if (enemyBattleTurnOver == true
                    && battleManager.enemyCharacter.GetComponent<EnemyController>().AttackHasEnded() == true)
                {
                    //Disable player movement after the enemy's punish attack ends--
                    //  --> This HAS to occur before the below statements in order to check if the player
                    //        was defeated or is still alive.
                    battleManager.playerCharacter.GetComponent<PlayerController>().MovementEnabled(false);

                    //When the player is defeated (currentHealth < 1), signal the player's defeat--
                    if (battleManager.playerCharacter.GetComponent<PlayerController>().GetPlayerDefeated() == true
                        && showingGameOver == false)
                    {
                        showingGameOver = true; //Prevents these statements from repeatedly being called
                        dataManager.SetInvisible(); //Permanently hide the dataStructure
                        StartCoroutine(DefeatCoroutine()); //Begin the sequence of events following the player's defeat
                    }
                    else if (battleManager.playerCharacter.GetComponent<PlayerController>().GetPlayerDefeated() == false
                             && battleManager.playerCharacter.GetComponent<PlayerController>().GetCurrentHealth() > 0)
                    {
                        //Disable player movement after the enemy's punish attack ends--
                        battleManager.playerCharacter.GetComponent<PlayerController>().MovementEnabled(false);

                        enemyBattleTurnOver = false;
                        subStepIsStarting = true;
                    }
                }
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
                dataManager.SetVisible();
                dataStructure.GetComponent<ReticleController>().SetActive();
                dataStructure.GetComponent<ReticleController>().EnableControls();
                battleManager.playerCharacter.GetComponent<PlayerController>().EndEmote("Exclaim");
                battleManager.playerCharacter.GetComponent<PlayerController>().StartEmote("Ellipses");

                //promptManager.swapIt_animator.gameObject.SetActive(false);
                //promptManager.question4_animator.gameObject.SetActive(false);
                //promptManager.actionConfirm_animator.gameObject.SetActive(false);

                isWaitingForChoice = true;
                isWaitingForConfirm = false;
                hasCheckedAnswer = false;
                givingFeedback = false;
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

                //Save the player's answer choice for later (when the numbers need to be swapped)--
                swapNumSpace = selectReticle.GetComponent<ReticleScript>().GetCurrentMemorySpace();

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
                if (isCorrectAnswer == true && hasCheckedAnswer == false)
                {
                    hasCheckedAnswer = true;
                    battleManager.playerCharacter.GetComponent<PlayerController>().EndEmote("Ellipses");
                    givingFeedback = true;
                    StartCoroutine(CorrectAnswer2Coroutine());
                    //included in above statement: subStepIsEnding = true;
                }

                //  - If the choice is incorrect, show that the choice was incorrect
                //      with some visual and sound feedback
                //    --> Checking for correctness needs to be implemented
                //  - Make the enemy attack the player and allow the player to dodge
                //  - Then, "subStepIsStarting" = true so that the current question
                //      can be viewed and answered again
                else if (isCorrectAnswer == false && hasCheckedAnswer == false)
                {
                    hasCheckedAnswer = true;
                    battleManager.playerCharacter.GetComponent<PlayerController>().EndEmote("Ellipses");
                    givingFeedback = true;
                    StartCoroutine(WrongAnswer2Coroutine());
                    /*included in above statement:
                    dataManager.GetComponent<ReticleController>().SetInactive();
                    inputManager.DisableMouseControls();
                    dataManager.SetInvisible();
                    promptManager.HideTextBoxQuestion2();
                    if ((enemyAlive == true) && (enemyDefeated == false))
                    {
                        PunishWrongAnswer();
                        enemyBattleTurnOver = true;
                    }
                    else if ((enemyAlive == false))
                    {
                        subStepIsStarting = true;
                    }
                    */
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
                //Temporarily disable control of the select reticle's movement and selection--
                dataStructure.GetComponent<ReticleController>().DisableControls();

                //promptManager.actionConfirm_animator.gameObject.SetActive(true);
                //yield return new WaitForSeconds(0.1f);
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
                //Give the player control over the select reticle once again--
                if (givingFeedback == false)
                {
                    dataStructure.GetComponent<ReticleController>().EnableControls();
                }
                promptManager.HideTextBoxActionConfirm();
            }


            if (subStepIsEnding) //Hide question 2 (this is done once after
            {                    //  sub step 2 is answered)
                Debug.Log("Ending step 2 of current iteration...");
                subStepIsEnding = false;

                //promptManager.swapIt_animator.gameObject.SetActive(true);
                //promptManager.question4_animator.gameObject.SetActive(true);
                //promptManager.actionConfirm_animator.gameObject.SetActive(true);

                promptManager.HideTextBoxQuestion2();
                dataManager.IncrementIterationStageFrom(mainSubStep);
                mainSubStep = dataManager.GetIterationStage(); //The current step in iteration

                subStepIsStarting = true;
                isSubStep3 = true;
                isFirstSwap = false;
            }
            else //Keep updating as sub step 2 until the step is completed
            {
                isSubStep2 = true;
                if (enemyBattleTurnOver == true
                    && battleManager.enemyCharacter.GetComponent<EnemyController>().AttackHasEnded() == true)
                {
                    //Disable player movement after the enemy's punish attack ends--
                    //  --> This HAS to occur before the below statements in order to check if the player
                    //        was defeated or is still alive.
                    battleManager.playerCharacter.GetComponent<PlayerController>().MovementEnabled(false);

                    //When the player is defeated (currentHealth < 1), signal the player's defeat--
                    if (battleManager.playerCharacter.GetComponent<PlayerController>().GetPlayerDefeated() == true
                        && showingGameOver == false)
                    {
                        showingGameOver = true; //Prevents these statements from repeatedly being called
                        dataManager.SetInvisible(); //Permanently hide the dataStructure
                        StartCoroutine(DefeatCoroutine()); //Begin the sequence of events following the player's defeat
                    }
                    else if (battleManager.playerCharacter.GetComponent<PlayerController>().GetPlayerDefeated() == false
                             && battleManager.playerCharacter.GetComponent<PlayerController>().GetCurrentHealth() > 0)
                    {
                        //Disable player movement after the enemy's punish attack ends--
                        battleManager.playerCharacter.GetComponent<PlayerController>().MovementEnabled(false);

                        enemyBattleTurnOver = false;
                        subStepIsStarting = true;
                    }
                }
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
                promptManager.ShowTextBoxQuestion2();
                yield return new WaitForSeconds(0.5f);
                dataManager.SetVisible();
                dataStructure.GetComponent<ReticleController>().SetActive();
                dataStructure.GetComponent<ReticleController>().EnableControls();
                battleManager.playerCharacter.GetComponent<PlayerController>().EndEmote("Exclaim");
                battleManager.playerCharacter.GetComponent<PlayerController>().StartEmote("Ellipses");

                //promptManager.swapIt_animator.gameObject.SetActive(false);
                //promptManager.question4_animator.gameObject.SetActive(false);
                //promptManager.actionConfirm_animator.gameObject.SetActive(false);

                isWaitingForChoice = true;
                isWaitingForConfirm = false;
                hasCheckedAnswer = false;
                givingFeedback = false;
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

                //Save the player's answer choice for later (when the numbers need to be swapped)--
                swapNumSpace = selectReticle.GetComponent<ReticleScript>().GetCurrentMemorySpace();

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
                if (isCorrectAnswer == true && hasCheckedAnswer == false)
                {
                    hasCheckedAnswer = true;
                    battleManager.playerCharacter.GetComponent<PlayerController>().EndEmote("Ellipses");
                    givingFeedback = true;
                    StartCoroutine(CorrectAnswer3Coroutine());
                    //included in above statement: subStepIsEnding = true;
                }

                //  - If the choice is incorrect, show that the choice was incorrect
                //      with some visual and sound feedback
                //    --> Checking for correctness needs to be implemented
                //  - Make the enemy attack the player and allow the player to dodge
                //  - Then, "subStepIsStarting" = true so that the current question
                //      can be viewed and answered again
                else if (isCorrectAnswer == false && hasCheckedAnswer == false)
                {
                    hasCheckedAnswer = true;
                    battleManager.playerCharacter.GetComponent<PlayerController>().EndEmote("Ellipses");
                    givingFeedback = true;
                    StartCoroutine(WrongAnswer3Coroutine());
                    /*included in above statement:
                    dataManager.GetComponent<ReticleController>().SetInactive();
                    inputManager.DisableMouseControls();
                    dataManager.SetInvisible();
                    promptManager.HideTextBoxQuestion3();
                    if ((enemyAlive == true) && (enemyDefeated == false))
                    {
                        PunishWrongAnswer();
                        enemyBattleTurnOver = true;
                    }
                    else if ((enemyAlive == false))
                    {
                        subStepIsStarting = true;
                    }
                    */
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
                //Temporarily disable control of the select reticle's movement and selection--
                dataStructure.GetComponent<ReticleController>().DisableControls();

                //promptManager.actionConfirm_animator.gameObject.SetActive(true);
                //yield return new WaitForSeconds(0.1f);
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
                //Give the player control over the select reticle once again--
                if (givingFeedback == false)
                {
                    dataStructure.GetComponent<ReticleController>().EnableControls();
                }
                promptManager.HideTextBoxActionConfirm();
            }


            if (subStepIsEnding) //Hide question 3 (this is done once after
            {                    //  sub step 3 is answered)
                Debug.Log("Ending step 2 of current iteration...");
                subStepIsEnding = false;

                //promptManager.swapIt_animator.gameObject.SetActive(true);
                //promptManager.question4_animator.gameObject.SetActive(true);
                //promptManager.actionConfirm_animator.gameObject.SetActive(true);

                promptManager.HideTextBoxQuestion2();
                dataManager.IncrementIterationStageFrom(mainSubStep);
                mainSubStep = dataManager.GetIterationStage(); //The current step in iteration

                subStepIsStarting = true;
                isSubStep3 = true;
            }
            else //Keep updating as sub step 2 until the step is completed
            {
                isSubStep2 = true;
                if (enemyBattleTurnOver == true
                    && battleManager.enemyCharacter.GetComponent<EnemyController>().AttackHasEnded() == true)
                {
                    //Disable player movement after the enemy's punish attack ends--
                    //  --> This HAS to occur before the below statements in order to check if the player
                    //        was defeated or is still alive.
                    battleManager.playerCharacter.GetComponent<PlayerController>().MovementEnabled(false);

                    //When the player is defeated (currentHealth < 1), signal the player's defeat--
                    if (battleManager.playerCharacter.GetComponent<PlayerController>().GetPlayerDefeated() == true
                        && showingGameOver == false)
                    {
                        showingGameOver = true; //Prevents these statements from repeatedly being called
                        dataManager.SetInvisible(); //Permanently hide the dataStructure
                        StartCoroutine(DefeatCoroutine()); //Begin the sequence of events following the player's defeat
                    }
                    else if (battleManager.playerCharacter.GetComponent<PlayerController>().GetPlayerDefeated() == false
                             && battleManager.playerCharacter.GetComponent<PlayerController>().GetCurrentHealth() > 0)
                    {
                        //Disable player movement after the enemy's punish attack ends--
                        battleManager.playerCharacter.GetComponent<PlayerController>().MovementEnabled(false);

                        enemyBattleTurnOver = false;
                        subStepIsStarting = true;
                    }
                }
            }
        }

/*subStep3 */
        else if (mainSubStep == 3)
        {
            if (subStepIsStarting) // Display continue or end iteration prompt (this is done once)
            {
                Debug.Log("Now in step 3 of current iteration...");
                subStepIsStarting = false;

                //promptManager.question4_animator.gameObject.SetActive(true);
                //yield return new WaitForSeconds(0.1f);

                promptManager.ShowTextBoxQuestion4();
                yield return new WaitForSeconds(0.5f);
                dataManager.SetVisible();
                //We don't want the player to have control over the reticle for this question...
                dataStructure.GetComponent<ReticleController>().SetInactive();
                //removed statement: dataManager.GetComponent<ReticleController>().SetActive();
                battleManager.playerCharacter.GetComponent<PlayerController>().EndEmote("Exclaim");
                battleManager.playerCharacter.GetComponent<PlayerController>().StartEmote("Ellipses");

                //promptManager.swapIt_animator.gameObject.SetActive(false);
                //promptManager.actionConfirm_animator.gameObject.SetActive(false);

                isWaitingForChoice = true;
                isWaitingForConfirm = false;
            }

            //TO_DO: Allow the player to click either button: "Swap Them" or "Don't Swap"
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
                readyToSwap = answerToCheck;
                bool isCorrectAnswer = dataManager.GetAnswerForComparison(answerToCheck, mainSubStep);
                if (isCorrectAnswer == true)
                {
                    battleManager.playerCharacter.GetComponent<PlayerController>().EndEmote("Ellipses");
                    iterationPhaseSolved = dataManager.GetIterationStatus(mainStep);
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
                    battleManager.playerCharacter.GetComponent<PlayerController>().EndEmote("Ellipses");
                    dataStructure.GetComponent<ReticleController>().SetInactive();
                    inputManager.DisableMouseControls();
                    dataManager.SetInvisible();
                    promptManager.HideTextBoxQuestion4();
                    if ((enemyAlive == true) && (enemyDefeated == false))
                    {
                        PunishWrongAnswer();
                        enemyBattleTurnOver = true;
                    }
                    else if ((enemyAlive == false))
                    {
                        subStepIsStarting = true;
                    }
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

                //promptManager.actionConfirm_animator.gameObject.SetActive(true);
                //yield return new WaitForSeconds(0.1f);
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
                promptManager.HideTextBoxQuestion4();

                //promptManager.swapIt_animator.gameObject.SetActive(true);
                //promptManager.question4_animator.gameObject.SetActive(true);
                //promptManager.actionConfirm_animator.gameObject.SetActive(true);

                inputManager.DisableMouseControls();
                dataManager.IncrementIterationStageFrom(mainSubStep);
                mainSubStep = dataManager.GetIterationStage(); //The current step in iteration

                subStepIsStarting = true;
                isSubStep4 = true;
            }
            else //Keep updating as sub step 3 until the step is completed
            {
                isSubStep3 = true;
                if (enemyBattleTurnOver == true
                    && battleManager.enemyCharacter.GetComponent<EnemyController>().AttackHasEnded() == true)
                {
                    //Disable player movement after the enemy's punish attack ends--
                    //  --> This HAS to occur before the below statements in order to check if the player
                    //        was defeated or is still alive.
                    battleManager.playerCharacter.GetComponent<PlayerController>().MovementEnabled(false);

                    //When the player is defeated (currentHealth < 1), signal the player's defeat--
                    if (battleManager.playerCharacter.GetComponent<PlayerController>().GetPlayerDefeated() == true
                        && showingGameOver == false)
                    {
                        showingGameOver = true; //Prevents these statements from repeatedly being called
                        dataManager.SetInvisible(); //Permanently hide the dataStructure
                        StartCoroutine(DefeatCoroutine()); //Begin the sequence of events following the player's defeat
                    }
                    else if (battleManager.playerCharacter.GetComponent<PlayerController>().GetPlayerDefeated() == false
                             && battleManager.playerCharacter.GetComponent<PlayerController>().GetCurrentHealth() > 0)
                    {
                        //Disable player movement after the enemy's punish attack ends--
                        battleManager.playerCharacter.GetComponent<PlayerController>().MovementEnabled(false);

                        enemyBattleTurnOver = false;
                        subStepIsStarting = true;
                    }
                }
            }
        }

/*subStep4*/ //NEEDS TO BE EDITED FOR SWAPPING W/O MOUSE!!! (OR WITH ARROW KEYS AND E)
        else if (mainSubStep == 4)
        {
            if (subStepIsStarting) // Display swap prompt(this is done once)
            {
                Debug.Log("Now in step 4 of current iteration...");
                subStepIsStarting = false;

                //-->NOTE: No longer necessary to prompt the player to manually swap the data
                //     removed statement: promptManager.ShowTextBoxSwapIt();

                //promptManager.swapIt_animator.gameObject.SetActive(false);
                //promptManager.question4_animator.gameObject.SetActive(false);
                //promptManager.actionConfirm_animator.gameObject.SetActive(false);

                yield return new WaitForSeconds(0.5f);
                dataManager.SetVisible();
                //We don't want the player to have control over the select reticle here...
                dataStructure.GetComponent<ReticleController>().SetInactive();

                //Enable the use of mouse controls for swapping--
                //-->NOTE: The swapping occurs automatically, so mouse controls are not activated
                //     removed statement: inputManager.EnableMouseControls();

                //removed statement: dataManager.GetComponent<ReticleController>().SetActive();
                battleManager.playerCharacter.GetComponent<PlayerController>().EndEmote("Exclaim");
                battleManager.playerCharacter.GetComponent<PlayerController>().StartEmote("Ellipses");

                hasPerformedSwap = false;
                if (readyToSwap == true)
                {
                    readyToSwap = false;
                    if (bubbleNumSpace.GetComponent<BoxScript>() != null
                        && swapNumSpace.GetComponent<BoxScript>() != null)
                    {
                        dataManager.PerformSwap(bubbleNumSpace, swapNumSpace, tempNumSpace);
                        StartCoroutine(WaitForSwap());
                    }
                }
                else if (readyToSwap == false)
                {
                    subStepIsEnding = true;
                }

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
            if (hasPerformedSwap == true)
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
                    battleManager.playerCharacter.GetComponent<PlayerController>().EndEmote("Ellipses");
                    inputManager.DisableMouseControls();
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
                    Debug.Log("WARNING: Swapping error!!! Answer was marked incorrect...");
                    battleManager.playerCharacter.GetComponent<PlayerController>().EndEmote("Ellipses");
                    inputManager.DisableMouseControls();
                    subStepIsEnding = true;
                    Debug.Log("WARNING: Continuing as if answer was marked correct...");
                }

            }
            else if (hasPerformedSwap == false)
            {
                selectReticle.GetComponent<ReticleScript>().SetFinalChoice(false);
                selectReticle.GetComponent<ReticleScript>().SetDecline(false);
                //Do nothing and allow the player to continue making a decision as to
                //  what their final choice should be
            }

            /*
            //When the player makes a temporary choice via using the selectReticle, show the
            //  text prompt that asks for confirmation of final answer choice:
            if (selectReticle.GetComponent<ReticleScript>().GetFinalChoice() == true
                    && isWaitingForChoice == true)
            {
                isWaitingForConfirm = true; //Prevents update from calling repeatedly
                isWaitingForChoice = false;
                //Disable the use of mouse controls while waiting for answer confirmation--
                inputManager.DisableMouseControls();
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
                //Enable the use of mouse controls once again so the player can continue swapping--
                inputManager.EnableMouseControls();
                promptManager.HideTextBoxActionConfirm();
            }
            */


            if (subStepIsEnding)
            {
                Debug.Log("Ending step 4 of current iteration...");
                subStepIsEnding = false;
                //not necessary: promptManager.HideTextBoxSwapIt();

                //promptManager.swapIt_animator.gameObject.SetActive(true);
                //promptManager.question4_animator.gameObject.SetActive(true);
                //promptManager.actionConfirm_animator.gameObject.SetActive(true);

                inputManager.DisableMouseControls();
                dataManager.IncrementIterationStageFrom(mainSubStep);
                mainSubStep = dataManager.GetIterationStage(); //The current step in iteration

                subStepIsStarting = true;
                isSubStep5 = true;
            }
            else //Keep updating as sub step 3 until the step is completed
            {
                isSubStep4 = true;
                if (enemyBattleTurnOver == true
                    && battleManager.enemyCharacter.GetComponent<EnemyController>().AttackHasEnded() == true)
                {
                    //Disable player movement after the enemy's punish attack ends--
                    //  --> This HAS to occur before the below statements in order to check if the player
                    //        was defeated or is still alive.
                    battleManager.playerCharacter.GetComponent<PlayerController>().MovementEnabled(false);

                    //When the player is defeated (currentHealth < 1), signal the player's defeat--
                    if (battleManager.playerCharacter.GetComponent<PlayerController>().GetPlayerDefeated() == true
                        && showingGameOver == false)
                    {
                        showingGameOver = true; //Prevents these statements from repeatedly being called
                        dataManager.SetInvisible(); //Permanently hide the dataStructure
                        StartCoroutine(DefeatCoroutine()); //Begin the sequence of events following the player's defeat
                    }
                    else if (battleManager.playerCharacter.GetComponent<PlayerController>().GetPlayerDefeated() == false
                             && battleManager.playerCharacter.GetComponent<PlayerController>().GetCurrentHealth() > 0)
                    {
                        //Disable player movement after the enemy's punish attack ends--
                        battleManager.playerCharacter.GetComponent<PlayerController>().MovementEnabled(false);

                        enemyBattleTurnOver = false;
                        subStepIsStarting = true;
                    }
                }
            }
        }

/*subStep5 */
        else if (mainSubStep == 5)
        {
            if (subStepIsStarting) // Display continue or end iteration prompt (this is done once)
            {
                Debug.Log("Now in step 5 of current iteration...");
                subStepIsStarting = false;

                //promptManager.question5_animator.gameObject.SetActive(true);
                //yield return new WaitForSeconds(0.1f);

                promptManager.ShowTextBoxQuestion5();
                yield return new WaitForSeconds(0.5f);
                dataManager.SetVisible();
                //We don't want the player to have control over the reticle for this question...
                dataStructure.GetComponent<ReticleController>().SetInactive();
                //removed statement: dataManager.GetComponent<ReticleController>().SetActive();
                battleManager.playerCharacter.GetComponent<PlayerController>().EndEmote("Exclaim");
                battleManager.playerCharacter.GetComponent<PlayerController>().StartEmote("Ellipses");

                //promptManager.swapIt_animator.gameObject.SetActive(false);
                //promptManager.actionConfirm_animator.gameObject.SetActive(false);

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
                    battleManager.playerCharacter.GetComponent<PlayerController>().EndEmote("Ellipses");
                    iterationPhaseSolved = dataManager.GetIterationStatus(mainStep);
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
                    battleManager.playerCharacter.GetComponent<PlayerController>().EndEmote("Ellipses");
                    dataStructure.GetComponent<ReticleController>().SetInactive();
                    inputManager.DisableMouseControls();
                    dataManager.SetInvisible();
                    promptManager.HideTextBoxQuestion5();
                    if ((enemyAlive == true) && (enemyDefeated == false))
                    {
                        PunishWrongAnswer();
                        enemyBattleTurnOver = true;
                    }
                    else if ((enemyAlive == false))
                    {
                        subStepIsStarting = true;
                    }
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

                //promptManager.actionConfirm_animator.gameObject.SetActive(true);
                //yield return new WaitForSeconds(0.1f);
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
                Debug.Log("Ending step 5 of current iteration...");
                subStepIsEnding = false;

                //promptManager.swapIt_animator.gameObject.SetActive(true);
                //promptManager.actionConfirm_animator.gameObject.SetActive(true);

                promptManager.HideTextBoxQuestion5();


                //Check if the iteration phase has been solved (i.e. the number we were swapping has
                //  finally been "bubbled" to its final position in the data structure
                //  --> If iteration phase is solved, move on to the battle phase

                //NOTE: NEED TO IMPLEMENT!!!!
                //Checking if the iteration phased is solved or not...
                //Setting "iterationPhaseSolved" variable to the truth value...


                if (iterationPhaseSolved == true)
                {
                    //promptManager.swapIt_animator.gameObject.SetActive(false);
                    //promptManager.question5_animator.gameObject.SetActive(false);
                    //promptManager.actionConfirm_animator.gameObject.SetActive(false);

                    Debug.Log("ITERATION PHASE SOLVED!!! (i.e. currentMainStepSolved = true)");
                    iterationPhaseSolved = false; //Reset boolean flag
                    //NOTE: The sub step should increment from 5 to 6, but really
                    //        there is no sub step 6. Sub step 6 acts as a placed holder
                    //        to prevent any of the sub steps from repeating over again.
                    dataManager.IncrementIterationStageFrom(mainSubStep);
                    Debug.Log("Incremented iteration stage to: " + mainSubStep);
                    mainSubStep = dataManager.GetIterationStage(); //The current step in iteration
                    //NOTE: "subStepIsStarting" should be set true somewhere in the BattleCoroutine after
                    //         a battle has ended and we need to get to "mainSubStep0", where another
                    //         solve phase can possibly begin.
                    //subStepIsStarting = true;
                    currentMainStepSolved = true;
                    isSubStep6 = true; //No sub step 6 exists, however, this allows us to reenter
                                       //  the SortCoroutine() method so we can transition from
                                       //  the sorting phase to the battle phase
                }
                //  --> If iteration phase is not solved yet, continue swapping the number that
                //        still needs to be "bubbled" to the end
                else if (iterationPhaseSolved == false)
                {
                    Debug.Log("Iteration phase is not solved yet. Starting step 1 again...");
                    dataManager.SetIterationStageTo(1); //Reset "mainSubStep" to equal 1
                    mainSubStep = dataManager.GetIterationStage(); //The current step in iteration

                    subStepIsStarting = true;
                    isSubStep1 = true;
                }

            }
            else //Keep updating as sub step 5 until the step is completed
            {
                isSubStep5 = true;
                if (enemyBattleTurnOver == true
                    && battleManager.enemyCharacter.GetComponent<EnemyController>().AttackHasEnded() == true)
                {
                    //Disable player movement after the enemy's punish attack ends--
                    //  --> This HAS to occur before the below statements in order to check if the player
                    //        was defeated or is still alive.
                    battleManager.playerCharacter.GetComponent<PlayerController>().MovementEnabled(false);

                    //When the player is defeated (currentHealth < 1), signal the player's defeat--
                    if (battleManager.playerCharacter.GetComponent<PlayerController>().GetPlayerDefeated() == true
                        && showingGameOver == false)
                    {
                        showingGameOver = true; //Prevents these statements from repeatedly being called
                        dataManager.SetInvisible(); //Permanently hide the dataStructure
                        StartCoroutine(DefeatCoroutine()); //Begin the sequence of events following the player's defeat
                    }
                    else if (battleManager.playerCharacter.GetComponent<PlayerController>().GetPlayerDefeated() == false
                             && battleManager.playerCharacter.GetComponent<PlayerController>().GetCurrentHealth() > 0)
                    {
                        //Disable player movement after the enemy's punish attack ends--
                        battleManager.playerCharacter.GetComponent<PlayerController>().MovementEnabled(false);

                        enemyBattleTurnOver = false;
                        subStepIsStarting = true;
                    }
                }
            }
        }

/*mainStepSolved*/
        else if (currentMainStepSolved)
        {
            //Check if the sorting problem was solved:
            problemSolved = dataManager.GetCompletionStatus();

            //If the sorting problem is solved, show a prompt stating it is solved
            if (problemSolved == true)
            {
                //Display celebratory message prompt

                //Exit the Sorting Phase and determine whether to enter the battle phase or not:
                currentMainStepSolved = false;
                isTimeToSolve = false;
                isTimeToFight = false;
            }
            else
            {
                Debug.Log("Current iteration has been solved! Moving on to battle phase...");
                currentMainStepSolved = false;
                isTimeToSolve = false;

                //dataManager.ShowTextBoxCombatStart();
                //dataManager.HideTextBoxCombatStart();
                if ((enemyAlive == true) && (enemyDefeated == false)) { isTimeToFight = true; }
                else { isTimeToFight = false; }
            }
            
        }

        yield return new WaitForSeconds(0.1f);

    }//End of SortCoroutine()




    // Start is called before the first frame update
    void Start()
    {
        selectReticle = dataStructure.GetComponent<ReticleController>().GetReticle();
        if (selectHand != null)
        {
            selectHand.SetActive(false);
            selectHandActive = false;
        }

        if (endOfLevelMenu != null)
        {
            endOfLevelMenu.SetActive(false);
        }

        playerBattleTurnOver = false;
        enemyBattleTurnOver = false;
        enemyAlive = true;
        enemyDefeated = false;
        showingGameOver = false;

        introFinished = false;
        sortStarted = false;

        subStepIsStarting = false;
        subStepIsEnding = false;
        firstTimeSortCoroutine = true;
        firstTimeBattleCoroutine = true;
        firstTimeVictoryCoroutine = false;
        isTimeToFight = false;
        isTimeToSolve = true;
        isTimeToCelebrate = false;

        hasPerformedSwap = false;

        endResultsExited = false;
        hasExitedEndResults = false;
        isChoosingReward = false;
        exitingLevel = false;

        mainStep = 0;
        mainSubStep = 0;

        isFirstSwap = true;
        isSubStep1 = false;
        isSubStep2 = false;
        isSubStep3 = false;
        isSubStep4 = false;
        isSubStep5 = false;
        isSubStep6 = false;
        isSubStep0 = false;

        currentMainStepSolved = false;
        iterationPhaseSolved = false;
        problemSolved = false;

        StartCoroutine(DisplayIntroCoroutine());
    }


    IEnumerator BattleCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        if (firstTimeBattleCoroutine)
        {
            //Hide the data structure and disable all objects while battling:
            dataStructure.GetComponent<ReticleController>().SetInactive();
            inputManager.DisableMouseControls();
            dataManager.SetInvisible();

            Debug.Log("TIME TO FIGHT!!! Entering Battle Phase...");
            firstTimeBattleCoroutine = false;
            promptManager.ShowTextBoxCombatStart();
            yield return new WaitForSeconds(2.0f);
            promptManager.HideTextBoxCombatStart();
            yield return new WaitForSeconds(0.5f);
            battleManager.GetBattleMenu().GetComponent<BattleMenuScript>().ShowBattleMenuMain();

            selectHand.GetComponent<SelectHandController>().ActivateSelectHand();
            selectHandActive = true;
        }

        //Player's turn to make a choice from the battle menu options:
        if (selectHandActive && selectHand.GetComponent<SelectHandController>().IsOptionSelected() == true)
        {
            selectHand.GetComponent<SelectHandController>().DeactivateSelectHand();
            selectHandActive = false;
            playerBattleTurnOver = true;
        }

        //Enemy's turn to attack the player:
        if (playerBattleTurnOver == true
            && battleManager.playerCharacter.GetComponent<PlayerController>().AttackHasEnded() == true)
        {
            //Make the enemy attack the player and allow the player to dodge the attack:
            if (enemyAlive == true)
            {
                battleManager.playerCharacter.GetComponent<PlayerController>().MovementEnabled(true);
                battleManager.enemyCharacter.GetComponent<EnemyController>().AttackPlayer();
            }
            playerBattleTurnOver = false;
            enemyBattleTurnOver = true;
        }

        //Battle ends after both the player and enemy have attacked each other:
        if (enemyBattleTurnOver == true
            && battleManager.enemyCharacter.GetComponent<EnemyController>().AttackHasEnded() == true)
        {
            //TO_DO: Implement some message prompts for the end of the battle phase--
            //  - Show some prompt indicating the battle has ended
            //  - Show some prompt indicating the solving phase is starting again
            enemyBattleTurnOver = false;
            //Disable player movement after enemy has finished their turn--
            battleManager.playerCharacter.GetComponent<PlayerController>().MovementEnabled(false);
            //battleManager.playerCharacter.GetComponent<PlayerController>().combatEnded = true;

            //Check if enemy health has reached 0 or below 0:
            //  - If enemy health is at or below 0, have enemy explode and exit the scene
            //  - NOTE: This if statement will only occur once!
            float enemyHealth = battleManager.enemyCharacter.GetComponent<EnemyController>().GetCurrentHealth();
            if (enemyAlive && enemyHealth <= 0)
            {
                enemyAlive = false;
                enemyDefeated = true;
                //When the enemy is no longer alive, have it play an exit animation (such as an explosion)
                //  - Then, either make the enemy no longer active or destroy the enemy
                battleManager.enemyCharacter.SetActive(false);
                //Need to make the enemy's resource bar no longer active--
                battleManager.HideEnemyResourceBar();
                Debug.Log("ENEMY HAS BEEN DEFEATED!!!");
            }

            if (problemSolved == false)
            {
                Debug.Log("Before updating iteration stage, it is set to: " + mainSubStep);
                mainSubStep = dataManager.GetIterationStage(); //The current step in iteration
                Debug.Log("Iteration stage is currently set to: " + mainSubStep);
                //removed: isSubStep0 = true;
                isTimeToFight = false;
                isTimeToSolve = true;
                Debug.Log("isTimeToFight: " + isTimeToFight);
                Debug.Log("isTimeToSolve: " + isTimeToSolve);

                /*subStep0*/
                //NOTE: Need to set mainSubStep to 0 after the battle phase ends
                if (mainSubStep == 0)
                {
                    dataManager.IncrementCurrentIterationFrom(mainStep);
                    mainStep = dataManager.GetIterationNum(); //Iteration number
                    dataManager.IncrementIterationStageFrom(mainSubStep);
                    mainSubStep = dataManager.GetIterationStage(); //The current step in iteration
                    Debug.Log("Entered substep 0 and incremented iteration stage to: " + mainSubStep);
                    Debug.Log("Incremented iteration number to: " + mainStep);
                    //removed: subStepIsStarting = true;
                    //removed: isSubStep1 = true;
                    isSubStep0 = true;
                    firstTimeSortCoroutine = true;
                }
            }
            else if ((problemSolved == true)) 
            {
                //If problem solved and enemy still alive, keep battling until someone loses
                //  - Then, when the player wins, exit battle phase and enter victory phase
                //  - Or, if the player loses, show the game over screen
                isTimeToSolve = false;
                isTimeToFight = false;
                if (enemyDefeated == true)
                {
                    isTimeToCelebrate = true;
                    firstTimeVictoryCoroutine = true;
                }
                else { firstTimeBattleCoroutine = true; }
            }
        }
    }//End of BattleCoroutine()




    IEnumerator ResetBattleEntryFlagCoroutine()
    {
        yield return new WaitForSeconds(1.0f);
        firstTimeBattleCoroutine = true;
    }


    //***NEED TO IMPLEMENT CURRENCY INCREMENTING WHEN CORRECT ANSWER IS CHOSEN***
    //  - Also need to implement a "SortingStreak" and "Multiplier" to reward consecutive correct
    //      answers
    //
    IEnumerator CorrectAnswer1Coroutine()
    {
        dataStructure.GetComponent<ReticleController>().StartFeedbackCorrect();
        promptManager.ShowFeedbackCorrect();
        battleManager.playerCharacter.GetComponent<PlayerController>().IncrementCurrencyCount(1);
        yield return new WaitForSeconds(2.0f);
        promptManager.HideFeedbackCorrect();
        subStepIsEnding = true;
    }

    IEnumerator CorrectAnswer2Coroutine()
    {
        dataStructure.GetComponent<ReticleController>().StartFeedbackCorrect();
        promptManager.ShowFeedbackCorrect();
        battleManager.playerCharacter.GetComponent<PlayerController>().IncrementCurrencyCount(1);
        yield return new WaitForSeconds(2.0f);
        promptManager.HideFeedbackCorrect();
        subStepIsEnding = true;
    }

    IEnumerator CorrectAnswer3Coroutine()
    {
        dataStructure.GetComponent<ReticleController>().StartFeedbackCorrect();
        promptManager.ShowFeedbackCorrect();
        battleManager.playerCharacter.GetComponent<PlayerController>().IncrementCurrencyCount(1);
        yield return new WaitForSeconds(2.0f);
        promptManager.HideFeedbackCorrect(); 
        subStepIsEnding = true;
    }

    IEnumerator CorrectAnswer4Coroutine()
    {
        dataStructure.GetComponent<ReticleController>().StartFeedbackCorrect();
        promptManager.ShowFeedbackCorrect();
        battleManager.playerCharacter.GetComponent<PlayerController>().IncrementCurrencyCount(1);
        yield return new WaitForSeconds(2.0f);
        promptManager.HideFeedbackCorrect();
        subStepIsEnding = true;
    }

    IEnumerator CorrectAnswer5Coroutine()
    {
        dataStructure.GetComponent<ReticleController>().StartFeedbackCorrect();
        promptManager.ShowFeedbackCorrect();
        battleManager.playerCharacter.GetComponent<PlayerController>().IncrementCurrencyCount(1);
        yield return new WaitForSeconds(2.0f);
        promptManager.HideFeedbackCorrect();
        subStepIsEnding = true;
    }

    //***NEED TO IMPLEMENT CURRENCY DECREMENTING WHEN WRONG ANSWER IS CHOSEN***
    //  - Also need to implement code to reset "SortingStreak" and "Multiplier"
    //
    IEnumerator WrongAnswer1Coroutine()
    {
        dataStructure.GetComponent<ReticleController>().StartFeedbackWrong();
        battleManager.playerCharacter.GetComponent<PlayerController>().DecrementCurrencyCount(1);
        promptManager.ShowFeedbackWrong();
        yield return new WaitForSeconds(2.0f);
        promptManager.HideFeedbackWrong();
        dataStructure.GetComponent<ReticleController>().SetInactive();
        inputManager.DisableMouseControls();
        dataManager.SetInvisible();
        promptManager.HideTextBoxQuestion1();
        if ((enemyAlive == true) && (enemyDefeated == false))
        {
            PunishWrongAnswer();
            enemyBattleTurnOver = true;
        }
        else if ((enemyAlive == false))
        {
            subStepIsStarting = true;
        }
    }

    IEnumerator WrongAnswer2Coroutine()
    {
        dataStructure.GetComponent<ReticleController>().StartFeedbackWrong();
        battleManager.playerCharacter.GetComponent<PlayerController>().DecrementCurrencyCount(1);
        promptManager.ShowFeedbackWrong();
        yield return new WaitForSeconds(2.0f);
        promptManager.HideFeedbackWrong();
        dataStructure.GetComponent<ReticleController>().SetInactive();
        inputManager.DisableMouseControls();
        dataManager.SetInvisible();
        promptManager.HideTextBoxQuestion2();
        if ((enemyAlive == true) && (enemyDefeated == false))
        {
            PunishWrongAnswer();
            enemyBattleTurnOver = true;
        }
        else if ((enemyAlive == false))
        {
            subStepIsStarting = true;
        }
    }

    IEnumerator WrongAnswer3Coroutine()
    {
        dataStructure.GetComponent<ReticleController>().StartFeedbackWrong();
        battleManager.playerCharacter.GetComponent<PlayerController>().DecrementCurrencyCount(1);
        promptManager.ShowFeedbackWrong();
        yield return new WaitForSeconds(2.0f);
        promptManager.HideFeedbackWrong(); 
        dataStructure.GetComponent<ReticleController>().SetInactive();
        inputManager.DisableMouseControls();
        dataManager.SetInvisible();
        promptManager.HideTextBoxQuestion3();
        if ((enemyAlive == true) && (enemyDefeated == false))
        {
            PunishWrongAnswer();
            enemyBattleTurnOver = true;
        }
        else if ((enemyAlive == false))
        {
            subStepIsStarting = true;
        }
    }

    IEnumerator WaitForSwap()
    {
        promptManager.ShowFeedbackSwapping();
        yield return new WaitForSeconds(6.0f);
        promptManager.HideFeedbackSwapping();
        yield return new WaitForSeconds(0.5f);
        hasPerformedSwap = true;
    }

    IEnumerator WrongAnswer4Coroutine()
    {
        dataStructure.GetComponent<ReticleController>().StartFeedbackWrong();
        battleManager.playerCharacter.GetComponent<PlayerController>().DecrementCurrencyCount(1);
        promptManager.ShowFeedbackWrong();
        yield return new WaitForSeconds(2.0f);
        promptManager.HideFeedbackWrong();
        dataStructure.GetComponent<ReticleController>().SetInactive();
        inputManager.DisableMouseControls();
        dataManager.SetInvisible();
        promptManager.HideTextBoxQuestion4();
        if ((enemyAlive == true) && (enemyDefeated == false))
        {
            PunishWrongAnswer();
            enemyBattleTurnOver = true;
        }
        else if ((enemyAlive == false))
        {
            subStepIsStarting = true;
        }
    }

    IEnumerator WrongAnswer5Coroutine()
    {
        dataStructure.GetComponent<ReticleController>().StartFeedbackWrong();
        battleManager.playerCharacter.GetComponent<PlayerController>().DecrementCurrencyCount(1);
        promptManager.ShowFeedbackWrong();
        yield return new WaitForSeconds(2.0f);
        promptManager.HideFeedbackWrong();
        dataStructure.GetComponent<ReticleController>().SetInactive();
        inputManager.DisableMouseControls();
        dataManager.SetInvisible();
        //TEMP REMOVAL: promptManager.HideTextBoxQuestion5();
        if ((enemyAlive == true) && (enemyDefeated == false))
        {
            PunishWrongAnswer();
            enemyBattleTurnOver = true;
        }
        else if ((enemyAlive == false))
        {
            subStepIsStarting = true;
        }
    }

    private void PunishWrongAnswer()
    {
        battleManager.playerCharacter.GetComponent<PlayerController>().IncrementMistakesMade();
        battleManager.playerCharacter.GetComponent<PlayerController>().StartEmote("Exclaim");
        //Allow the player to be able to dodge the enemy attack--
        battleManager.playerCharacter.GetComponent<PlayerController>().MovementEnabled(true);
        //Enemy attacks player
        battleManager.enemyCharacter.GetComponent<EnemyController>().AttackPlayer();
    }


    IEnumerator PresentRewardsCoroutine()
    {
        promptManager.HideFeedbackVictory();
        yield return new WaitForSeconds(1.0f);
        promptManager.ShowReward1();
        yield return new WaitForSeconds(0.5f);
        promptManager.ShowReward2();
        yield return new WaitForSeconds(0.5f);
        promptManager.ShowReward3();
        yield return new WaitForSeconds(0.5f);
        promptManager.ShowChooseRewardMessage();
        isChoosingReward = true;
    }



    //NOTE: NEED TO IMPLEMENT!!!
    IEnumerator VictoryCoroutine()
    {
        //Just using code from BattleCoroutine as a template...
        //  VVVVVVV
        yield return new WaitForSeconds(0.5f);
        if (firstTimeVictoryCoroutine)
        {
            firstTimeVictoryCoroutine = false;

            //Hide the data structure and disable all objects after victory is achieved--
            dataStructure.GetComponent<ReticleController>().SetInactive();
            inputManager.DisableMouseControls();
            dataManager.SetInvisible();

            //Disable the playerCharacter movement during results screen--
            battleManager.playerCharacter.GetComponent<PlayerController>().MovementEnabled(false);

            //Display a VICTORY message to the player--
            Debug.Log("Displaying VICTORY Message!!!");
            promptManager.ShowFeedbackVictory();
            yield return new WaitForSeconds(3.0f);
            promptManager.ShowEndResults();
            
        }

        if (promptManager.resultsChart_animator.GetBool("isVisible") == false && hasExitedEndResults == false)
        {
            hasExitedEndResults = true;
            endResultsExited = true;
        }

        if (endResultsExited == true)
        {
            endResultsExited = false;
            //Drop the three reward chests from above and display message to choose a reward--
            StartCoroutine(PresentRewardsCoroutine());

        }

        if (isChoosingReward)
        {
            //isChoosingReward = false; //NOTE:
            //exitingLevel = true;      //  Only temporarily placed here until rewards are implemented
            //Allow the player to control character movement during results screen--
            battleManager.playerCharacter.GetComponent<PlayerController>().MovementEnabled(true);


            //Wait for the player to select a reward

            //Reward 1 is chosen:
            if (reward1 != null)
            {
                if (reward1.GetComponent<RewardScript>().GetChosenStatus() == true)
                {
                    Debug.Log("A reward has been chosen! All reward chests should disappear now...");
                    isChoosingReward = false;
                    if (reward2 != null)
                    {
                        promptManager.HideReward2();
                    }
                    if (reward3 != null)
                    {
                        promptManager.HideReward3();
                    }

                    //Disable the playerCharacter movement after a reward has been selected--
                    battleManager.playerCharacter.GetComponent<PlayerController>().MovementEnabled(false);
                    exitingLevel = true;
                }
            }
            //Show the player has received the reward
            //isChoosingReward = false;
            //exitingLevel = true;

            //Reward 2 is chosen:
            if (reward2 != null)
            {
                if (reward2.GetComponent<RewardScript>().GetChosenStatus() == true)
                {
                    Debug.Log("A reward has been chosen! All reward chests should disappear now...");
                    isChoosingReward = false;
                    if (reward1 != null)
                    {
                        promptManager.HideReward1();
                    }
                    if (reward3 != null)
                    {
                        promptManager.HideReward3();
                    }

                    //Disable the playerCharacter movement after a reward has been selected--
                    battleManager.playerCharacter.GetComponent<PlayerController>().MovementEnabled(false);
                    exitingLevel = true;
                }
            }
            //Show the player has received the reward
            //isChoosingReward = false;
            //exitingLevel = true;

            //Reward 3 is chosen:
            if (reward3 != null)
            {
                if (reward3.GetComponent<RewardScript>().GetChosenStatus() == true)
                {
                    Debug.Log("A reward has been chosen! All reward chests should disappear now...");
                    isChoosingReward = false;
                    if (reward1 != null)
                    {
                        promptManager.HideReward1();
                    }
                    if (reward2 != null)
                    {
                        promptManager.HideReward2();
                    }

                    //Disable the playerCharacter movement after a reward has been selected--
                    battleManager.playerCharacter.GetComponent<PlayerController>().MovementEnabled(false);
                    exitingLevel = true;
                }
            }
            //Show the player has received the reward
            //isChoosingReward = false;
            //exitingLevel = true;

            //Decline to take a reward:
            //Bestow currency upon the player instead and show the increase
            //isChoosingReward = false;
            //exitingLevel = true;
        }

        if (exitingLevel)
        {
            promptManager.HideChooseRewardMessage();
            Debug.Log("Displaying end of level menu...");
            //Display a menu with various options
                //An option to return to the level select
                //An option to check the player's character menu
                //An option to return to the title screen
            StartCoroutine(EndOfLevelMenuCoroutine());
        }
        else
        {
            //Keep calling the VictoryCoroutine so the player can decide what to do next--
            isTimeToCelebrate = true;
        }
    }

    IEnumerator EndOfLevelMenuCoroutine()
    {
        endOfLevelMenu.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        promptManager.ShowEndOfLevelMenu();
    }

    //NOTE: NEED TO IMPLEMENT!!!
    IEnumerator DefeatCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("GAMEOVER!!! Player has been defeated...Now showing the game over screen...");
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
            else if (isSubStep5)
            {
                isSubStep5 = false;
                StartCoroutine(SortCoroutine());
            }
            else if (isSubStep6) //There really isn't a sub step 6, but it
            {                    //  transitions from the sort phase to the battle phase.
                isSubStep6 = false;
                StartCoroutine(SortCoroutine());
            }
            else if (isSubStep0) //Acts as the means of transitioning from battle phase
            {                    //  to the sort phase once again.
                Debug.Log("Entered Solve Phase Once Again!!!");
                isSubStep0 = false;
                StartCoroutine(SortCoroutine());
                StartCoroutine(ResetBattleEntryFlagCoroutine());
                // removed: subStepIsStarting = true;
                isSubStep1 = true;
            }

        }//End of sorting phase--

        //Begin of battle phase--
        else if (isTimeToFight)
        {
            StartCoroutine(BattleCoroutine());
            /*
            isTimeToFight = false; //Only temporarily placed here. Really, it should be
                                   //  in the BattleCoroutine when the battle ends
            isTimeToSolve = true;  //Only temporarily placed here. This should be triggered
                                   //  once the battle ends and if the problem isn't solved yet
            isSubStep0 = true;     //This should be triggered once the battle ends and
                                   //  isTimeToSolve is true again
            */
        }//End of battle phase--


        //We might want to put this above the "isTimeToSolve" if statement, and
        // make that statement an else if statement instead.
        else if (problemSolved && (enemyAlive == true) && (isTimeToFight == false))
        {
            Debug.Log("SORTING COMPLETE!!! - DATA STRUCTURE IS SORTED!!!");
            //Shift permanently to battle phase if enemy is still alive
            isTimeToSolve = false;
            isTimeToFight = true;
        }
        else if (problemSolved && (enemyDefeated == true) && (isTimeToCelebrate == true))
        {
            Debug.Log("YOU WIN!!! - SORTING COMPLETE AND ENEMY DEFEATED!!!");
            isTimeToCelebrate = false; //We only want the victory phase to occur once

            //Display victory screen with results/rewards earned
            StartCoroutine(VictoryCoroutine());
            //Then, display the level select scene
        }
    }//End of Update()

    
}//End of LevelStartCoroutine
