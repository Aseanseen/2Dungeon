using System.Collections;
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
	RaycastHit2D deadRay;
	float range = 300f;
  float knockForce = 5f;

	Vector2 target;
  Vector2 oldLPosition;
  Vector2 oldRPosition;

	public Transform leftRange;
	public Transform rightRange;

	void Start(){
		target = leftRange.position;
	}

    // Update is called once per frame
    void Update()
    {
    	// Casts a box, with the size of the collider, at 0 degree angle, downwards, distance, mask to detect
//        walkRay = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f,Vector2.up, walkTrigger, playerMask);
    	isDead = enemyHealth.isDead;
      if (isDead){
			 deadRay = Physics2D.CircleCast(transform.position, Mathf.Infinity, Vector2.down, range, playerMask);
    	 maxDist = 0.1f;
        GetComponent<Collider2D>().enabled = false;
      }
    	else{
    		// Create left and right rays
	    	leftRay = Physics2D.Raycast(boxCollider.bounds.center, Vector2.left, range, sidesMask);
	    	rightRay = Physics2D.Raycast(boxCollider.bounds.center, Vector2.right, range, sidesMask);

	        Debug.DrawRay(boxCollider.bounds.center, Vector2.left * (boxCollider.bounds.extents.x + range),Color.green);
	        Debug.DrawRay(boxCollider.bounds.center, Vector2.right * (boxCollider.bounds.extents.x + range),Color.red);
    	}
    }
    void FixedUpdate(){
  		if (!isDead){
  			if (leftRay.collider != null && leftRay.collider.gameObject.layer == LayerMask.NameToLayer("Player")){
          oldLPosition = transform.position;
          oldLPosition.x = Mathf.MoveTowards(transform.position.x, leftRay.collider.transform.position.x, maxDist);
          transform.position = oldLPosition;
//  				transform.position = Vector2.MoveTowards(transform.position, leftRay.collider.transform.position, maxDist);
  			}
  			else if (rightRay.collider != null && rightRay.collider.gameObject.layer == LayerMask.NameToLayer("Player")){
          oldRPosition = transform.position;
          oldRPosition.x = Mathf.MoveTowards(transform.position.x, rightRay.collider.transform.position.x, maxDist);
          transform.position = oldRPosition;
//  				transform.position = Vector2.MoveTowards(transform.position, rightRay.collider.transform.position, maxDist);
  			}
  			else{
  				transform.position = Vector2.MoveTowards(transform.position, target, maxDist);
  			}
  		}
      if(isDead){
        transform.position = Vector2.MoveTowards(transform.position, deadRay.collider.transform.position, maxDist);
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
