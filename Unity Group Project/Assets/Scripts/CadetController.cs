using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CadetController : MonoBehaviour
{
	[SerializeField] float movement;
	[SerializeField] Rigidbody2D rigid;
	[SerializeField] float speed = 5f;
	[SerializeField] bool isFacingRight = true;
    // Start is called before the first frame update
    void Start()
    {
        if (rigid == null)
		{
			rigid = GetComponent<Rigidbody2D>();
		}
    }

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxis("Horizontal");
    }
	
	//called potentially multiple times per frame, best for physics for smooth behavior
	void FixedUpdate()
	{
		rigid.velocity = new Vector2(movement*speed, rigid.velocity.y);
		if (movement < 0 && isFacingRight || movement > 0 && !isFacingRight)
		{
			Flip();
		}
	}
	
	void Flip()
	{
		Vector3 playerScale = transform.localScale;
		playerScale.x = playerScale.x * -1;
		transform.localScale = playerScale;
		
		isFacingRight = !isFacingRight;
	}
}
