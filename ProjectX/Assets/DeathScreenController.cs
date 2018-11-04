using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreenController : MonoBehaviour {

    public GameObject deathPanel;
    public GameObject winPanel;
    private bool dead = false;
    private bool win = false;
    private float endTime;
    private float duration = 2f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(dead && Time.time <= endTime)
        {
            Color tmp = deathPanel.GetComponent<Image>().color;
            tmp.a = 1 - (endTime - Time.time) / (duration + 2f);
            deathPanel.GetComponent<Image>().color = tmp;
        }
        else if (win && Time.time <= endTime)
        {
            Color tmp = winPanel.GetComponent<Image>().color;
            tmp.a = 1 - (endTime - Time.time) / duration;
            winPanel.GetComponent<Image>().color = tmp;
        }
    }

    public void Dead()
    {
        dead = true;
        deathPanel.SetActive(true);
        endTime = Time.time + duration + 2f;
    }

    public void Won()
    {
        win = true;
        winPanel.SetActive(true);
        endTime = Time.time + duration;
    }
}
