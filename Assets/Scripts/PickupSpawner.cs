using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    public GameObject pickupPrefab;
    public float spawnDelay;
    private float nextSpawnTime;
    private GameObject spawnedPickup;
    private Transform tf;
    // Start is called before the first frame update
    void Start()
    {
        nextSpawnTime = Time.time + spawnDelay;
    }

    // Update is called once per frame
    void Update()
    {
        // If it is there is nothing spawns
        if (spawnedPickup == null)
        {
            // And it is time to spawn
            if (Time.time > nextSpawnTime)
            {
                // Spawn it and set the next time
                spawnedPickup = Instantiate(pickupPrefab, transform.position, Quaternion.identity) as GameObject;
                spawnedPickup.transform.parent = this.transform;
                nextSpawnTime = Time.time + spawnDelay;
            }
        }
        else
        {
            // Otherwise, the object still exists, so postpone the spawn
            nextSpawnTime = Time.time + spawnDelay;
        }
    }
}
