using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleMenuScript : MonoBehaviour
{
    public GameObject attackOptionMenu;

    public GameObject attackOption;
    public GameObject itemOption;
    public GameObject keyOption;
    public GameObject runOption;

    public bool isVisible;
    public Animator battleMenu_animator;

    // Start is called before the first frame update
    void Start()
    {
        isVisible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ((isVisible == true) && (battleMenu_animator.GetBool("isVisible") == false))
        {
            battleMenu_animator.SetBool("isVisible", true);
        }

        if ((isVisible == false) && (battleMenu_animator.GetBool("isVisible") == true))
        {
            battleMenu_animator.SetBool("isVisible", false);
        }
    }

    public bool IsVisible() { return isVisible; }
    public void SetVisible(bool truthValue) { isVisible = truthValue; }

    public void ShowBattleMenuMain()
    {
        isVisible = true;
        battleMenu_animator.SetBool("isVisible", true);
    }

    public void HideBattleMenuMain()
    {
        isVisible = false;
        battleMenu_animator.SetBool("isVisible", false);
    }

    public GameObject GetAttackOptionMenu()
    {
        if (attackOptionMenu != null)
        {
            return attackOptionMenu;
        }
        else { return null; }
    }

    public GameObject GetOption(int optionNum)
    {
        if (optionNum == 1)
        {
            if (attackOption != null) { return attackOption; }
            else { return null; }
        }
        else { return null; }
    }
}
