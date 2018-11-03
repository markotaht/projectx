using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonekyAnimationEventHandler : MonoBehaviour {

    [SerializeField]
    CharacterControllerRb cc;

    [SerializeField]
    PlayerSoundController psc;

	void Jump()
    {
        cc.Jump();
        psc.Jump();
    }

    void Landed()
    {
        cc.Landed();
    }
    
    void Die()
    {
        psc.Die();
    }

    void Fart()
    {
        psc.Fart();
    }
}
