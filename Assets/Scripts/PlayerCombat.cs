using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
	public Animator anim;

	public Transform attackPoint;
	float attackRange = 3f;
	public LayerMask enemyLayers;
    float knockForce = 5f;

	int attackDamage = 20;

    // Update is called once per frame
    void Update()
    {
    	if (Input.GetButtonDown("Jump")){
    		Attack();
    	}
    }
    void Attack(){
    	anim.SetTrigger("Attack");
    	// Play hurt sound
        FindObjectOfType<AudioManager>().Play("Slash");

    	// Creates a circle at the attack point and see if it overlaps with enemy layer
    	// Gathers a list of enemies that we hit
    	Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

    	// Damage the enemy that is in the array
    	foreach(Collider2D enemy in hitEnemies){
    		enemy.GetComponent<EnemyHealth>().takeDamage(attackDamage);
            // Knockback effect after hit
            Vector3 direction = (enemy.gameObject.transform.position - transform.position).normalized;
            enemy.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * knockForce, ForceMode2D.Impulse);
//            Vector2 diff = (enemy.gameObject.transform.position - transform.position);
//            enemy.gameObject.transform.position = new Vector2(enemy.gameObject.transform.position.x + diff.x + knockOffset, enemy.gameObject.transform.position.y + diff.y + knockOffset);
//    		Debug.Log("Enemy hit");
    	}
    }
    // Just draws the circle for attack range
    void OnDrawGizmosSelected(){
    	if (attackPoint == null){
    		return;
    	}
    	Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
