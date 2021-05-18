using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float speed = 2.0f;
    [SerializeField] int XvelocityMultiplier = -1;
    [SerializeField] bool isFacingRight = false;
    [SerializeField] bool resetX;

    // Start is called before the first frame update
    void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(XvelocityMultiplier * speed, rb.velocity.y);
        if (resetX == true && (rb.transform.position.x < -3.65f || rb.transform.position.x > 3.67f)){
            Debug.Log("Flipping");
            Flip();
            XvelocityMultiplier *= -1;
            resetX = false;
        }

        if (rb.transform.position.x > 0.0f && rb.transform.position.x < 1.0f) {
            resetX = true;
        }
    }

    void Flip(){
        transform.Rotate(0f, 180f, 0f);
        isFacingRight = !isFacingRight;
    }
}
