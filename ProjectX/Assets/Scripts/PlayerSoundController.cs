using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSoundController : MonoBehaviour {
    public AudioSource eating;
    public AudioSource jumping;
    public AudioSource running;
    public AudioSource dying;
    public AudioSource farting;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Eat()
    {
        eating.Play();
    }

    public void Jump()
    {
        jumping.Play();
    }

    public void Die()
    {
        dying.Play();
    }

    public void Running(bool start)
    {
        if (start)
        {
            running.Play();
        }
        else
        {
            running.Stop();
        }
    }

    public void Fart()
    {
        farting.Play();
    }

}
