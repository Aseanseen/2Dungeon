using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	// static because don't want to reference this var
    public static bool isPaused = false;

    public GameObject pauseMenuUI;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
        	if(isPaused){
        		Resume();
        	}
        	else{
        		Pause();
        	}
		}
    }
    public void Resume(){
    	// Sets the menu to be inactive
    	pauseMenuUI.SetActive(false);
    	// Set time
    	Time.timeScale = 1f;
    	isPaused = false;
    }
    void Pause(){
    	// Sets the menu to be active
    	pauseMenuUI.SetActive(true);
    	// Freeze time
    	Time.timeScale = 0f;
    	isPaused = true;
    }
    public void LoadMenu(){
    	Time.timeScale = 1f;
    	SceneManager.LoadScene("StartMenu");
    }
    public void Retry(){
        SceneManager.LoadScene (SceneManager.GetActiveScene().buildIndex);
    }
	public void QuitGame(){
    	Application.Quit();
    }
}
