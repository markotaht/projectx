using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodTrigger : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        other.gameObject.GetComponent<CharacterControllerRb>().jumpModifiers.FoodEaten += 1;
        Destroy(transform.gameObject);
    }
}
