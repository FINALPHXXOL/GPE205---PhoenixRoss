using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBlindAssassin : AIController
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
                // Check for transitions
                if (target != null)
                {
                    if (CanHear(target))
                    {
                        ChangeState(AIState.Attack);
                    }
                }
                break;
            case AIState.Attack:
                TargetNearestTank();
                DoAttackState();
                if (target == null)
                {
                    ChangeState(AIState.Idle);
                }
                    break;
        }
    }
}
