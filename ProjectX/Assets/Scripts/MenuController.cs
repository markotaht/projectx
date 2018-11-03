using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public Text pauseText;
    public AudioSource source;
    public AudioListener listener;
    public Object audio;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void StartGame()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("MainMenu"));
        //source.Stop();
        //listener.enabled = false;
        Time.timeScale = 1;
        SceneManager.LoadScene("Aimar Level");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit game");
    }

    public void ContinueGame()
    {
        SceneManager.UnloadSceneAsync("MainMenu");
        Time.timeScale = 1;
    }
}
