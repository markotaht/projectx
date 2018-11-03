using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonekyAnimationEventHandler : MonoBehaviour {

    [SerializeField]
    CharacterControllerRb cc;
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

    void Run(bool start)
    {
        psc.Running(start);
    }

    void Fart()
    {
        psc.Fart();
    }
}
