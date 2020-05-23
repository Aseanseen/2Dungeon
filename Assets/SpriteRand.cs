using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRand : MonoBehaviour
{
	SpriteRenderer rend;
	public Sprite[] arr;

    // Start is called before the first frame update
    void Start()
    {
     rend = GetComponent<SpriteRenderer>();
     int rand = Random.Range(0,arr.Length);
     rend.sprite = arr[rand];   
    }
}
