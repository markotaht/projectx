using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonekyAnimationEventHandler : MonoBehaviour {

    [SerializeField]
    CharacterControllerRb cc;

	void Jump()
    {
        cc.Jump();
    }

    void Landed()
    {
        cc.Landed();
    }
}
