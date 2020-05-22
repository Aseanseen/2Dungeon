using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
	int maxHealth = 100;
	int currentHealth;

	public HealthBar healthBar;
    public PlayerMovement playerMovement;
    public GameObject bloodEffect;
    public GameObject bloodSplash;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
    }

    public void takeDamage(int damage){
    	currentHealth -= damage;
        // Blood effect
        Instantiate(bloodEffect, transform.position, Quaternion.identity);
        Instantiate(bloodSplash, transform.position, Quaternion.identity);
        if (currentHealth <= 0){
            playerMovement.endGame();
        }
    	healthBar.setHealth(currentHealth);
    }
    public void gainHealth(int health){
        currentHealth += health;
        if (currentHealth > maxHealth){
            currentHealth = maxHealth;
        }
        healthBar.setHealth(currentHealth);
    }
    public void setHealth(int health){
        healthBar.setHealth(health);
    }
}
