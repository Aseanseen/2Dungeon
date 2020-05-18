using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Death : MonoBehaviour
{
	void OnCollisionEnter2D(Collision2D collision){
		Destroy(collision.gameObject);
	}
}
