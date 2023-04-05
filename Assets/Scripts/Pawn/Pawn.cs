using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Variable to hold our Mover


public abstract class Pawn : MonoBehaviour
{
    public float moveSpeed;
    public float dashSpeed;
    public float turnSpeed;
    public Mover mover;
    public Health hp;
    public bool speedUpActive;
    // Variable for Rate of Fire
    public float fireRate;
    public Controller controller;

    public AudioSource explosion3;
    public AudioSource explosion4;

    // Start is called before the first frame update
    public virtual void Start()
    {
        mover = GetComponent<Mover>();
        hp = GetComponent<Health>();
    }


    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public void SpeedUpActivate()
    {
        speedUpActive = true;
    }

    public void SpeedUpDeactive()
    {
        speedUpActive = false;
    }
    
    public void AddMoveSpeed(float amount)
    {
        if (speedUpActive != true)
        { 
        moveSpeed = moveSpeed + amount;
        }
    }

    public void RemoveMoveSpeed(float amount)
    {
        moveSpeed = moveSpeed - amount;
    }

    public void AddTurnSpeed(float amount)
    {
        turnSpeed = turnSpeed + amount;
    }

    public void RemoveTurnSpeed(float amount)
    {
        turnSpeed = turnSpeed - amount;
    }

    public abstract void MoveForward();
    public abstract void MoveBackward();
    public abstract void RotateClockwise();
    public abstract void RotateCounterClockwise();
    public abstract void Shoot();
    public abstract void RotateTowards(Vector3 targetPosition);
}
