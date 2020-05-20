using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
	public int maxHealth = 100;
	public int currentHealth;
    public bool isDead = false;

	public HealthBar healthBar;
    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
    }

    public void takeDamage(int damage){
    	currentHealth -= damage;
        if (currentHealth <= 0){
            isDead = true;
        	die();
        }
    	healthBar.setHealth(currentHealth);
        anim.SetTrigger("Hurt");
    	
    }

	public void die(){
        isDead = true;
        anim.SetBool("isDead", true);
		GetComponent<Collider2D>().enabled = false;
    }
    /*
    public void gainHealth(int health){
        currentHealth += health;
        if (currentHealth > maxHealth){
            currentHealth = maxHealth;
        }
        healthBar.setHealth(currentHealth);
    }
    */
}
