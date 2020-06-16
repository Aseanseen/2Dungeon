using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlayer : MonoBehaviour
{
	public GameObject lightCheck;
	Collider2D selfCollider;

	void Start()
    {
        selfCollider = GetComponent<Collider2D>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (lightCheck == null){
        	selfCollider.enabled = true;
        }
        else{
        	selfCollider.enabled = false;
        }
    }
}
