using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindPickup : Pickup
{
    public WindPowerup powerup;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnTriggerEnter(Collider other)
    {
        // variable to store other object's PowerupController - if it has one
        PowerupManager powerupManager = other.GetComponent<PowerupManager>();
       
        // If the other object has a PowerupController
        if (powerupManager != null)
        {
            Pawn targetSpeed = powerupManager.GetComponent<Pawn>();
            // Add the powerup
            if (targetSpeed.speedUpActive != true)
            {
                Debug.Log("speed passed through.");
                powerupManager.Add(powerup);
            
                // Destroy this pickup
                Destroy(gameObject);
            }


        }
    }
}
