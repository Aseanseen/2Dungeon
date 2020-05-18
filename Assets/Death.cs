using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Death : MonoBehaviour
{
	/* // To pass variables between scripts in unity
	private FuncName func
	void Awake(){
		func = GameObject.FindObjectOfType<FuncName>();
	}
	func.UpdateScore(score);
	*/
	void OnCollisionEnter2D(Collision2D collision){
		Destroy(collision.gameObject);
	}
}
