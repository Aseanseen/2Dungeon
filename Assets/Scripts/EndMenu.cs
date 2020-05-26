using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
	// static because don't want to reference this var
    public static bool isPaused = false;

    public GameObject endMenuUI;

    public void LoadMenu(){
    	Time.timeScale = 1f;
    	SceneManager.LoadScene("StartMenu");
    }
    public void Retry(){
//    	SceneManager.LoadScene("Level1");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
	public void QuitGame(){
    	Application.Quit();
    }
}
