using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICoward : AIController
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void MakeDecisions()
    {
        switch (currentState)
        {
            case AIState.Idle:
                // Do work 
                DoIdleState();
                TargetPlayerOne();
                break;
            case AIState.Flee:
                // Do work
                Flee();
                if (!CanSee(target) && !CanHear(target))
                {
                    ChangeState(AIState.Idle);
                }
                if (pawn.hp != null)
                {
                    if (pawn.hp.IsHealthPercentBelow(20))
                    {
                        ChangeState(AIState.Idle);
                    }
                }
                break;
            case AIState.Patrol:
                TargetPlayerOne();
                // Only travels to waypoints once
                isPatrolLoop = false;
                // Do work
                Patrol();
                // Check for transitions
                if (CanSee(target) || CanHear(target))
                {
                    ChangeState(AIState.Flee);
                }
                if (pawn.hp != null)
                {
                    if (pawn.hp.IsHealthPercentBelow(20))
                    {
                        ChangeState(AIState.Idle);
                    }
                }
                break;
            case AIState.Guard:
                TargetPlayerOne();
                // Loops between all waypoints
                isPatrolLoop = true;
                // Do work
                Patrol();
                // Check for transitions
                if (CanSee(target) || CanHear(target))
                {
                    ChangeState(AIState.Flee);
                }
                if (pawn.hp != null)
                {
                    if (pawn.hp.IsHealthPercentBelow(20))
                    {
                        ChangeState(AIState.Idle);
                    }
                }
                break;
        }
    }
}
