using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankPawn : Pawn
{
    public Shooter shooter;
    public GameObject shellPrefab;
    public float fireForce;
    public float damageDone;
    public float lifespan;
    private float timeUntilNextEvent;
    float secondsPerShot;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        shooter = GetComponent<Shooter>();
        timeUntilNextEvent = 0f;
        secondsPerShot = (1f / fireRate);
        

    }

    // Update is called once per frame
    public override void Update()
    {
        base.Start();
        timeUntilNextEvent -= Time.deltaTime;
    }

    public override void MoveForward()
    {
        mover.Move(transform.forward, moveSpeed);
    }

    public override void MoveBackward()
    {
        mover.Move(transform.forward, -moveSpeed);
    }

    public override void RotateClockwise()
    {
        mover.Rotate(turnSpeed);
    }

    public override void RotateCounterClockwise()
    {
        mover.Rotate(-turnSpeed);
    }

    public override void RotateTowards(Vector3 targetPosition)
    {
        Vector3 vectorToTarget = targetPosition - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(vectorToTarget, Vector3.up);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    public override void Shoot()
    {
        if (timeUntilNextEvent <= 0)
        {
            shooter.Shoot(shellPrefab, fireForce, damageDone, lifespan);
            timeUntilNextEvent = secondsPerShot;
        }
        
    }
    
}
