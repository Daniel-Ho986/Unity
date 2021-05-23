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

	[SerializeField] public bool doublejump = false;
	[SerializeField] int jumpcount = 1;

	private Rigidbody2D rb;
	[SerializeField] public bool dash = false;
	[SerializeField] float dashSpeed;
	[SerializeField] float dashTime;
	[SerializeField] float startDashTime;
	[SerializeField] int direction;
	[SerializeField] bool canDash = true;
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
		if(PersistentData.Instance.GetCurrentHealth() <= 0){
			PersistentData.Instance.Die();
		}

        movement = Input.GetAxis("Horizontal");
		if(Input.GetButtonDown("Jump") && jumpcount > 0)
		{
			Jump();
		}


		rigid.velocity = new Vector2(movement * speed, rigid.velocity.y);
		if (movement < 0 && isFacingRight || movement > 0 && !isFacingRight)
		{
			Flip();
		}





		if (dash)
        {
			if (direction == 0)
			{
				if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
				{
					if (!isFacingRight)
					{
						direction = 1;
					}
					else if (isFacingRight)
					{
						direction = 2;
					}
					canDash = false;
				}
			}
			else
			{
				if (dashTime <= 0)
				{
					direction = 0;
					dashTime = startDashTime;
					rigid.velocity = Vector2.zero;
				}
				else
				{
					dashTime -= Time.deltaTime;

					if (direction == 1)
					{
						rigid.velocity = Vector2.left * dashSpeed;
					}
					else if (direction == 2)
					{
						rigid.velocity = Vector2.right * dashSpeed;
					}
				}
			}
		}
	}
	
	//called potentially multiple times per frame, best for physics for smooth behavior
	void FixedUpdate()
	{
		if (grounded && doublejump)
		{
			jumpcount = 2;
		}
		else if (grounded)
		{
			jumpcount = 1;
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
		rigid.velocity = new Vector2 (rigid.velocity.x, 0);
		rigid.AddForce(new Vector2(0, jumpForce));
		grounded = false;
		jumpcount -= 1;
		
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

	void OnCollisionStay2D(Collision2D collider)
    {
		if (collider.gameObject.tag == "Ground")
		{
			canDash = true;
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
