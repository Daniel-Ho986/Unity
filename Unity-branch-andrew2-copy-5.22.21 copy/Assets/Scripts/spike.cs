using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spike : MonoBehaviour
{
    private GameObject playerObj = null;
    [SerializeField] private Vector2 initialpoint;
    // Start is called before the first frame update
    void Start()
    {
        if (playerObj == null)
            playerObj = GameObject.FindGameObjectWithTag("Player");

        initialpoint = new Vector2(playerObj.transform.position.x, playerObj.transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerObj.transform.position = initialpoint;
            PersistentData.Instance.SetCurrentHealth(PersistentData.Instance.GetCurrentHealth() - 1);
        }
    }
}
