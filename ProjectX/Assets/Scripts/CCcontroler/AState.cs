using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AState
{

    protected Character character;
    public AState(Character character)
    {
        this.character = character;
    }

    public abstract void Tick();
    public virtual void FixedTick() { }

    public virtual void OnStateEnter() { }
    public virtual void OnStateExit() { }
}
