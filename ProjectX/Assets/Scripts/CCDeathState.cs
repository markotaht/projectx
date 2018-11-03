using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCDeathState : AState
{

    private CharacterController charControl;
    public CCDeathState(Character character) : base(character)
    {
        this.charControl = character.charControl;
    }

    public override void Tick()
    {
        
    }

    public override void OnStateEnter()
    {
        character.state = "Death";
        character.Die();
    }

}
