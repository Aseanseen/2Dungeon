﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerUD : MonoBehaviour
{

	float maxDist = 0.3f;
	bool isDead = false;

	public BoxCollider2D boxCollider;
	public LayerMask sidesMask;
	public LayerMask playerMask;
	
	RaycastHit2D upRay;
	RaycastHit2D downRay;
	RaycastHit2D deadRay;
	float range = 300f;

	Vector2 target;

	public Transform upRange;
	public Transform downRange;

	void Start(){
		target = upRange.position;
	}

    // Update is called once per frame
    void Update()
    {
    	// Casts a box, with the size of the collider, at 0 degree angle, downwards, distance, mask to detect
//        walkRay = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f,Vector2.up, walkTrigger, playerMask);
    	if (isDead){
			deadRay = Physics2D.CircleCast(transform.position, Mathf.Infinity, Vector2.up, range, playerMask);
    	}
    	else{
    		// Create up and down rays
	    	upRay = Physics2D.Raycast(boxCollider.bounds.center, Vector2.up, range, sidesMask);
	    	downRay = Physics2D.Raycast(boxCollider.bounds.center, Vector2.down, range, sidesMask);

	        Debug.DrawRay(boxCollider.bounds.center, Vector2.up * (boxCollider.bounds.extents.x + range),Color.green);
	        Debug.DrawRay(boxCollider.bounds.center, Vector2.down * (boxCollider.bounds.extents.x + range),Color.red);
    	}
    }
    void FixedUpdate(){
		if (!isDead){
			if (upRay.collider != null && upRay.collider.gameObject.layer == LayerMask.NameToLayer("Player")){
				transform.position = Vector2.MoveTowards(transform.position, upRay.collider.transform.position, maxDist);
			}
			else if (downRay.collider != null && downRay.collider.gameObject.layer == LayerMask.NameToLayer("Player")){
				transform.position = Vector2.MoveTowards(transform.position, downRay.collider.transform.position, maxDist);
			}
			else{
				transform.position = Vector2.MoveTowards(transform.position, target, maxDist);
			}
		}
        if(isDead && deadRay.collider != null){
        	transform.position = Vector2.MoveTowards(transform.position, deadRay.collider.transform.position, maxDist);
        }
    }

   	void OnCollisionEnter2D(Collision2D collision){
   		if (collision.gameObject.layer != LayerMask.NameToLayer("Player")){
   			Debug.Log(Mathf.Abs(target.y - upRange.position.y));
   			Debug.Log(Mathf.Abs(target.y - downRange.position.y));
   			if (Mathf.Abs(target.y - upRange.position.y) < 1f){
   				target = downRange.position;
   				Debug.Log("Change direction");
   			}
   			else if (Mathf.Abs(target.y - downRange.position.y) < 1f){
   				target = upRange.position;
   				Debug.Log("Change direction");
   			}
   		}
   		else{
   			isDead = true;
   			maxDist = 0.1f;
   			GetComponent<Collider2D>().enabled = false;
   		}
   	}

/*

        if (walkRay.collider != null && walkRay.collider.gameObject.name == "Player"){
        	move();
        }
        if (boxCollider.enabled == false){
        	isDead = true;
        }
        if (walkRay.collider == null || walkRay.collider.gameObject.name != "Player"){
        	anim.SetBool("Move",false);
        }
        if(isDead && deadRay.collider != null){
        	transform.position = Vector2.MoveTowards(transform.position, deadRay.collider.transform.position, maxDist);
        }
*/
/*
    void move(){
    	transform.position = Vector2.MoveTowards(transform.position, walkRay.collider.transform.position, maxDist);
    	anim.SetBool("Move",true);
    }

    void idleMove(){
    	transform.position = Vector2.MoveTowards(transform.position,target, maxDist);
    }

    void die(){
    	isDead = true;
    }
*/
}
