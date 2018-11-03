using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpModifiers {

    private int _foodEaten = 0;

    public int FoodEaten
    {
        get
        {
            return _foodEaten;
        }
        set
        {
            _foodEaten = value;
        }
    }

    public void EatFood()
    {
        FoodEaten += 1;
    }

    public float ApplyModifiers(float jumpVelocity, float foodForce)
    {
        return jumpVelocity + jumpVelocity * FoodEaten * foodForce;
    }
}
