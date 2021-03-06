﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerLR : MonoBehaviour
{

	float maxDist = 0.2f;
	bool isDead = false;

	public BoxCollider2D boxCollider;
	public LayerMask sidesMask;
	public LayerMask playerMask;
  public PlayerMovement playerMovement;
  public EnemyHealth enemyHealth;
	
	RaycastHit2D leftRay;
	RaycastHit2D rightRay;
	Collider2D deadRay;
	float range = 300f;
  float knockForce = 5f;

	Vector2 target;
  Vector2 oldLPosition;
  Vector2 oldRPosition;

	public Transform leftRange;
	public Transform rightRange;
  Collider2D selfCollider;

	void Start(){
		target = leftRange.position;
    selfCollider = GetComponent<Collider2D>();
	}

    // Update is called once per frame
    void Update()
    {
    	isDead = enemyHealth.isDead;
      if (isDead){
			 deadRay = Physics2D.OverlapCircle(transform.position, range, playerMask);
    	 maxDist = 0.1f;
        selfCollider.enabled = false;
      }
    	else{
    		// Create left and right rays
	    	leftRay = Physics2D.Raycast(boxCollider.bounds.center, Vector2.left, range, sidesMask);
	    	rightRay = Physics2D.Raycast(boxCollider.bounds.center, Vector2.right, range, sidesMask);

//	        Debug.DrawRay(boxCollider.bounds.center, Vector2.left * (boxCollider.bounds.extents.x + range),Color.green);
//	        Debug.DrawRay(boxCollider.bounds.center, Vector2.right * (boxCollider.bounds.extents.x + range),Color.red);
    	}
    }
    void FixedUpdate(){
  		if (!isDead){
  			if (leftRay.collider != null && leftRay.collider.gameObject.layer == LayerMask.NameToLayer("Player")){
	          oldLPosition = transform.position;
	          oldLPosition.x = Mathf.MoveTowards(transform.position.x, leftRay.collider.transform.position.x, maxDist);
	          transform.position = oldLPosition;
  			}
  			else if (rightRay.collider != null && rightRay.collider.gameObject.layer == LayerMask.NameToLayer("Player")){
	          oldRPosition = transform.position;
	          oldRPosition.x = Mathf.MoveTowards(transform.position.x, rightRay.collider.transform.position.x, maxDist);
	          transform.position = oldRPosition;
  			}
  			else{
  				transform.position = Vector2.MoveTowards(transform.position, target, maxDist);
  			}
  		}
      	if(isDead){
        	transform.position = Vector2.MoveTowards(transform.position, deadRay.transform.position, maxDist);
      	}
    }

   	void OnCollisionStay2D(Collision2D collision){
   		if (collision.gameObject.layer != LayerMask.NameToLayer("Player")){
//   			Debug.Log(Mathf.Abs(target.x - leftRange.position.x));
//   			Debug.Log(Mathf.Abs(target.x - rightRange.position.x));
   			if (Mathf.Abs(target.x - leftRange.position.x) < 1f){
   				target = rightRange.position;
//   				Debug.Log("Change direction");
   			}
   			else if (Mathf.Abs(target.x - rightRange.position.x) < 1f){
   				target = leftRange.position;
//   				Debug.Log("Change direction");
   			}
   		}
   		else{
        // Knockback effect after hit only in slippery mode
        Vector3 direction = (collision.gameObject.transform.position - transform.position).normalized;
        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * knockForce, ForceMode2D.Impulse);
        playerMovement.hurt(10);
   		}
   	}
}
