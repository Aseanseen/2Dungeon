﻿using System.Collections;
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

    public SpriteRenderer body;
    public Color hurtColor;
    AudioManager audioManager;

    BloodPool bloodPool;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.setMaxHealth(maxHealth);
        audioManager = FindObjectOfType<AudioManager>();
        bloodPool = BloodPool.Instance;
    }

    public void takeDamage(int damage){
    	currentHealth -= damage;
        // Flash effect upon hit
        StartCoroutine(Flash());
        // Blood effect upon hit
        bloodPool.SpawnFromPool("Blood",transform.position, Quaternion.identity);
//        Instantiate(bloodEffect, transform.position, Quaternion.identity);
        // Blood stain upon hit
        bloodPool.SpawnFromPool("FadeRed",transform.position, Quaternion.identity);
//        Instantiate(bloodSplash, transform.position, Quaternion.identity);
        // Play hurt sound
        audioManager.Play("PlayerHurt");
        if (currentHealth <= 0){
            playerMovement.endGame();
        }
    	healthBar.setHealth(currentHealth);
    }

    // Handles flash effect
    IEnumerator Flash(){
        body.color = hurtColor;
        yield return new WaitForSeconds(0.05f);
        body.color = Color.white;
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
