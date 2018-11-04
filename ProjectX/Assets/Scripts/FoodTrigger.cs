using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodTrigger : MonoBehaviour {

    [SerializeField]
    private bool cheese;

    [SerializeField]
    private bool beans;

    [SerializeField]
    private bool banana;

    void OnTriggerEnter2D(Collider2D other)
    {
        CharacterControllerRb cc = other.gameObject.GetComponent<CharacterControllerRb>();
        if (cheese)
        {
            cc.EatCheese();
        }
        else if (beans)
        {
            cc.EatBeans();
        }else if (banana)
        {
            cc.EatBanana();
        }

        Destroy(transform.gameObject);
    }
}
