using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
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
        if (gameObject != null)
        {
            NoiseMaker noise = gameObject.GetComponent<NoiseMaker>();
            mover.Move(transform.forward, moveSpeed);
            if (noise != null)
            {
                noise.MakeNoise(11);
            }
        }
    }

    public override void MoveBackward()
    {
        if (gameObject != null)
        {
            NoiseMaker noise = gameObject.GetComponent<NoiseMaker>();
            mover.Move(transform.forward, -moveSpeed);
            if (noise != null)
            {
                noise.MakeNoise(11);
            }
        }
    }

    public override void RotateClockwise()
    {
        if (gameObject != null)
        {
            NoiseMaker noise = gameObject.GetComponent<NoiseMaker>();
            mover.Rotate(turnSpeed);
            if (noise != null)
            {
                noise.MakeNoise(6);
            }
        }
    }

    public override void RotateCounterClockwise()
    {
        if (gameObject != null)
        {
            NoiseMaker noise = gameObject.GetComponent<NoiseMaker>();
            mover.Rotate(-turnSpeed);
            if (noise != null)
            {
                noise.MakeNoise(6);
            }
        }
    }

    public override void RotateTowards(Vector3 targetPosition)
    {
        
        Vector3 vectorToTarget = targetPosition - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(vectorToTarget, Vector3.up);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    public override void Shoot()
    {
        if (gameObject != null) { 
        NoiseMaker noise = gameObject.GetComponent<NoiseMaker>();
        if (timeUntilNextEvent <= 0)
        {
            if (shooter != null)
            {
                shooter.Shoot(shellPrefab, fireForce, damageDone, lifespan);
                timeUntilNextEvent = secondsPerShot;
                if (noise != null)
                {
                    noise.MakeNoise(21);
                }
            }
        }
        }

    }
    
}
