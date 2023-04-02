using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStoicGuard : AIController
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
            case AIState.Guard:
                TargetNearestTank();
                // Loops between all waypoints
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
                break;
            case AIState.Attack:
                TargetNearestTank();
                DoAttackState();
                if (target == null || (!CanSee(target) && !CanHear(target)))
                {
                   ChangeState(AIState.Guard);
                }
                break;
        }
    }
}
