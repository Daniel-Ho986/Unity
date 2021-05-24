using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class doubleJumpAbility : MonoBehaviour
{

    [SerializeField] int cost;
    [SerializeField] string description;
    [SerializeField] TextMeshProUGUI descriptionAndCost;
    [SerializeField] bool inRange;


    //Item Sound Effect
    public AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        if (audio == null){
            audio = GetComponent<AudioSource>();
        }
        if (PersistentData.Instance.playerAbility.Contains("Double Jump")){
            gameObject.GetComponent<Renderer>().enabled = false;
        }

        cost = 30;
        description = "Double Jump";
        descriptionAndCost.text = description + System.Environment.NewLine + "Cost: " + cost + System.Environment.NewLine + "Press E to buy";
        descriptionAndCost.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        descriptionAndCost.text = description + System.Environment.NewLine + "Cost: " + cost + System.Environment.NewLine + "Press E to buy";

        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            if (PersistentData.Instance.GetCurrency() >= cost)
            {
                //Play Item Sound Effect
                AudioSource.PlayClipAtPoint(audio.clip, transform.position);

                GameObject.FindGameObjectWithTag("Player").GetComponent<CadetController>().doublejump = true;
                PersistentData.Instance.SetCurrency(PersistentData.Instance.GetCurrency() - cost);
                PersistentData.Instance.playerAbility.Add("Double Jump");
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