﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class campFire : MonoBehaviour
{

    [SerializeField] int cost;
    [SerializeField] string description;
    [SerializeField] TextMeshProUGUI descriptionAndCost;
    [SerializeField] bool inRange;


    // Start is called before the first frame update
    void Start()
    {
        cost = 3;
        description = "Add 5 to the health total";
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
                PersistentData.Instance.SetHealth(PersistentData.Instance.GetHealth() + 5);
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