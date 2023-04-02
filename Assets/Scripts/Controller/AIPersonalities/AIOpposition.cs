using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIOpposition : AIController
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
                TargetNearestTank();
                if (target != null)
                {
                    if (CanSee(target) || CanHear(target))
                    {
                        ChangeState(AIState.Attack);
                    }
                }

                break;
            case AIState.Flee:
                // Do work
                TargetNearestTank();
                Flee();
                if (!pawn.hp.IsHealthPercentBelow(20))
                {
                    ChangeState(AIState.Attack);
                }
                if (target != null)
                {
                    if (!CanSee(target) && !CanHear(target))
                    {
                        ChangeState(AIState.Idle);
                    }
                }
                break;
            case AIState.Patrol:
                TargetNearestTank();
                // Only travels to waypoints once
                isPatrolLoop = false;
                // Do work
                Patrol();
                // Check for transitions
                if (target != null)
                {
                    if (CanSee(target) || CanHear(target))
                    {
                        ChangeState(AIState.Attack);
                    }
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
                TargetNearestTank();
                // Loops between all waypoints
                isPatrolLoop = true;
                // Do work
                Patrol();
                // Check for transitions
                if (target != null)
                {
                    if (CanSee(target) || CanHear(target))
                    {
                        ChangeState(AIState.Attack);
                    }
                }
                if (pawn.hp != null)
                {
                    if (pawn.hp.IsHealthPercentBelow(20))
                    {
                        ChangeState(AIState.Idle);
                    }
                }
                break;
            case AIState.Attack:
                TargetNearestTank();
                DoAttackState();
                if (pawn.hp != null)
                {
                    if (pawn.hp.IsHealthPercentBelow(20))
                    {
                        ChangeState(AIState.Flee);
                    }
                }
                if (target == null || (!CanSee(target) && !CanHear(target)))
                {
                    ChangeState(AIState.Idle);
                }
                break;
        }
    }
}
