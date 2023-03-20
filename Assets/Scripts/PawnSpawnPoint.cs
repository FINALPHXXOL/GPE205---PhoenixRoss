using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnSpawnPoint : MonoBehaviour
{
    public void Start()
    {
        if (GameManager.instance != null)
        {
            
                GameManager.instance.spawns.Add(this);
        }
    }
}
