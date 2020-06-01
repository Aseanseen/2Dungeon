using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerUD : MonoBehaviour
{

	float maxDist = 0.2f;
	bool isDead = false;

	public BoxCollider2D boxCollider;
	public LayerMask sidesMask;
	public LayerMask playerMask;
  	public PlayerMovement playerMovement;
  	public EnemyHealth enemyHealth;
	
	RaycastHit2D upRay;
	RaycastHit2D downRay;
	RaycastHit2D deadRay;
	float range = 300f;
  float knockForce = 5f;

	Vector2 target;
  	Vector2 oldUPosition;
  	Vector2 oldDPosition;

	public Transform upRange;
	public Transform downRange;
  Collider2D selfCollider;

	void Start(){
		target = upRange.position;
    selfCollider = GetComponent<Collider2D>();
	}

    // Update is called once per frame
    void Update()
    {
    	isDead = enemyHealth.isDead;
    	if (isDead){
			deadRay = Physics2D.CircleCast(transform.position, Mathf.Infinity, Vector2.up, range, playerMask);
    		maxDist = 0.1f;
        	selfCollider.enabled = false;
    	}
    	else{
    		// Create up and down rays
	    	upRay = Physics2D.Raycast(boxCollider.bounds.center, Vector2.up, range, sidesMask);
	    	downRay = Physics2D.Raycast(boxCollider.bounds.center, Vector2.down, range, sidesMask);

//	        Debug.DrawRay(boxCollider.bounds.center, Vector2.up * (boxCollider.bounds.extents.x + range),Color.green);
//	        Debug.DrawRay(boxCollider.bounds.center, Vector2.down * (boxCollider.bounds.extents.x + range),Color.red);
    	}
    }
    void FixedUpdate(){
  		if (!isDead){
  			if (upRay.collider != null && upRay.collider.gameObject.layer == LayerMask.NameToLayer("Player")){
          oldUPosition = transform.position;
          oldUPosition.y = Mathf.MoveTowards(transform.position.y, upRay.collider.transform.position.y, maxDist);
          transform.position = oldUPosition;
  			}
  			else if (downRay.collider != null && downRay.collider.gameObject.layer == LayerMask.NameToLayer("Player")){
          oldDPosition = transform.position;
          oldDPosition.y = Mathf.MoveTowards(transform.position.y, downRay.collider.transform.position.y, maxDist);
          transform.position = oldDPosition;
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
//   			Debug.Log(Mathf.Abs(target.x - upRange.position.x));
//   			Debug.Log(Mathf.Abs(target.x - downRange.position.x));
   			if (Mathf.Abs(target.y - upRange.position.y) < 1f){
   				target = downRange.position;
//   				Debug.Log("Change direction");
   			}
   			else if (Mathf.Abs(target.y - downRange.position.y) < 1f){
   				target = upRange.position;
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
