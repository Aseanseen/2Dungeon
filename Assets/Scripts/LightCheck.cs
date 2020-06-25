using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCheck : MonoBehaviour
{
	public UnityEngine.Experimental.Rendering.Universal.Light2D global;
	public UnityEngine.Experimental.Rendering.Universal.Light2D player;
	// public LayerMask playerMask;
	public PlayerMovement pM;

	// void Update(){
	// 	Collider2D hitPlayer = Physics2D.OverlapCircle(transform.position, 5f, playerMask);
	// 	if (hitPlayer != null){
	// 		global.enabled = false;
	// 		player.enabled = true;
	// 		checkPoint.SetActive(true);
	// 		Destroy(gameObject);
	// 		pM.reset();
	// 	}
	// }
	
	void OnTriggerEnter2D(Collider2D hitPlayer){
		if(hitPlayer.CompareTag("Player")){
			global.enabled = false;
			player.enabled = true;
			pM.reset();
			Destroy(gameObject);
		}
	}
}
