using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Death : MonoBehaviour
{
	public PlayerMovement playerMovement;

	/* // To pass variables between scripts in unity
	private FuncName func
	void Awake(){
		func = GameObject.FindObjectOfType<FuncName>();
	}
	func.UpdateScore(score);
	*/
	void OnCollisionEnter2D(Collision2D collision){
		playerMovement.endGame();
//		Destroy(collision.gameObject);
	}
}
