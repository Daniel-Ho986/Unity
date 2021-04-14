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
    public Animator combatStart_animator;
    public Animator actionConfirm_animator;

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


}
