using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class WindPowerup : Powerup
{
    public float speedToAdd;
    public float turnSpeedToAdd;
    public override void Apply(PowerupManager target)
    {
        Pawn targetSpeed = target.GetComponent<Pawn>();
        if (targetSpeed != null)
        {
            targetSpeed.AddMoveSpeed(speedToAdd);
            targetSpeed.AddTurnSpeed(turnSpeedToAdd);
            targetSpeed.SpeedUpActivate();
        }
    }

    public override void Remove(PowerupManager target)
    {
        Pawn targetSpeed = target.GetComponent<Pawn>();
        if (targetSpeed != null)
        {
            targetSpeed.SpeedUpDeactive();
            targetSpeed.RemoveMoveSpeed(speedToAdd);
            targetSpeed.RemoveTurnSpeed(turnSpeedToAdd);
        }
    }
}
