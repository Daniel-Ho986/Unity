using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CadetController : MonoBehaviour
{
	[SerializeField] float movement;
	[SerializeField] Rigidbody2D rigid;
	[SerializeField] float speed = 2f;
	[SerializeField] bool isFacingRight = true;
	[SerializeField] float jumpForce = 250f;
	[SerializeField] LayerMask whatIsGround;
	[SerializeField] float groundDistance = 0.3f;
	[SerializeField] bool grounded;
	
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
		if(Input.GetButtonDown("Jump"))
		{
			Jump();
		}
		
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
	
	void Jump()
	{
		if (grounded)
		{
			rigid.velocity = new Vector2 (rigid.velocity.x, 0);
			rigid.AddForce(new Vector2(0, jumpForce));
			grounded = false;
		}
		
	}

	//make sure u replace "floor" with your gameobject name.on which player is standing
	void OnCollisionEnter2D(Collision2D collider)
	{
		if (collider.gameObject.tag == "Ground")
		{
			grounded = true;
		}
	}

	//consider when character is jumping .. it will exit collision.
	void OnCollisionExit2D(Collision2D collider)
	{
		if (collider.gameObject.tag == "Ground")
		{
			grounded = false;
		}
	}

	/*	bool isOnGround()
		{
			Vector2 position = transform.position;
			Vector2 direction = Vector2.down;

			Debug.DrawRay(position, direction, Color.green);

			RaycastHit2D hit = Physics2D.Raycast(position, direction, groundDistance, whatIsGround);
			if (hit.collider == null)
			{
				grounded = false;			
			}
			else
			{
				grounded = true;
			}
			return grounded;
		}
	*/
}
