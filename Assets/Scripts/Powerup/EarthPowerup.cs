using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class EarthPowerup : Powerup
{
    public float shieldToAdd;
    public Pawn owner;
    public override void Apply(PowerupManager target)
    {
        Health targetShield = target.GetComponent<Health>();
        if (targetShield != null)
        {
            targetShield.ShieldActivate(shieldToAdd);
            //targetShield.Heal(shieldToAdd);
        }
    }

    public override void Remove(PowerupManager target)
    {
        Health targetShield = target.GetComponent<Health>();
        if (targetShield != null)
        {
            targetShield.ShieldDeactivate();
            //targetShield.ClampHealth();
        }
    }
}
