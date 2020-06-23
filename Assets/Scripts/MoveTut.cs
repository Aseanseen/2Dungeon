using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTut : MonoBehaviour
{
	public PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement.enabled = true;
    }
}
