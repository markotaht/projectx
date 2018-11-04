using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour {

    public DeathScreenController canvas;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        canvas.Won();
        this.gameObject.SetActive(false);
    }
}
