using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RewardHealthBoostScript : MonoBehaviour
{
    [SerializeField] int cost;
    [SerializeField] string description;
    [SerializeField] TextMeshProUGUI descriptionAndCost;
    [SerializeField] bool inRange;

    private GameObject playerCharacter;


    // Start is called before the first frame update
    void Start()
    {
        cost = 0;
        description = "Increase health points by 5";
        descriptionAndCost.text = description + System.Environment.NewLine + "Cost: " + cost + System.Environment.NewLine + "Press E to choose";
        descriptionAndCost.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        descriptionAndCost.text = description + System.Environment.NewLine + "Cost: " + cost + System.Environment.NewLine + "Press E to choose";

        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            if (PersistentData.Instance.GetCurrency() >= cost)
            {
                if (playerCharacter != null)
                {
                    playerCharacter.GetComponent<PlayerController>().SetMaxHealth(playerCharacter.GetComponent<PlayerController>().GetMaxHealth() + 5);
                    playerCharacter.GetComponent<PlayerController>().SetCurrentHealth(playerCharacter.GetComponent<PlayerController>().GetMaxHealth());
                    playerCharacter.GetComponent<PlayerController>().SetPrevHealth(playerCharacter.GetComponent<PlayerController>().GetMaxHealth());
                }
                PersistentData.Instance.SetMaxHealth(PersistentData.Instance.GetMaxHealth() + 5);
                PersistentData.Instance.SetCurrentHealth(PersistentData.Instance.GetMaxHealth());
                PersistentData.Instance.SetPrevHealth(PersistentData.Instance.GetMaxHealth());
                PersistentData.Instance.SetCurrency(PersistentData.Instance.GetCurrency() - cost);
                if (gameObject.GetComponent<RewardScript>() != null)
                {
                    gameObject.GetComponent<RewardScript>().wasChosen = true;
                }
                descriptionAndCost.gameObject.SetActive(false);
                gameObject.GetComponent<Renderer>().enabled = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            descriptionAndCost.gameObject.SetActive(true);
            inRange = true;

            playerCharacter = collision.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            descriptionAndCost.gameObject.SetActive(false);
            inRange = false;

            playerCharacter = collision.gameObject;

        }
    }
}
