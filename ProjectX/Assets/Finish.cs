using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour {

    public DeathScreenController canvas;
    public AudioSource winAudio;
    public CharacterControllerRb cc;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        winAudio.Play();
        canvas.Won();
        cc.Win();
        this.gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }
}
