using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]

public class ScorePowerup : Powerup
{
    public float scoreToAdd;
    public override void Apply(PowerupManager target)
    {
        Pawn addScore = target.GetComponent<Pawn>();
        if (addScore != null)
        {
            addScore.controller.AddToScore(scoreToAdd);
        }
    }

    public override void Remove(PowerupManager target)
    {
        // TODO: Remove Health changes
    }
}
