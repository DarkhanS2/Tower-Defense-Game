using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : Enemy
{
    [SerializeField] private float _armor;

    // vozmozhnost' Robota est' armor

    public override void TakeDamage(float dmg)
    {
      if (_armor > 0)
       {
        float damageAfterArmor = dmg - _armor;
        _armor -= dmg;

        if (damageAfterArmor <= 0)
        {
               
         _armor = Mathf.Max(_armor, 0);
         return; 
        }
        dmg = damageAfterArmor; 
       }

       
        base.TakeDamage(dmg);
    }
}
