using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MonoBehaviour
{
//	public PlayerMovement playerMovement;
	float triggerHeight = 10f;
	public Rigidbody2D rb;
	public BoxCollider2D boxCollider;
	public LayerMask playerMask;

    // Start is called before the first frame update
    void Start()
    {
    	// Gravity off
    	rb.gravityScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
//		Vector2 triggerPosition = transform.position + new Vector2(0, -25);
    	// Casts a box, with the size of the collider, at 0 degree angle, downwards, distance, mask to detect
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f,Vector2.down, triggerHeight, playerMask);
        if (hit.collider != null && hit.collider.gameObject.name == "Player"){
        	drop();
        }
    }

    void drop(){
    	// Gravity on
    	rb.gravityScale = 9.81f;
    }
    void OnCollisionEnter2D(Collision2D collision){
    	if (collision.gameObject.name != "Player"){
    		boxCollider.enabled = false;
    		rb.isKinematic = true;
    	}
    }
}