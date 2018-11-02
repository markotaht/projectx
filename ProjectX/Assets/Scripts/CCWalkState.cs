using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCWalkState : AState
{

    private float walkSpeed;
    private CharacterController charControl;

    public CCWalkState(Character character) : base(character)
    {
        this.walkSpeed = character.walkSpeed;
        this.charControl = character.charControl;
    }

    public override void Tick()
    {
        float horiz = Input.GetAxisRaw("Horizontal");

        Vector3 moveDirSide = character.transform.right * horiz * walkSpeed;
        character.MovingDir = moveDirSide;

        if (Input.GetButtonDown("Jump"))
        {
            character.SetState(character.CCJUMPSTATE);
        }

        charControl.SimpleMove(moveDirSide);
    }

    public override void OnStateEnter()
    {
        character.state = "Walking";
    }
}
