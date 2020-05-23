using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StainAway : MonoBehaviour
{
	SpriteRenderer toFade;
    
    void Update(){
    	toFade = gameObject.GetComponent<SpriteRenderer>();
    	StartCoroutine(fadeOut(toFade, 5f));
    }
    // Fadeout coroutine, function that is capable of waiting and timing its process, as well as pausing it entirely
	IEnumerator fadeOut(SpriteRenderer render, float duration)
	{
	    float counter = 0;
	    // Get current color
	    Color spriteColor = render.material.color;

	    while (counter < duration)
	    {
	        counter += Time.deltaTime;
	        // Fade from 1 to 0 by counter/duration
	        float alpha = Mathf.Lerp(1, 0, counter / duration);
	        // Change alpha
	        render.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, alpha);
	        // Wait for a frame
	        yield return null;
    	}
    	Destroy(gameObject);
	}
}
