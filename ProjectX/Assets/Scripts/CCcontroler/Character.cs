using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    public CharacterController charControl;

    public float jumpVelocity;
    public float walkSpeed;

    public CCJumpState CCJUMPSTATE;
    public CCWalkState CCWALKSTATE;
    public CCDeathState CCDEATHSTATE;
    public CCFallingState CCFALLINGSTATE;

    public AState currentState;

    public JumpModifiers jumpModifiers;

    public string state;

    private Vector3 _movingDir;

    public Vector3 MovingDir
    {
        get
        {
            return _movingDir;
        }
        set
        {
            _movingDir = value;
        }
    }

    public Vector3 position
    {
        get
        {
            return transform.position;
        }
    }
    // Use this for initialization
    void Start()
    {
        this.charControl = GetComponent<CharacterController>();
        jumpModifiers = new JumpModifiers();

        CCJUMPSTATE = new CCJumpState(this);
        CCWALKSTATE = new CCWalkState(this);
        CCDEATHSTATE = new CCDeathState(this);
        CCFALLINGSTATE = new CCFallingState(this);
        SetState(CCWALKSTATE);

    }

    // Update is called once per frame
    void Update()
    {
        currentState.Tick();
    }

    void FixedUpdate()
    {
        currentState.FixedTick();
    }

    public void SetState(AState state)
    {
        if (currentState != null)
        {
            currentState.OnStateExit();
        }

        currentState = state;

        if (currentState != null)
        {
            currentState.OnStateEnter();
        }
    }

    public void HitCeiling()
    {
        if(currentState == CCJUMPSTATE)
        {
            if (((CCJumpState)currentState).jumpDir.y > 0)
            {
                SetState(CCDEATHSTATE);
            }
        }
    }

    public void Die()
    {
        StartCoroutine(DeathSequence());
    }

    IEnumerator DeathSequence()
    {
        for (int i = 0; i < 40; i++)
        {
            yield return null;
        }
    }
}
