using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
	public int maxHealth = 100;
	public int currentHealth;

	public HealthBar healthBar;
    public PlayerMovement playerMovement;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
    }

    public void takeDamage(int damage){
    	currentHealth -= damage;
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
}
