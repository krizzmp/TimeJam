using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerMovement : MonoBehaviour
{

    private float speed = 2f;
    private float jumpSpeed = 4f;

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


        rb2d.velocity = movement;
    }

    private void Jump()
    {
       
        Vector2 jumpMovement = new Vector2(rb2d.velocity.x, jumpSpeed);

        rb2d.velocity = jumpMovement;
    }


}