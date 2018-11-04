using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreenController : MonoBehaviour {

    public GameObject panel;
    private bool dead = false;
    private float endTime;
    private float duration = 2f;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(dead && Time.time <= endTime)
        {
            Color tmp = panel.GetComponent<Image>().color;
            tmp.a = 1 - (endTime - Time.time) / duration;
            panel.GetComponent<Image>().color = tmp;
        }
	}

    public void Dead()
    {
        dead = true;
        panel.SetActive(true);
        endTime = Time.time + duration;
    }

}
