using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alien : Enemy
{
    //vozmozhnost' Alien propuskat' 50% attack
    private bool shouldTakeDamage = true;

    public override void TakeDamage(float dmg)
    {
        if (shouldTakeDamage)
        {
            base.TakeDamage(dmg);
        }
        shouldTakeDamage = !shouldTakeDamage;
    }
}
