using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour {

    public DeathScreenController canvas;
    public AudioSource winAudio;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        winAudio.Play();
        canvas.Won();
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}
