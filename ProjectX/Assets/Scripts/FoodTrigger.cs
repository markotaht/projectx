using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodTrigger : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<Character>().jumpModifiers.FoodEaten += 1;
        Destroy(transform.gameObject);
    }
}
