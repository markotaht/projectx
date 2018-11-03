using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCJumpState : AState
{
    private CharacterController charControl;

    public Vector3 jumpDir = Vector3.zero;

    public float walkSpeed;
    public float jumpVelocity;

    public CCJumpState(Character character) : base(character)
    {
        this.jumpVelocity = character.jumpVelocity;
        this.walkSpeed = character.walkSpeed;
        this.charControl = character.charControl;
    }

    public override void Tick()
    {
        Vector3 moveDirSide = character.MovingDir;

        jumpDir.y += Physics.gravity.y * Time.deltaTime;

        charControl.Move(jumpDir * Time.deltaTime);
        charControl.Move(moveDirSide * Time.deltaTime);

        RaycastHit hit;

        if (jumpDir.y < 0 && Physics.Raycast(character.transform.position, Vector3.down, out hit))
        {
            if((character.position - hit.point).magnitude < 1.081)
            {
                character.SetState(character.CCWALKSTATE);
            }
        } 
    }

    public override void OnStateEnter()
    {
        character.state = "Jumping";
      //  jumpDir.y = character.jumpModifiers.ApplyModifiers(jumpVelocity);
        charControl.slopeLimit = 90;
    }

    public override void OnStateExit()
    {
        charControl.slopeLimit = 45;
    }
}
