using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PromptManager : MonoBehaviour
{

    public Animator intro_animator;
    public Animator instructions_animator;
    public Animator question1_animator;
    public Animator question2_animator;
    public Animator question3_animator;
    public Animator swapIt_animator;
    public Animator question4_animator;
    public Animator combatStart_animator;
    public Animator actionConfirm_animator;

    public GameObject feedbackMessage_Correct;
    public GameObject feedbackMessage_Wrong;
    public GameObject feedbackMessage_Victory;

    public Animator resultsChart_animator;
    public Animator button_Ok_animator;
    public Animator resultsStats_animator;
    public Animator endOfLevelMenu_animator;

    // Start is called before the first frame update
    void Start()
    {
        
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

    //Feedback Message - Sorted


    //Feedback Message - Enemy Defeated


    //Feedback Message - Defeat



    //End Results Chart Methods--
    public void ShowEndResults()
    {
        resultsChart_animator.SetBool("isVisible", true);
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
