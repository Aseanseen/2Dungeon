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
    
    public GameObject bloodEffect;
    public GameObject bloodSplash;

    public SpriteRenderer body;
    public Color hurtColor;
    Collider2D selfCollider;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
        selfCollider = GetComponent<Collider2D>();
    }

    public void takeDamage(int damage){
        if (!isDead){
        	currentHealth -= damage;
            // Blood effect upon hit
            Instantiate(bloodEffect, transform.position, Quaternion.identity);
            // Flash effect upon hit
            StartCoroutine(Flash());
            // Updates helath bar and death animation
        	healthBar.setHealth(currentHealth);
            anim.SetTrigger("Hurt");
        
            if (currentHealth <= 0){
                // Blood stain upon death
                Instantiate(bloodSplash, transform.position, Quaternion.identity);
                die();
            }
        }
    }
    // Handles flash effect
    IEnumerator Flash(){
        body.color = hurtColor;
        yield return new WaitForSeconds(0.1f);
        body.color = Color.white;
    }
	public void die(){
        isDead = true;
        anim.SetBool("isDead", true);
		selfCollider.enabled = false;
        Destroy(transform.gameObject.GetComponent<Rigidbody2D>());
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
