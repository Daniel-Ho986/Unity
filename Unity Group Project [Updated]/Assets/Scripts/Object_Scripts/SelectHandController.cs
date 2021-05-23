using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectHandController : MonoBehaviour
{
    public GameObject playerCharacter;

    public GameObject battleMenu;
    public GameObject attackOptionMenu;
    public GameObject currentMenu;

    public GameObject attackOption;
    public GameObject itemOption;
    public GameObject keyOption;
    public GameObject runOption;

    public GameObject attackMenuOption1;
    public GameObject attackMenuBackButton;

    public InputManager inputManager;

    public bool isEnabled;
    public bool optionSelected;
    public bool battleMenuVisible;
    public bool attackMenuVisible;

    private GameObject currentOption;
    private GameObject leftNeighbor;
    private GameObject rightNeighbor;

    // Start is called before the first frame update
    void Start()
    {
        /* This code does not work as intended (only for reference)--
        if (attackOption != null)
        {
            if (attackOption.tag == "MenuOption")
            {
                gameObject.transform.position = new Vector3((attackOption.transform.position.x + 0.1125f),
                                                            (attackOption.transform.position.y - 0.137f),
                                                            gameObject.transform.position.z);
            }
        }
        */

        //On start, have the SelectHand be positoned on the attackOption
        if (currentOption == null)
        {
            if (attackOption != null)
            {
                currentOption = attackOption;
                leftNeighbor = attackOption.GetComponent<MenuOptionScript>().GetLeftNeighbor();
                rightNeighbor = attackOption.GetComponent<MenuOptionScript>().GetRightNeighbor();
            }
        }

        //On start, have the SelectHand be in the battleMenu
        if (currentMenu == null)
        {
            if (battleMenu != null)
            {
                currentMenu = battleMenu;
            }
        }


        inputManager = InputManager.instance;

        isEnabled = false;

        battleMenuVisible = false;
        attackMenuVisible = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (isEnabled == false)
        {
            //Resets these menu visibility variables whenever the select hand is activated:
            battleMenuVisible = false;
            attackMenuVisible = false;

            if (attackOption != null)
            {
                if (attackOption.tag == "MenuOption")
                {
                    gameObject.transform.position = new Vector3((attackOption.transform.position.x + 0.3f),
                                                            (attackOption.transform.position.y - 0.5f),
                                                            gameObject.transform.position.z);
                }
            }
            isEnabled = true;
            optionSelected = false;
        }

        if (currentMenu == battleMenu)
        {
            if (battleMenu.GetComponent<BattleMenuScript>().IsVisible() == true && battleMenuVisible == false)
            {
                StartCoroutine(WaitForBattleMenuCoroutine());
            }
            else if (battleMenu.GetComponent<BattleMenuScript>().IsVisible() == false && battleMenuVisible == true)
            {
                StartCoroutine(WaitForBattleMenuCoroutine());
            }
            
            //Move the SelectHand to the left MenuOption when the left arrow key is pressed:
            if (inputManager.GetKeyDown(KeyBindingActions.Left1)
                || inputManager.GetKeyDown(KeyBindingActions.Left2))
            {
                gameObject.transform.position = new Vector3((leftNeighbor.transform.position.x + 0.3f),
                                                        (leftNeighbor.transform.position.y - 0.5f),
                                                        gameObject.transform.position.z);
                currentOption = leftNeighbor;
                leftNeighbor = currentOption.GetComponent<MenuOptionScript>().GetLeftNeighbor();
                rightNeighbor = currentOption.GetComponent<MenuOptionScript>().GetRightNeighbor();

            }

            //Move the SelectHand to the right MenuOption when the right arrow key is pressed:
            if (inputManager.GetKeyDown(KeyBindingActions.Right1)
                || inputManager.GetKeyDown(KeyBindingActions.Right2))
            {
                gameObject.transform.position = new Vector3((rightNeighbor.transform.position.x + 0.3f),
                                                        (rightNeighbor.transform.position.y - 0.5f),
                                                        gameObject.transform.position.z);
                currentOption = rightNeighbor;
                leftNeighbor = currentOption.GetComponent<MenuOptionScript>().GetLeftNeighbor();
                rightNeighbor = currentOption.GetComponent<MenuOptionScript>().GetRightNeighbor();
            }


            //Select the highlighted MenuOption that the SelectHand is hovering over when the
            //  "E" key or "return" key is pressed:
            if (inputManager.GetKeyDown(KeyBindingActions.Select1)
                || inputManager.GetKeyDown(KeyBindingActions.Select2))
            {
                StartCoroutine(BattleMenuSelectCoroutine());
            }
        }//End of battleMenu if-statement

        else if (currentMenu == attackOptionMenu)
        {
            if (attackOptionMenu.GetComponent<AttackMenuScript>().IsVisible() == true && attackMenuVisible == false)
            {
                StartCoroutine(WaitForAttackMenuCoroutine());
            }
            else if (attackOptionMenu.GetComponent<AttackMenuScript>().IsVisible() == false && attackMenuVisible == true)
            {
                StartCoroutine(WaitForAttackMenuCoroutine());
            }

            //Move the SelectHand to the left MenuOption when the left arrow key is pressed:
            if (inputManager.GetKeyDown(KeyBindingActions.Left1)
                || inputManager.GetKeyDown(KeyBindingActions.Left2))
            {

                if (currentOption.GetComponent<MenuOptionScript>().GetLeftNeighbor().tag == "Button")
                {
                    gameObject.transform.position = new Vector3((leftNeighbor.transform.position.x + 0.3f),
                                                        (leftNeighbor.transform.position.y - 0.5f),
                                                        gameObject.transform.position.z);
                    currentOption = leftNeighbor;
                    leftNeighbor = currentOption.GetComponent<MenuOptionScript>().GetLeftNeighbor();
                    rightNeighbor = currentOption.GetComponent<MenuOptionScript>().GetRightNeighbor();
                }
                else if (currentOption.GetComponent<MenuOptionScript>().GetLeftNeighbor().tag == "AttackOption")
                {
                    gameObject.transform.position = new Vector3((leftNeighbor.transform.position.x + 1.2f),
                                                        (leftNeighbor.transform.position.y - 0.5f),
                                                        gameObject.transform.position.z);
                    currentOption = leftNeighbor;
                    leftNeighbor = currentOption.GetComponent<MenuOptionScript>().GetLeftNeighbor();
                    rightNeighbor = currentOption.GetComponent<MenuOptionScript>().GetRightNeighbor();
                }

            }

            //Move the SelectHand to the right MenuOption when the right arrow key is pressed:
            if (inputManager.GetKeyDown(KeyBindingActions.Right1)
                || inputManager.GetKeyDown(KeyBindingActions.Right2))
            {
                if (currentOption.GetComponent<MenuOptionScript>().GetRightNeighbor().tag == "Button")
                {
                    gameObject.transform.position = new Vector3((rightNeighbor.transform.position.x + 0.3f),
                                                        (rightNeighbor.transform.position.y - 0.5f),
                                                        gameObject.transform.position.z);
                    currentOption = rightNeighbor;
                    leftNeighbor = currentOption.GetComponent<MenuOptionScript>().GetLeftNeighbor();
                    rightNeighbor = currentOption.GetComponent<MenuOptionScript>().GetRightNeighbor();
                }
                else if (currentOption.GetComponent<MenuOptionScript>().GetRightNeighbor().tag == "AttackOption")
                {
                    gameObject.transform.position = new Vector3((rightNeighbor.transform.position.x + 1.2f),
                                                        (rightNeighbor.transform.position.y - 0.5f),
                                                        gameObject.transform.position.z);
                    currentOption = rightNeighbor;
                    leftNeighbor = currentOption.GetComponent<MenuOptionScript>().GetLeftNeighbor();
                    rightNeighbor = currentOption.GetComponent<MenuOptionScript>().GetRightNeighbor();
                }
            }

            //Select the highlighted attackMenuOption that the SelectHand is hovering over when the
            //  "E" key or "return" key is pressed:
            if (inputManager.GetKeyDown(KeyBindingActions.Select1)
                || inputManager.GetKeyDown(KeyBindingActions.Select2))
            {
                StartCoroutine(AttackMenuSelectCoroutine());
            }
        }//End of attackOptionMenu
        
    }

    void FixedUpdate()
    {

    }


    IEnumerator WaitForBattleMenuCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        currentOption = attackOption;
        leftNeighbor = attackOption.GetComponent<MenuOptionScript>().GetLeftNeighbor();
        rightNeighbor = attackOption.GetComponent<MenuOptionScript>().GetRightNeighbor();
        battleMenuVisible = battleMenu.GetComponent<BattleMenuScript>().IsVisible();
        gameObject.transform.position = new Vector3((attackOption.transform.position.x + 0.3f),
                            (attackOption.transform.position.y - 0.5f),
                            gameObject.transform.position.z);
    }

    IEnumerator WaitForAttackMenuCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        currentOption = attackMenuOption1;
        leftNeighbor = attackMenuOption1.GetComponent<MenuOptionScript>().GetLeftNeighbor();
        rightNeighbor = attackMenuOption1.GetComponent<MenuOptionScript>().GetRightNeighbor();
        attackMenuVisible = attackOptionMenu.GetComponent<AttackMenuScript>().IsVisible();
        gameObject.transform.position = new Vector3((attackMenuOption1.transform.position.x + 1.2f),
                    (attackMenuOption1.transform.position.y - 0.5f),
                    gameObject.transform.position.z);
    }

    IEnumerator BattleMenuSelectCoroutine()
    {
        if (currentMenu = battleMenu)
        {
            //Get the letters that compose the name of the MenuOption selected:
            GameObject letter1 = null;
            GameObject letter2 = null;
            GameObject letter3 = null;

            if (currentOption == attackOption)
            {
                letter1 = currentOption.transform.Find("PixelFont_Letter_A").gameObject;
                letter2 = currentOption.transform.Find("PixelFont_Letter_T").gameObject;
                letter3 = currentOption.transform.Find("PixelFont_Letter_K").gameObject;
            }
            else if (currentOption == itemOption)
            {
                letter1 = currentOption.transform.Find("PixelFont_Letter_I").gameObject;
                letter2 = currentOption.transform.Find("PixelFont_Letter_T").gameObject;
                letter3 = currentOption.transform.Find("PixelFont_Letter_M").gameObject;
            }
            else if (currentOption == keyOption)
            {
                letter1 = currentOption.transform.Find("PixelFont_Letter_K").gameObject;
                letter2 = currentOption.transform.Find("PixelFont_Letter_E").gameObject;
                letter3 = currentOption.transform.Find("PixelFont_Letter_Y").gameObject;
            }
            else if (currentOption == runOption)
            {
                letter1 = currentOption.transform.Find("PixelFont_Letter_R").gameObject;
                letter2 = currentOption.transform.Find("PixelFont_Letter_U").gameObject;
                letter3 = currentOption.transform.Find("PixelFont_Letter_N").gameObject;
            }


            //Make the selected MenuOption label blink:
            if (letter1 != null && letter2 != null && letter3 != null)
            {
                letter1.GetComponent<SpriteRenderer>().enabled = false;
                letter2.GetComponent<SpriteRenderer>().enabled = false;
                letter3.GetComponent<SpriteRenderer>().enabled = false;
            }
            yield return new WaitForSeconds(0.1f);
            if (letter1 != null && letter2 != null && letter3 != null)
            {
                letter1.GetComponent<SpriteRenderer>().enabled = true;
                letter2.GetComponent<SpriteRenderer>().enabled = true;
                letter3.GetComponent<SpriteRenderer>().enabled = true;
            }


            //Switch to the SelectHand's highlighted MenuOption:
            if (currentOption == attackOption)
            {
                if (battleMenu != null)
                {
                    battleMenu.GetComponent<BattleMenuScript>().HideBattleMenuMain();
                }
                attackOptionMenu = battleMenu.GetComponent<BattleMenuScript>().GetAttackOptionMenu();
                yield return new WaitForSeconds(0.1f);
                attackOptionMenu.GetComponent<AttackMenuScript>().ShowAttackMenuMain();
                currentMenu = attackOptionMenu;

                yield return new WaitForSeconds(0.1f);

                //Returns the GameObject that is labeled with an attack name and can be selected by the SelectHand
                attackMenuOption1 = attackOptionMenu.GetComponent<AttackMenuScript>().GetOption(1);
                if (attackMenuOption1 != null)
                {
                    currentOption = attackMenuOption1;
                }
                if (attackMenuOption1.GetComponent<MenuOptionScript>().GetLeftNeighbor() != null)
                {
                    leftNeighbor = attackMenuOption1.GetComponent<MenuOptionScript>().GetLeftNeighbor();
                }
                if (attackMenuOption1.GetComponent<MenuOptionScript>().GetRightNeighbor() != null)
                {
                    rightNeighbor = attackMenuOption1.GetComponent<MenuOptionScript>().GetRightNeighbor();
                }

                //Set the SelectHand position on the first attackOption:
                gameObject.transform.position = new Vector3((attackMenuOption1.transform.position.x + 0.5f),
                                                        attackMenuOption1.transform.position.y,
                                                        gameObject.transform.position.z);
            }
            else if (currentOption == itemOption)
            {
                if (battleMenu != null)
                {
                    //battleMenu.GetComponent<BattleMenuScript>().HideBattleMenuMain();
                }
                //Display itemOptionMenu

                //NOT IMPLEMENTED: Temporarily returns the player to the battleMenu--

            }
            else if (currentOption == keyOption)
            {
                if (battleMenu != null)
                {
                    //battleMenu.GetComponent<BattleMenuScript>().HideBattleMenuMain();
                }
                //Display keyOptionMenu

                //NOT IMPLEMENTED: Temporarily returns the player to the battleMenu--
            }
            else if (currentOption == runOption)
            {
                if (battleMenu != null)
                {
                    //battleMenu.GetComponent<BattleMenuScript>().HideBattleMenuMain();
                }
                //Display runOptionMenu

                //NOT IMPLEMENTED: Temporarily returns the player to the battleMenu--
            }
        } 
    }

    //***TO_DO NOTE:***
    //  - The select hand only appears on the attack menu for the first battle phase
    //      --> We need to figure out why and have the select hand properly switch
    //      --> The issue may be caused by setting the select hand to stick to the
    //            atttack menu option in the main battle menu
    //      --> Maybe have a method in the attack menu script for getting the select
    //            hand to reposition to the attack menu's attack option
    //
    //  SOLUTION: Both the battleMenu and the attackOptionMenu are set to visible at the same
    //              time (or the attackOptionMenu is never set back to invisible)
    //*****************
    IEnumerator AttackMenuSelectCoroutine()
    {

        if (currentMenu == attackOptionMenu)
        {

            //Get the name of the attackMenuOption make it flash when selected:
            if (currentOption == attackMenuOption1)
            {
                attackMenuOption1.GetComponent<AttackMenuOption1Script>().SetLabelInvisible();
                yield return new WaitForSeconds(0.1f);
                attackMenuOption1.GetComponent<AttackMenuOption1Script>().SetLabelVisible();

                if (attackOptionMenu != null)
                {
                    attackOptionMenu.GetComponent<AttackMenuScript>().HideAttackMenuMain();
                }
                currentMenu = battleMenu;

                yield return new WaitForSeconds(0.1f);

                //Returns the GameObject that is labeled with an attack name and can be selected by the SelectHand
                attackOption = battleMenu.GetComponent<BattleMenuScript>().GetOption(1);
                if (attackOption != null)
                {
                    currentOption = attackOption;
                }
                if (attackOption.GetComponent<MenuOptionScript>().GetLeftNeighbor() != null)
                {
                    leftNeighbor = attackOption.GetComponent<MenuOptionScript>().GetLeftNeighbor();
                }
                if (attackOption.GetComponent<MenuOptionScript>().GetRightNeighbor() != null)
                {
                    rightNeighbor = attackOption.GetComponent<MenuOptionScript>().GetRightNeighbor();
                }

                //Set the SelectHand position on the first attackOption:
                gameObject.transform.position = new Vector3((attackOption.transform.position.x + 0.5f),
                                                        attackOption.transform.position.y,
                                                        gameObject.transform.position.z);


                //Allow the player to begin aiming an attack:
                if (playerCharacter != null)
                {
                    if (playerCharacter.tag == "Player")
                    {
                        playerCharacter.GetComponent<PlayerController>().TakeAim();
                        optionSelected = true;
                    }
                }

            }
            else if (currentOption == attackMenuBackButton)
            {
                attackMenuBackButton.GetComponent<AttackMenuOption2Script>().SetLabelInvisible();
                yield return new WaitForSeconds(0.1f);
                attackMenuBackButton.GetComponent<AttackMenuOption2Script>().SetLabelVisible();

                //Get the name of the attackMenuOption make it flash when selected:
                if (attackOptionMenu != null)
                {
                    attackOptionMenu.GetComponent<AttackMenuScript>().HideAttackMenuMain();
                }
                battleMenu = attackOptionMenu.GetComponent<AttackMenuScript>().GetBattleMenu();
                yield return new WaitForSeconds(0.1f);
                battleMenu.GetComponent<BattleMenuScript>().ShowBattleMenuMain();
                currentMenu = battleMenu;

                yield return new WaitForSeconds(0.1f);

                //Returns the GameObject that is labeled with an attack name and can be selected by the SelectHand
                attackOption = battleMenu.GetComponent<BattleMenuScript>().GetOption(1);
                if (attackOption != null)
                {
                    currentOption = attackOption;
                }
                if (attackOption.GetComponent<MenuOptionScript>().GetLeftNeighbor() != null)
                {
                    leftNeighbor = attackOption.GetComponent<MenuOptionScript>().GetLeftNeighbor();
                }
                if (attackOption.GetComponent<MenuOptionScript>().GetRightNeighbor() != null)
                {
                    rightNeighbor = attackOption.GetComponent<MenuOptionScript>().GetRightNeighbor();
                }

                //Set the SelectHand position on the first attackOption:
                gameObject.transform.position = new Vector3((attackOption.transform.position.x + 0.5f),
                                                        attackOption.transform.position.y,
                                                        gameObject.transform.position.z);
            }
        }
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("SelectHand is hovering over the following: " + other.tag);
        if (other.tag == "MenuOption")
        {
            /*
            currentOption = other.gameObject;
            leftNeighbor = currentOption.GetComponent<MenuOptionScript>().GetLeftNeighbor();
            rightNeighbor = currentOption.GetComponent<MenuOptionScript>().GetRightNeighbor();
            */
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {

    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("SelectHand has moved to another object...");
    }


    //Methods for making the SelectHand active or non-active--
    public void ActivateSelectHand()
    {
        isEnabled = false;
        optionSelected = false;
        gameObject.SetActive(true);
    }
    public void DeactivateSelectHand()
    {
        gameObject.SetActive(false);
    }

    //Used in the LevelStartCoroutine to determine if a final option from the battle menu was chosen--
    public bool IsOptionSelected() { return optionSelected; }
}
