using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WepTut : MonoBehaviour
{
	public PlayerCombat playerCombat;
	public Button attackButton;

    // Start is called before the first frame update
    void Start()
    {
        playerCombat.enabled = true;
        attackButton.interactable = true;
    }
}
