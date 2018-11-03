using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FartTrigger : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.GetComponent<CharacterControllerRb>().FartFood();
        Destroy(transform.gameObject);
    }
}
