using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AIController : Controller
{
    public enum AIState { Idle, Guard, Chase, Flee, Patrol, Attack, ChooseTarget };
    public AIState currentState;
    private float lastStateChangeTime;
    public GameObject target;
    public float fleeDistance;
    public Transform[] waypoints;
    public float waypointStopDistance;
    private int currentWaypoint = 0;
    public float hearingDistance;
    public float fieldOfView = 45.0f;
    public bool isPatrolLoop;


    // Start is called before the first frame update
    public override void Start()
    {
        if (GameManager.instance != null)
        {
            //if (GameManager.instance.enemyAIsSpawnTranform != null)
           // {
                GameManager.instance.enemyAIs.Add(this);
           // }
        }
        base.Start();
    }

    public void OnDestroy()
    {
        if (GameManager.instance != null)
        {
           // if (GameManager.instance.enemyAIsSpawnTranform != null)
           // {
                GameManager.instance.enemyAIs.Remove(this);
           // }
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        MakeDecisions();
        base.Update();
    }

    public virtual void MakeDecisions()
    {
        
        switch (currentState)
        {
            case AIState.Idle:
                // Do work 
                DoIdleState();
                TargetPlayerOne();
                // Check for transitions
                if /*(IsDistanceLessThan(target, 10))*/(CanSee(target) || CanHear(target))
                {
                    ChangeState(AIState.Attack);
                }
                break;
            case AIState.Chase:
                // Do work
                DoChaseState();
                // Check for transitions
                if /*(!IsDistanceLessThan(target, 10))*/(!CanSee(target) && !CanHear(target))
                {
                    ChangeState(AIState.Idle);
                }
                if (pawn.hp != null)
                {
                    if (pawn.hp.IsHealthPercentBelow(20))
                    {
                        ChangeState(AIState.Flee);
                    }
                }
                break;
            case AIState.Flee:
                // Do work
                Flee();
                if (!pawn.hp.IsHealthPercentBelow(20))
                {
                    ChangeState(AIState.Attack);
                }
                    if /*(!IsDistanceLessThan(target, 10))*/(!CanSee(target) && !CanHear(target))
                {
                    ChangeState(AIState.Idle);
                }
                break;
            case AIState.Attack:
                DoAttackState();
                if /*(!IsDistanceLessThan(target, 10))*/(!CanSee(target) && !CanHear(target))
                {
                    ChangeState(AIState.Idle);
                }
                if (pawn.hp != null)
                {
                    if (pawn.hp.IsHealthPercentBelow(20))
                    {
                        ChangeState(AIState.Flee);
                    }
                }
                break;
            case AIState.Patrol:
                Patrol();
                if /*(IsDistanceLessThan(target, 10))*/(CanSee(target) || CanHear(target))
                {
                    ChangeState(AIState.Chase);
                }
                break;
            case AIState.ChooseTarget:
                TargetPlayerOne();
                if /*(IsDistanceLessThan(target, 10))*/(CanSee(target) || CanHear(target))
                {
                    ChangeState(AIState.Chase);
                } else if /*(!IsDistanceLessThan(target, 10))*/(!CanSee(target) && !CanHear(target))
                {
                    ChangeState(AIState.Idle);
                }
                break;
        }
    }

    public virtual void ChangeState(AIState newState)
    {
        //Change current state
        currentState = newState;

        //Save the time when we changed states
        lastStateChangeTime = Time.time;
    }

    #region States
    public void DoSeekState()
    {
        if (target.transform.position != null)
        {
            Seek(target.transform.position);
        }
    }
    protected virtual void DoChaseState()
    {
        if (target.transform.position != null)
        {
            Seek(target.transform.position);
        }
    }
    protected virtual void DoIdleState()
    {
        // Do Nothing
    }
    protected virtual void DoAttackState()
    {
        // Shoot
        Shoot();
        // Chase
        if (target.transform.position != null)
        {
            Seek(target.transform.position);
        }
       
    }
    protected void Flee()
    {
        // Find the Vector to our target
        Vector3 vectorToTarget = target.transform.position - pawn.transform.position;
        // Find the Vector away from our target by multiplying by -1
        Vector3 vectorAwayFromTarget = -vectorToTarget;
        // Find the vector we would travel down in order to flee
        Vector3 fleeVector = vectorAwayFromTarget.normalized * fleeDistance;

        float targetDistance = Vector3.Distance(target.transform.position, pawn.transform.position);
        float percentOfFleeDistance = targetDistance / fleeDistance;
        percentOfFleeDistance = Mathf.Clamp01(percentOfFleeDistance);
        float flippedPercentOfFleeDistance = 1 - percentOfFleeDistance;

        // Seek the point that is "fleeVector" away from our current position
        Seek(pawn.transform.position + fleeVector * flippedPercentOfFleeDistance);
        
    }
    protected void Patrol()
    {
        // If we have a enough waypoints in our list to move to a current waypoint
        if (waypoints.Length > currentWaypoint)
        {
            // Then seek that waypoint
            Seek(waypoints[currentWaypoint]);
            // If we are close enough, then increment to next waypoint
            if (Vector3.Distance(pawn.transform.position, waypoints[currentWaypoint].position) <= waypointStopDistance)
            {
                currentWaypoint++;
            }
        }
        else if (isPatrolLoop == true)
        {
            RestartPatrol();
        }
    }

    protected void RestartPatrol()
    {
        // Set the index to 0
        currentWaypoint = 0;
    }
    #endregion States

    #region Behaviors
    public void Seek(Vector3 targetPosition)
    {
        if (targetPosition != null)
        {
            pawn.RotateTowards(targetPosition);

            if (target != null)
            {
                pawn.MoveForward();
            }
            
        }
    }

    public void Seek(Transform targetTransform)
    {
        if (targetTransform != null)
        { 
        // Seek the position of our target Transform
        Seek(targetTransform.position);
        }
    }

    public void Seek(Pawn targetPawn)
    {
        if (targetPawn != null)
        {
            // Seek the pawn's transform!
            Seek(targetPawn.transform);
        }
    }
    public void Shoot()
    {
        // Tell the pawn to shoot
        pawn.Shoot();
    }
    public void TargetPlayerOne()
    {
        // If the GameManager exists
        if (GameManager.instance != null)
        {
            // And the array of players exists
            if (GameManager.instance.players != null)
            {
                // And there are players in it
                if (GameManager.instance.players.Count > 0)
                {
                    //Then target the gameObject of the pawn of the first player controller in the list
                    target = GameManager.instance.players[0].pawn.gameObject;
                }
            }
        }
    }
    protected void TargetNearestTank()
    {
        // Get a list of all the tanks (pawns)
        Pawn[] allTanks = FindObjectsOfType<Pawn>();

        // Assume that the first tank is closest
        Pawn closestTank = allTanks[0];
        float closestTankDistance = Vector3.Distance(pawn.transform.position, closestTank.transform.position);

        // Iterate through them one at a time
        foreach (Pawn tank in allTanks)
        {
            // If this one is closer than the closest
            if (Vector3.Distance(pawn.transform.position, tank.transform.position) <= closestTankDistance)
            {
                // It is the closest
                closestTank = tank;
                closestTankDistance = Vector3.Distance(pawn.transform.position, closestTank.transform.position);
            }
        }

        // Target the closest tank
        target = closestTank.gameObject;
    }
    #endregion Behaviors

    #region Transitions
    protected bool IsDistanceLessThan(GameObject target, float distance)
    {
        if (IsHasTarget())
        {
            if (Vector3.Distance(pawn.transform.position, target.transform.position) < distance)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
           // Debug.Log("State changed to find target");
           // ChangeState(AIState.ChooseTarget);
            return false;
        }
    }
    protected bool IsHasTarget()
    {
        // return true if we have a target, false if we don't
        return (target != null);
    }
    public bool CanHear(GameObject target)
    {
        if (IsHasTarget())
        {
            // Get the target's NoiseMaker
            NoiseMaker noiseMaker = target.GetComponent<NoiseMaker>();
            // If they don't have one, they can't make noise, so return false
            if (noiseMaker == null)
            {
                return false;
            }
            // If they are making 0 noise, they also can't be heard
            if (noiseMaker.volumeDistance <= 0)
            {
                return false;
            }
            // If they are making noise, add the volumeDistance in the noisemaker to the hearingDistance of this AI
            float totalDistance = noiseMaker.volumeDistance + hearingDistance;
            // If the distance between our pawn and target is closer than this...
            if (Vector3.Distance(pawn.transform.position, target.transform.position) <= totalDistance)
            {
                // ... then we can hear the target
                return true;
            }
            else
            {
                // Otherwise, we are too far away to hear them
                return false;
            }
        }
        else
        {
           // Debug.Log("State changed to find target");
           // ChangeState(AIState.ChooseTarget);
            return false;
        }
    }
    public bool CanSee(GameObject target)
    {/*
        if (IsHasTarget())
        {
            // Find the vector from the agent to the target
            Vector3 agentToTargetVector = target.transform.position - transform.position;
            // Find the angle between the direction our agent is facing (forward in local space) and the vector to the target.
            float angleToTarget = Vector3.Angle(agentToTargetVector, pawn.transform.forward);
            // if that angle is less than our field of view
            if (angleToTarget < fieldOfView)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            ChangeState(AIState.ChooseTarget);
            return false;
        }
        */
        // We use the location of our target in a number of calculations - store it in a variable for easy access.
        Vector3 targetPosition = target.transform.position;

        // Find the vector from the agent to the target
        // We do this by subtracting "destination minus origin", so that "origin plus vector equals destination."
        Vector3 agentToTargetVector = targetPosition - transform.position;

        // Find the angle between the direction our agent is facing (forward in local space) and the vector to the target.
        float angleToTarget = Vector3.Angle(agentToTargetVector, transform.forward);

        // if that angle is less than our field of view
        if (angleToTarget < fieldOfView)
        {
            // Create a variable to hold a ray from our position to the target
            Ray rayToTarget = new Ray();

            // Set the origin of the ray to our position, and the direction to the vector to the target
            rayToTarget.origin = transform.position;
            rayToTarget.direction = agentToTargetVector;

            // Create a variable to hold information about anything the ray collides with
            RaycastHit hitInfo;

            // Cast our ray for infinity in the direciton of our ray.
            //    -- If we hit something...
            if (Physics.Raycast(rayToTarget, out hitInfo, Mathf.Infinity))
            {
                // ... and that something is our target 
                if (hitInfo.collider.gameObject == target)
                {
                    // return true 
                    //    -- note that this will exit out of the function, so anything after this functions like an else
                    return true;
                }
            }
        }
        // return false
        //   -- note that because we returned true when we determined we could see the target, 
        //      this will only run if we hit nothing or if we hit something that is not our target.
       // Debug.Log("State changed to find target");
      //  ChangeState(AIState.ChooseTarget);
        return false;

    }
    #endregion Transitions

}
