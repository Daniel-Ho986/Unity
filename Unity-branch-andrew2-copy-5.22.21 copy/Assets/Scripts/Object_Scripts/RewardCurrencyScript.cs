using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RewardCurrencyScript : MonoBehaviour
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
        description = "Receive 10 Gideon Coins";
        descriptionAndCost.text = description + System.Environment.NewLine + System.Environment.NewLine + "Press E to choose";
        descriptionAndCost.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        descriptionAndCost.text = description + System.Environment.NewLine + System.Environment.NewLine + "Press E to choose";

        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            if (PersistentData.Instance.GetCurrency() >= cost)
            {
                //PersistentData.Instance.SetCurrency(PersistentData.Instance.GetCurrency() + 10);
                if (playerCharacter != null)
                {
                    playerCharacter.GetComponent<PlayerController>().IncrementCurrencyCount(10);
                }

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
