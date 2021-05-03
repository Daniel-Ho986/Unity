using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public GameObject playerCharacter;
    public GameObject playerResourceBar;

    public GameObject enemyCharacter;
    public GameObject enemyResourceBar;

    public GameObject dataStructure;
    public GameObject battleMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Get Methods:
    public GameObject GetBattleMenu()
    {
        if (battleMenu != null) { return battleMenu; }
        else { return null; }
    }


    public void HidePlayerResourceBar()
    {
        if (playerResourceBar != null)
        {
            playerResourceBar.SetActive(false);
        }
    }
    public void DisplayPlayerResourceBar()
    {
        if (playerResourceBar != null)
        {
            playerResourceBar.SetActive(true);
        }
    }

    public void HideEnemyResourceBar()
    {
        if (enemyResourceBar != null)
        {
            enemyResourceBar.SetActive(false);
        }
    }
    public void DisplayEnemyResourceBar()
    {
        if (enemyResourceBar != null)
        {
            enemyResourceBar.SetActive(true);
        }
    }
}
