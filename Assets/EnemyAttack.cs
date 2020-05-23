using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
	float walkTrigger = 20f;
	float attackTrigger = 9f;
	int attackDamage = 1;
	float maxDist = 0.1f;
	bool isDead = false;

	public Transform hitCircle;
	public Animator anim;
	public BoxCollider2D boxCollider;
	public LayerMask playerMask;
    public EnemyHealth enemyHealth;

	float attackOffsetUD = 4.3f;
	float attackRange = 1.7f;
	float knockForce = 10f;

	RaycastHit2D walkRay;
	RaycastHit2D attackRay;
	RaycastHit2D deadRay;

    // Start is called before the first frame update
    void Start(){
    	hitCircle.position = transform.position + new Vector3(0,attackOffsetUD,0);
    }

    // Update is called once per frame
    void Update()
    {
    	hitCircle.position = transform.position + new Vector3(0,attackOffsetUD,0);
    	// Casts a box, with the size of the collider, at 0 degree angle, downwards, distance, mask to detect
//        walkRay = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f,Vector2.up, walkTrigger, playerMask);
        isDead = enemyHealth.isDead;
    	if (isDead){
			deadRay = Physics2D.CircleCast(transform.position, Mathf.Infinity, Vector2.up, Mathf.Infinity, playerMask);
    	}
    	else{
	    	walkRay = Physics2D.Raycast(boxCollider.bounds.center, Vector2.up, walkTrigger, playerMask);
	        attackRay = Physics2D.Raycast(boxCollider.bounds.center, Vector2.up, attackTrigger, playerMask);
	        
	        Debug.DrawRay(boxCollider.bounds.center, Vector2.up * (boxCollider.bounds.extents.y + walkTrigger),Color.green);
	        Debug.DrawRay(boxCollider.bounds.center, Vector2.up * (boxCollider.bounds.extents.y + attackTrigger),Color.red);
    	}
    }
    void FixedUpdate(){
        if (walkRay.collider != null && walkRay.collider.gameObject.layer == LayerMask.NameToLayer("Player")){
        	move();
        }
        if (!isDead && attackRay.collider != null && attackRay.collider.gameObject.layer == LayerMask.NameToLayer("Player")){
        	attack();
        }
        if (walkRay.collider == null || walkRay.collider.gameObject.layer != LayerMask.NameToLayer("Player")){
        	anim.SetBool("Move",false);
        }
        if(isDead){
        	transform.position = Vector2.MoveTowards(transform.position, deadRay.collider.transform.position, maxDist);
        }
    }

    void move(){
    	transform.position = Vector2.MoveTowards(transform.position, walkRay.collider.transform.position, maxDist);
    	anim.SetBool("Move",true);
    }

    void attack(){
    	anim.SetTrigger("Attack");

    	// Creates a circle at the attack point and see if it overlaps with enemy layer
    	// Finds the enemy that we hit
    	Collider2D Player = Physics2D.OverlapCircle(hitCircle.position, attackRange, playerMask);
    	if (Player != null){
        	// Knockback effect after hit
			Vector3 direction = (Player.gameObject.transform.position - transform.position).normalized;
			Player.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * knockForce, ForceMode2D.Impulse);
			Player.GetComponent<PlayerMovement>().hurt(attackDamage);
		}
    }

    // Just draws the circle for attack range
    void OnDrawGizmosSelected(){
    	if (hitCircle == null){
    		return;
    	}
    	Gizmos.DrawWireSphere(hitCircle.position, attackRange);
    }
}
