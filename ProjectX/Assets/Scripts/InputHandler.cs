using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class InputHandler : MonoBehaviour {

    private Event current = new Event();
    private KeyCode currentKey;
    private List<KeyCode> keysDown = new List<KeyCode>();
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {
        current = new Event();
        Event.PopEvent(current);
        currentKey = ReadKeyCode();

        if (currentKey == KeyCode.Escape)
        {
            Time.timeScale = 0;
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Additive);
        }
    }

    protected KeyCode ReadKeyCode()
    {
        if (current.type == EventType.KeyDown && !keysDown.Contains(current.keyCode))
        {
            keysDown.Add(current.keyCode);
            return current.keyCode;
        }
        else if (current.type == EventType.KeyUp)
        {
            keysDown.Remove(current.keyCode);
        }
        return KeyCode.None;
    }
}
