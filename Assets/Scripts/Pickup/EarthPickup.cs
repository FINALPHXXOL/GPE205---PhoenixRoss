using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EarthPickup : Pickup
{
    public EarthPowerup powerup;
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
            Health targetShield = powerupManager.GetComponent<Health>();
            if (targetShield.shieldActive != true)
            {
                Debug.Log("shield passed through.");
                // Add the powerup
                powerupManager.Add(powerup);
            
                // Destroy this pickup
                Destroy(gameObject);
            }

        }
    }
}
