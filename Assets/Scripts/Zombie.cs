using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{
    // vozmozhnost' Zombie 50% umenshenie ataki 
    public override void TakeDamage(float dmg)
    {
        base.TakeDamage(dmg * 0.50f);
    }
}

