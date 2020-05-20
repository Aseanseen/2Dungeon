using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerR : MonoBehaviour
{
	public PolygonCollider2D polyCollider;
	public LayerMask sidesMask;
	
	RaycastHit2D rightRay;
	Rigidbody2D rb;

	float range = 30f;
	float moveSpeed = 15f;
	bool isDead = false;

	void Start(){
		rb = gameObject.GetComponent<Rigidbody2D>();
	}
    // Update is called once per frame
    void Update()
    {
    	// Casts a box, with the size of the collider, at 0 degree angle, downwards, distance, mask to detect
//        walkRay = Physics2D.BoxCast(polyCollider.bounds.center, polyCollider.bounds.size, 0f,Vector2.up, walkTrigger, playerMask);
    	if (!isDead){
			// Create right rays
	    	rightRay = Physics2D.Raycast(polyCollider.bounds.center, Vector2.right, range, sidesMask);
	        Debug.DrawRay(polyCollider.bounds.center, Vector2.right * (polyCollider.bounds.extents.x + range),Color.red);
		}
    }

    void FixedUpdate(){
		if (!isDead){
			if (rightRay.collider != null && rightRay.collider.gameObject.layer == LayerMask.NameToLayer("Player")){
				rb.velocity = new Vector2(1,0) * moveSpeed;
			}
		}
    }

   	void OnCollisionEnter2D(Collision2D collision){
   		GetComponent<Collider2D>().enabled = false;
   		rb.bodyType = RigidbodyType2D.Static;
   		isDead = true;
   	}
}
