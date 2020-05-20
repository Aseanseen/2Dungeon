using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
	public Animator anim;

	public Transform attackPoint;
	float attackRange = 3f;
	public LayerMask enemyLayers;

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

    	// Creates a circle at the attack point and see if it overlaps with enemy layer
    	// Gathers a list of enemies that we hit
    	Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

    	// Damage the enemy that is in the array
    	foreach(Collider2D Enemy in hitEnemies){
    		Enemy.GetComponent<EnemyHealth>().takeDamage(attackDamage);
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
