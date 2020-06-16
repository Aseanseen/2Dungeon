using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PromptController : MonoBehaviour
{
	public PlayerMovement playerMovement;
	public PlayerCombat playerCombat;
	public Button attackButton;

	bool wait = false;

//	public List<GameObject> speechList;
	public GameObject speech;

	// public GameObject words1;
	// public GameObject words2;
	// public GameObject words3;
	// public GameObject words4;

	// Creates a queue for speech
//	Queue<GameObject> speechPool = new Queue<GameObject>();
	public List<GameObject> speechList = new List<GameObject>();
	int i = 0;
//    GameObject[] speechList;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement.enabled = false;
        playerCombat.enabled = false;
        attackButton.interactable = false;

    	speech = speechList[i];
    	Debug.Log(speech);
    	speech.SetActive(true);
    }
    void Update()
    {

    	Debug.Log(speechList.Count);
    	if (Input.touchCount > 0 && speechList.Count != 0 && !wait)
    	{
    		StartCoroutine(NextWord(speech));
    	}
    	if (speechList.Count == 0 && !wait)
    	{
    		Destroy(gameObject);
    	}
    	if (Input.GetButtonDown("Jump")){
    		StartCoroutine(NextWord(speech));
    	}
    }
    IEnumerator NextWord(GameObject thing)
    {
		speech.SetActive(false);
    	i ++;
    	speech = speechList[i];
		speech.SetActive(true);
		wait = true;
		yield return new WaitForSeconds(2);
		wait = false;
    }
}
