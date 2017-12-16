using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{
<<<<<<< HEAD
	public float speed = 2f;
	public float jumpSpeed = 4f;

	private Rigidbody2D rb2d;
	private bool isClimbing;
	private bool canClimb;
	private float climbDirection;
	private float moveDirection;

	void Start()
	{
		rb2d = GetComponent<Rigidbody2D>();
		isClimbing = false;
		canClimb = false;
		climbDirection = 0;
		moveDirection = 0;
	}

	private void Update()
	{
		if(canClimb)
		{
			if(Input.GetKeyDown(KeyCode.W))
			{
				EnableClimbing();
			}
		}

		if(isClimbing)
		{
			if(Input.GetKey(KeyCode.W))
			{
				climbDirection = 1;
			}
			else if(Input.GetKey(KeyCode.S))
			{
				climbDirection = -1;
			}
			else
			{
				climbDirection = 0;
			}
		}
		else
		{
			moveDirection = Input.GetAxis("Horizontal");
		}

		if(rb2d.velocity.y == 0)
		{
			if(Input.GetKeyDown(KeyCode.Space))
			{
				Jump();
			}
		}
	}

	void FixedUpdate()
	{
		float velocityY = 0;

		if(isClimbing)
		{
			velocityY = climbDirection * speed;
		}
		else
		{
			velocityY = rb2d.velocity.y;
		}

		Vector2 movement = new Vector2(moveDirection * speed, velocityY);
		rb2d.velocity = movement;

		Animator anim = GetComponent<Animator>();
		anim.SetFloat("moveSpeed", Mathf.Abs(moveDirection));
		FlipSPrite(moveDirection);
	}

	private void Jump()
	{
		DisableClimbing();
		Vector2 jumpMovement = new Vector2(rb2d.velocity.x, jumpSpeed);
		rb2d.velocity = jumpMovement;
		GetComponent<AudioSource>().Play();
	}

	private void OnTriggerEnter2D(Collider2D collider)
	{
		if(collider.tag == "Ladder")
		{
			canClimb = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collider)
	{
		if(collider.tag == "Ladder")
		{
			canClimb = false;
			DisableClimbing();
		}
	}

	private void EnableClimbing()
	{
		moveDirection = 0;
		isClimbing = true;
		rb2d.isKinematic = true;
	}

	private void DisableClimbing()
	{
		isClimbing = false;
		rb2d.isKinematic = false;
	}

	private void FlipSPrite(float dir)
	{
		SpriteRenderer sr = GetComponent<SpriteRenderer>();

		if(dir >= 0.01)
		{
			sr.flipX = false;
		}
		else if(dir <= -0.01)
		{
			sr.flipX = true;
		}
	}
=======

    public float speed = 2f;
    public float jumpSpeed = 4f;

    private Rigidbody2D rb2d;


    void Start()
    {
        
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
 
        float moveHorizontal = Input.GetAxis("Horizontal");

     
        Vector2 movement = new Vector2(moveHorizontal * speed, rb2d.velocity.y);

        Animator anim = GetComponent<Animator>();
        anim.SetFloat("moveSpeed", Mathf.Abs(moveHorizontal));


        rb2d.velocity = movement;

        FlipSPrite(moveHorizontal);
    }

    private void Jump()
    {
       
        Vector2 jumpMovement = new Vector2(rb2d.velocity.x, jumpSpeed);

        rb2d.velocity = jumpMovement;

        GetComponent<AudioSource>().Play();

    }

    private void FlipSPrite(float dir)
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        if (dir >= 0.01)
        {
            sr.flipX = false;
        }
        else if (dir <= -0.01)
        {
            sr.flipX = true;
        }
    }


>>>>>>> origin/master
}