using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCWalkState : AState
{

    private float walkSpeed;
    private CharacterController charControl;

    private float lastY;

    public CCWalkState(Character character) : base(character)
    {
        this.walkSpeed = character.walkSpeed;
        this.charControl = character.charControl;
        lastY = character.position.y;
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
        if (lastY - character.position.y > 0.05)
        {
            character.SetState(character.CCFALLINGSTATE);
        }

        lastY = character.position.y;
        charControl.Move(moveDirSide * Time.deltaTime);
        charControl.SimpleMove(Vector3.zero);
    }

    public override void OnStateEnter()
    {
        character.state = "Walking";
    }
}
