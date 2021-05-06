using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashAbility : MonoBehaviour
{

    [SerializeField] int cost;
    [SerializeField] string description;
    [SerializeField] Text descriptionAndCost;
    [SerializeField] bool inRange;


    // Start is called before the first frame update
    void Start()
    {
        cost = 7;
        description = "Dash";
        descriptionAndCost.text = description + System.Environment.NewLine + "Cost: " + cost + System.Environment.NewLine + "Press E to buy";
        descriptionAndCost.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        descriptionAndCost.text = description + System.Environment.NewLine + "Cost: " + cost + System.Environment.NewLine + "Press E to buy";

        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            if (PersistentData.Instance.GetCoin() >= cost)
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<CadetController>().dash = true;
                PersistentData.Instance.SetCoin(PersistentData.Instance.GetCoin() - cost);
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

        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            descriptionAndCost.gameObject.SetActive(false);
            inRange = false;

        }
    }
}
