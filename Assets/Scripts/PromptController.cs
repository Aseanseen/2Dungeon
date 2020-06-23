﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PromptController : MonoBehaviour
{
	public PlayerMovement playerMovement;
	public PlayerCombat playerCombat;
	public Button attackButton;

	bool wait = false;
	bool attack = false;
	float lagTime = 1.5f;

	GameObject speech;

	public List<GameObject> speechList = new List<GameObject>();

	int i = 0;

	public GameObject player;
	public GameObject textBox;
	public List<GameObject> toDestroy = new List<GameObject>();
	Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement.enabled = false;
        playerCombat.enabled = false;
        attackButton.interactable = false;

    	speech = speechList[i];

    	speech.SetActive(true);

    	startPosition = player.transform.position;

    	attackButton.onClick.AddListener(Attack);
    }
    void Update()
    {
    	if (speech != null){
	    	// Check if player is going into move/wep tutorial
	    	if (speech.tag == "MoveTut" || speech.tag == "WepTut"){
	    		if (speech.tag == "MoveTut" && player.transform.position != startPosition){
	    			StartCoroutine(NextWord(speech));
	    		}
	    		if (speech.tag == "WepTut" && attack){
	    			StartCoroutine(NextWord(speech));
	    		}
	    	}
	    	// Check if player touches screen and after delay
	    	else if (Input.touchCount > 0 && speech.tag != "Final" && !wait)
	    	{
	    		StartCoroutine(NextWord(speech));
	    	}

	    	// If no more speech, destroy the speech object
	    	else if (speech.tag == "Final" && !wait)
	    	{
	    		speechList.Clear();
	    		Destroy(speech);
    		    foreach(GameObject des in toDestroy)
     			{
         			Destroy(des);
     			}
	    		textBox.SetActive(false);
	    	}
    	}

    	// For testing purposes
    	// if (Input.GetButtonDown("Jump")){
    	// 	StartCoroutine(NextWord(speech));
    	// }
    }

    // Check if player pressed attack button
    void Attack(){
    	attack = true;
    }

    // Move to the next word
    IEnumerator NextWord(GameObject thing)
    {
    	Destroy(speech);
//		speech.SetActive(false);
    	i ++;
    	speech = speechList[i];
		speech.SetActive(true);
		wait = true;
		yield return new WaitForSeconds(lagTime);
		wait = false;
    }
}
