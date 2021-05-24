using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PromptManager : MonoBehaviour
{
    public static PromptManager instance;

    public GameObject aimReticle;

    public Animator intro_animator;
    public Animator instructions_animator;
    public Animator question1_animator;
    public Animator question2_animator;
    public Animator question3_animator;
    public Animator swapIt_animator;
    public Animator question4_animator;
    public Animator question5_animator;
    public Animator combatStart_animator;
    public Animator actionConfirm_animator;

    public GameObject feedbackMessage_Correct;
    public GameObject feedbackMessage_Wrong;
    public GameObject feedbackMessage_Swapping;
    public GameObject feedbackMessage_Victory;
    public GameObject feedbackMessage_Nice;
    public GameObject feedbackMessage_Great;
    public GameObject feedbackMessage_Oops;
    public GameObject feedbackMessage_Miss;

    public Animator swappingMessage_animator;
    public Animator dataMessage_animator;
    public Animator loadingCircle_animator;
    public Animator niceMessage_animator;
    public Animator greatMessage_animator;
    public Animator oopsMessage_animator;
    public Animator missMessage_animator;

    public Animator goldCoin_animator;

    public Animator reward1_animator;
    public Animator reward2_animator;
    public Animator reward3_animator;
    public GameObject feedbackMessage_ChooseReward;
    public Animator chooseRewardMessage_animator;

    public Animator resultsChart_animator;
    public Animator button_Ok_animator;
    public Animator resultsStats_animator;
    public Animator endOfLevelMenu_animator;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (aimReticle == null)
        {
            GameObject[] gameObjects = SceneManager.GetActiveScene().GetRootGameObjects();
            for (int i = 0; i < gameObjects.Length; i++)
            {
                if (gameObjects[i].name == "AimReticle")
                {
                    aimReticle = gameObjects[i];
                }
            }
        }
    }

    //TextBox_Intro Methods--
    public void ShowTextBoxIntro()
    {
        intro_animator.SetBool("isVisible", true);
    }

    public void HideTextBoxIntro()
    {
        intro_animator.SetBool("isVisible", false);
    }


    //TextBox_Instructions Methods--
    public void ShowTextBoxInstructions()
    {
        instructions_animator.SetBool("isVisible", true);
    }

    public void HideTextBoxInstructions()
    {
        instructions_animator.SetBool("isVisible", false);
    }


    //TextBox_Question1 Methods--
    public void ShowTextBoxQuestion1()
    {
        question1_animator.SetBool("isVisible", true);
    }

    public void HideTextBoxQuestion1()
    {
        question1_animator.SetBool("isVisible", false);
    }


    //TextBox_Question2 Methdos--
    public void ShowTextBoxQuestion2()
    {
        question2_animator.SetBool("isVisible", true);
    }

    public void HideTextBoxQuestion2()
    {
        question2_animator.SetBool("isVisible", false);
    }


    //TextBox_Question3 Methods--
    public void ShowTextBoxQuestion3()
    {
        question3_animator.SetBool("isVisible", true);
    }

    public void HideTextBoxQuestion3()
    {
        question3_animator.SetBool("isVisible", false);
    }


    //TextBox_SwapIt Methods--
    public void ShowTextBoxSwapIt()
    {
        swapIt_animator.SetBool("isVisible", true);
    }

    public void HideTextBoxSwapIt()
    {
        swapIt_animator.SetBool("isVisible", false);
    }


    //TextBox_Question4 Methods--
    public void ShowTextBoxQuestion4()
    {
        question4_animator.SetBool("isVisible", true);
    }

    public void HideTextBoxQuestion4()
    {
        question4_animator.SetBool("isVisible", false);
    }


    //TextBox_Question5 Methods--
    public void ShowTextBoxQuestion5()
    {
        question5_animator.SetBool("isVisible", true);
    }

    public void HideTextBoxQuestion5()
    {
        question5_animator.SetBool("isVisible", false);
    }


    //TextBox_CombatStart Methods--
    public void ShowTextBoxCombatStart()
    {
        combatStart_animator.SetBool("isVisible", true);
    }

    public void HideTextBoxCombatStart()
    {
        combatStart_animator.SetBool("isVisible", false);
    }


    //TextBox_ActionConfirm Methods--
    public void ShowTextBoxActionConfirm()
    {
        actionConfirm_animator.SetBool("isVisible", true);
    }

    public void HideTextBoxActionConfirm()
    {
        actionConfirm_animator.SetBool("isVisible", false);
    }


    //Custom Feedback Message Methods--

    //Feedback Message - Correct
    public void ShowFeedbackCorrect()
    {
        feedbackMessage_Correct.SetActive(true);
    }
    public void HideFeedbackCorrect()
    {
        feedbackMessage_Correct.SetActive(false);
    }

    //Feedback Message - Wrong
    public void ShowFeedbackWrong()
    {
        feedbackMessage_Wrong.SetActive(true);
    }
    public void HideFeedbackWrong()
    {
        feedbackMessage_Wrong.SetActive(false);
    }

    //Feedback Message - Victory
    public void ShowFeedbackVictory()
    {
        feedbackMessage_Victory.SetActive(true);
    }
    public void HideFeedbackVictory()
    {
        feedbackMessage_Victory.SetActive(false);
    }

    //Feedback Message - Swapping Data...
    public void ShowFeedbackSwapping()
    {
        feedbackMessage_Swapping.SetActive(true);
        swappingMessage_animator.SetBool("isVisible", true);
        dataMessage_animator.SetBool("isVisible", true);
        loadingCircle_animator.SetBool("isVisible", true);
    }
    public void HideFeedbackSwapping()
    {
        feedbackMessage_Swapping.SetActive(false);
        swappingMessage_animator.SetBool("isVisible", false);
        dataMessage_animator.SetBool("isVisible", false);
        loadingCircle_animator.SetBool("isVisible", false);
    }




    //Player Attack Feedback Messages:

    //Feedback Message - Nice--
    public void ShowFeedbackNice()
    {
        feedbackMessage_Nice.SetActive(true);
        feedbackMessage_Nice.transform.position = new Vector3(aimReticle.transform.position.x - 0.75f,
                                                                aimReticle.transform.position.y + 0.95f,
                                                                aimReticle.transform.position.z);
        niceMessage_animator.SetBool("isVisible", true);
    }
    public void HideFeedbackNice()
    {
        feedbackMessage_Nice.SetActive(false);
        niceMessage_animator.SetBool("isVisible", false);
    }

    //Feeback Message - Great--
    public void ShowFeedbackGreat()
    {
        feedbackMessage_Great.SetActive(true);
        feedbackMessage_Great.transform.position = new Vector3(aimReticle.transform.position.x + 0.75f,
                                                                aimReticle.transform.position.y + 0.95f,
                                                                aimReticle.transform.position.z);
        greatMessage_animator.SetBool("isVisible", true);
    }
    public void HideFeedbackGreat()
    {
        feedbackMessage_Great.SetActive(false);
        greatMessage_animator.SetBool("isVisible", false);
    }

    //Feedback Message - Oops--
    public void ShowFeedbackOops()
    {
        feedbackMessage_Oops.SetActive(true);
        oopsMessage_animator.SetBool("isVisible", true);
    }

    public void HideFeedbackOops()
    {
        feedbackMessage_Oops.SetActive(false);
        oopsMessage_animator.SetBool("isVisible", false);
    }

    //Feedback Message - Miss--
    public void ShowFeedbackMiss()
    {
        feedbackMessage_Miss.SetActive(true);
        missMessage_animator.SetBool("isVisible", true);
    }
    public void HideFeedbackMiss()
    {
        feedbackMessage_Miss.SetActive(false);
        missMessage_animator.SetBool("isVisible", false);
    }




    //Feedback Message - Sorted


    //Feedback Message - Enemy Defeated


    //Feedback Message - Defeat




    //Currency Animation Methods--
    IEnumerator WaitForCoinSpinCoroutine()
    {
        yield return new WaitForSeconds(1.0f);
        //Reset the goldCoin increment animation to the idle animation:
        if (goldCoin_animator.GetBool("earnedCoin") == true)
        {
            goldCoin_animator.SetBool("earnedCoin", false);
        }

        //Reset the goldCoin decrement animation to the idle animation:
        if (goldCoin_animator.GetBool("lostCoin") == true)
        {
            goldCoin_animator.SetBool("lostCoin", false);
        }
    }
    public void ShowCurrencyIncremented()
    {
        goldCoin_animator.SetBool("earnedCoin", true);
        StartCoroutine(WaitForCoinSpinCoroutine());
    }
    public void ShowCurrencyDecremented()
    {
        goldCoin_animator.SetBool("lostCoin", true);
        StartCoroutine(WaitForCoinSpinCoroutine());
    }


    //End Results Chart Methods--
    public void ShowEndResults()
    {
        resultsChart_animator.SetBool("isVisible", false);
        StartCoroutine(ShowResultsStatsCoroutine());
        StartCoroutine(ShowButtonOkCoroutine());
    }
    public void HideEndResults()
    {
        button_Ok_animator.SetBool("isVisible", false);
        resultsStats_animator.SetBool("isVisible", false);
        StartCoroutine(HideEndResultsCoroutine());
    }

    IEnumerator ShowResultsStatsCoroutine()
    {
        yield return new WaitForSeconds(1.0f);
        resultsStats_animator.SetBool("isVisible", true);
    }

    IEnumerator ShowButtonOkCoroutine()
    {
        yield return new WaitForSeconds(5.0f);
        button_Ok_animator.SetBool("isVisible", true); 
    }

    IEnumerator HideEndResultsCoroutine()
    {
        yield return new WaitForSeconds(0.3f);
        resultsChart_animator.SetBool("isVisible", false);
    }


    //Reward Related Methods--
    public void ShowReward1()
    {
        reward1_animator.gameObject.GetComponent<RewardScript>().isVisible = true;
    }
    public void HideReward1()
    {
        reward1_animator.gameObject.GetComponent<RewardScript>().isVisible = false;
    }

    public void ShowReward2()
    {
        reward2_animator.gameObject.GetComponent<RewardScript>().isVisible = true;
    }
    public void HideReward2()
    {
        reward2_animator.gameObject.GetComponent<RewardScript>().isVisible = false;
    }

    public void ShowReward3()
    {
        reward3_animator.gameObject.GetComponent<RewardScript>().isVisible = true;
    }
    public void HideReward3()
    {
        reward3_animator.gameObject.GetComponent<RewardScript>().isVisible = false;
    }

    //***NOTE: NEED TO IMPLEMENT AND INITIALIZE chooseRewardMessage_animator
    //  - When displaying this message at the end results screen, make sure to hide the victoryMessage
    //  - Then, after the player has chosen a reward, make the chooseRewardMessage disappear
    public void ShowChooseRewardMessage()
    {
        if (feedbackMessage_ChooseReward != null)
        {
            feedbackMessage_ChooseReward.SetActive(true);
            chooseRewardMessage_animator.SetBool("isVisible", true);

        }
    }
    public void HideChooseRewardMessage()
    {
        if (feedbackMessage_ChooseReward != null)
        {
            feedbackMessage_ChooseReward.SetActive(false);
            chooseRewardMessage_animator.SetBool("isVisible", false);

        }
    }


    //EndOfLevel Menu Methods--
    public void ShowEndOfLevelMenu()
    {
        endOfLevelMenu_animator.SetBool("isVisible", true);
    }
    public void HideEndOfLevelMenu()
    {
        endOfLevelMenu_animator.SetBool("isVisible", false);
    }
}
