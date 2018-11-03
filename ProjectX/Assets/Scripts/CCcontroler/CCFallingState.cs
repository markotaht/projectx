using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCFallingState : AState {

    private CharacterController charControl;
    private float lastY;
    public CCFallingState(Character character) : base(character)
    {
        this.charControl = character.charControl;
        lastY = character.position.y;
    }

    public override void Tick()
    {
       
        if (Mathf.Approximately(lastY, character.position.y))
        {
            character.SetState(character.CCWALKSTATE);
        }      
        //charControl.SimpleMove(Vector3.zero);
        lastY = character.position.y;
    }

    public override void OnStateEnter()
    {
        character.state = "Falling";
    }
}
