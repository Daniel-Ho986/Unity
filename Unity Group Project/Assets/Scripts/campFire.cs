using System.Collections;
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


    //Item Sound Effect
    public AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        if (audio == null){
            audio = GetComponent<AudioSource>();
        }

        cost = 10;
        description = "Restore 5 health (Maximum 10 hp)";
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
                if(PersistentData.Instance.GetCurrentHealth() >= 5){
                    PersistentData.Instance.SetCurrentHealth(10);
                } else{
                    PersistentData.Instance.SetCurrentHealth(PersistentData.Instance.GetCurrentHealth() + 5);
                }
                
                PersistentData.Instance.SetCurrency(PersistentData.Instance.GetCurrency() - cost);

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