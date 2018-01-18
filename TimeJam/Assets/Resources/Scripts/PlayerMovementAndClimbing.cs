using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovementAndClimbing : MonoBehaviour
{
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
        var layerHandler = FindObjectOfType<LayerHandler>();
        layerHandler.LayerChanged += OnLayerChanged;
    }

    private void OnLayerChanged(Layer layer)
    {
        if (layer == Layer.Past)
        {
            this.gameObject.layer = 10;
        }
        else if (layer == Layer.Present)
        {
            this.gameObject.layer = 11;
        }
    }

    private void Update()
    {
        if (canClimb)
        {
            if (Mathf.RoundToInt(Input.GetAxis("Vertical")) == 1)
            {
                EnableClimbing();
            }
        }
        if (isClimbing)
        {
            climbDirection = Mathf.RoundToInt(Input.GetAxis("Vertical"));
        }
        else
        {
            moveDirection = Input.GetAxis("Horizontal");
        }

        if (Math.Abs(rb2d.velocity.y) < 0.01)
        {
            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
        }
    }

    void FixedUpdate()
    {
        float velocityY = 0;

        if (isClimbing)
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
        if (collider.CompareTag("Ladder"))
        {
            canClimb = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Ladder"))
        {
            canClimb = false;

            if (isClimbing)
            {
                Jump();
            }
            else
            {
                DisableClimbing();
            }
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

        if (dir >= 0.01)
        {
            sr.flipX = false;
        }
        else if (dir <= -0.01)
        {
            sr.flipX = true;
        }
    }
}