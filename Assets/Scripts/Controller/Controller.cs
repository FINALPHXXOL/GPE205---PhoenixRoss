using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Controller : MonoBehaviour
{
    public Pawn pawn;
    public Text UIScore;
    public Text UILives;

    public float score;
    public float lives;
    
    // Start is called before the first frame update
    public virtual void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        
    }

    public void RespawnPlayer()
    {
        GameManager.instance.playerSpawnTransform = GameManager.instance.FindRandomSpawn();

        GameObject newPawnObj = Instantiate(GameManager.instance.tankPawnPrefab, GameManager.instance.playerSpawnTransform.position, GameManager.instance.playerSpawnTransform.rotation);

        Pawn newPawn = newPawnObj.GetComponent<Pawn>();

        this.pawn = newPawn;
        newPawn.controller = this;

        Debug.Log("Player respawned.");

    }

    public void AddToScore(float amount)
    {
        score = amount + score;
        UIScore.text = "Score: " + score;
    }

    public void RemoveScore(float amount)
    {
        score = score - amount;
        UIScore.text = "Score: " + score;
    }

    public void AddLives(float amount)
    {
        lives = amount + lives;
        if (lives >= 0)
        {
            RespawnPlayer();
        }
        UILives.text = "Lives: " + lives;
    }

    public void RemoveLives(float amount)
    {
        lives = lives - amount;
        if (lives >= 0)
        {
            Debug.Log("Player should be respawning.");
            RespawnPlayer();
        }
        UILives.text = "Lives: " + lives;
    }
}
