using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverseBlockPlayer : MonoBehaviour
{
	public GameObject item;
	Collider2D selfCollider;

	void Start()
    {
        selfCollider = GetComponent<Collider2D>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (item == null){
        	selfCollider.enabled = false;
        }
        else{
        	selfCollider.enabled = true;
        }
    }
}