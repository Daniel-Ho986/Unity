using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMenuScript : MonoBehaviour
{
    public GameObject battleMenu;

    public GameObject attackOption1;

    public GameObject backButton;

    public bool isVisible;
    public Animator attackMenu_animator;


    // Start is called before the first frame update
    void Start()
    {
        isVisible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ((isVisible == true) && (attackMenu_animator.GetBool("isVisible") == false))
        {
            attackMenu_animator.SetBool("isVisible", true);
        }

        if ((isVisible == false) && (attackMenu_animator.GetBool("isVisible") == true))
        {
            attackMenu_animator.SetBool("isVisible", false);
        }
    }

    //Display Methods--
    public bool IsVisible() { return isVisible; }
    public void SetVisible(bool truthValue) { isVisible = truthValue; }

    public void ShowAttackMenuMain()
    {
        isVisible = true;
        attackMenu_animator.SetBool("isVisible", true);
    }

    public void HideAttackMenuMain()
    {
        isVisible = false;
        attackMenu_animator.SetBool("isVisible", false);
    }


    //Get Methods--
    public GameObject GetBattleMenu()
    {
        if (battleMenu != null) { return battleMenu; }
        else { return null; }
    }
    public GameObject GetBackButton()
    {
        if (backButton != null) { return backButton; }
        else { return null; }
    }

    public GameObject GetOption(int optionNum)
    {
        if (optionNum == 1)
        {
            if (attackOption1 != null) { return attackOption1; }
            else { return null; }
        }
        else if (optionNum == 2)
        {
            if (backButton != null) { return backButton; }
            else { return null; }
        }
        else { return null; }
    }

}
