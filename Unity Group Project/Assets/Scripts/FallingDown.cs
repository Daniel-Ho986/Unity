using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingDown : MonoBehaviour
{
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.tag == "Player"){
            PlatformManager.Instance.StartCoroutine("SpawnPlatform", 
                new Vector2 (transform.position.x, transform.position.y));
            Invoke("DropPlatform", 1f);
            Destroy(gameObject, 2.3f);
        }
    }

    void DropPlatform(){
        rb.isKinematic = false;
    }
}
