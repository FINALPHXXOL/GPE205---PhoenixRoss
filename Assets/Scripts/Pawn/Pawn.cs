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
    // Variable for Rate of Fire
    public float fireRate;

    // Start is called before the first frame update
    public virtual void Start()
    {
        mover = GetComponent<Mover>();
    }


    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public abstract void MoveForward();
    public abstract void MoveBackward();
    public abstract void RotateClockwise();
    public abstract void RotateCounterClockwise();
    public abstract void Shoot();
    public abstract void RotateTowards(Vector3 targetPosition);
}
