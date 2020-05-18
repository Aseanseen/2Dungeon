using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 15f;
    public Rigidbody2D rb;
    public Animator anim;
    Vector2 movement;

    // Update is called once per frame
    // Better to handle input here
    void Update()
    {
    	movement.x = Input.GetAxisRaw("Horizontal");
    	movement.y = Input.GetAxisRaw("Vertical");

    	anim.SetFloat("Horizontal", movement.x);
    	anim.SetFloat("Vertical", movement.y);
    	anim.SetFloat("Speed", movement.sqrMagnitude);
    }
    // Better not to put physics stuff into update, put into fixedupdate
    // Better to handle movement
    void FixedUpdate()
    {
    	move();
    }
    void move(){
    	rb.velocity = new Vector2(movement.x,movement.y) * moveSpeed;
//    	rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
}
