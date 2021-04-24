using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpinScript : MonoBehaviour
{
    public GameObject playerCharacter;

    [SerializeField] float movementSpeed;
    [SerializeField] float damageValue;

    public bool attackSpawned;
    public bool attackStarted;
    public bool attackEnded;
    private bool justStarted;
    private Vector3 movement;

    private Vector3 ogPosition;

    // Start is called before the first frame update
    void Start()
    {
        ogPosition = gameObject.GetComponent<Transform>().position;

        movementSpeed = 0.085f;
        damageValue = 1.0f;

        attackSpawned = false;
        attackStarted = false;
        justStarted = false;
        attackEnded = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (attackStarted)
        {
            if (attackSpawned)
            {
                gameObject.SetActive(true);
                gameObject.GetComponent<Transform>().position = ogPosition;
                transform.localScale = new Vector3(2.0f, 2.0f, 0.0f);
                justStarted = true;
                attackSpawned = false;
            }
            if (justStarted)
            {
                if (transform.localScale.x < 4.0f && transform.localScale.y < 4.0f)
                {
                    transform.localScale += new Vector3(0.01f, 0.01f, 0.0f);
                }
                StartCoroutine(SpawnCoroutine());
            }
            else { MoveForward(); }
        }
        
    }

    IEnumerator SpawnCoroutine()
    {
        yield return new WaitForSeconds(2.0f);
        justStarted = false;
    }



    public void ActivateAttack() { gameObject.SetActive(true); }

    public void StartAttack()
    {
        attackSpawned = true;
        attackStarted = true;
        justStarted = true;
    }

    private void MoveForward()
    {
        movement = new Vector3(transform.position.x + (movementSpeed * -1),
                                transform.position.y, transform.position.z);
        transform.position = movement;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("DAMAGED TAKEN!!! - FireSpin Attack made contact with player.");
        if (other.tag == "Player")
        {
            if (playerCharacter != null)
            {
                float playerHealth = 0.0f;
                playerHealth = playerCharacter.GetComponent<PlayerController>().GetCurrentHealth();
                if (playerHealth > 0)
                {
                    playerHealth = playerHealth - damageValue;
                    playerCharacter.GetComponent<PlayerController>().SetCurrentHealth(playerHealth);
                    //not necessary: playerCharacter.GetComponent<PlayerController>().AdjustHealthBar();
                    playerCharacter.GetComponent<PlayerController>().DamageTaken(true);
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("EXITED THE INVISIBLE WALL!!!");
        if (other.tag == "Wall")
        {
            gameObject.SetActive(false);
        }
    }


}
