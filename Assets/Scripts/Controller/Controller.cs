using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class Controller : MonoBehaviour
{
    public Pawn pawn;
    public TextMeshProUGUI UIScore;
    public TextMeshProUGUI UILives;

    public float score;
    public float lives;
    
    // Start is called before the first frame update
    public virtual void Start()
    {
        if (UIScore != null)
        {
            UIScore.text = "Score: " + score;
        }
        if (UILives != null)
        {
            UILives.text = "Lives: " + lives;
        }
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
        if (UIScore != null)
        {
            score = amount + score;
        
            UIScore.text = "Score: " + score;
        }
    }

    public void RemoveScore(float amount)
    {
        if (UIScore != null)
        {
            score = score - amount;
        
            UIScore.text = "Score: " + score;
        }
    }

    public void AddLives(float amount)
    {
        if (UILives != null)
        {
            lives = amount + lives;
            if (lives >= 0)
            {
                RespawnPlayer();
            }
        
            UILives.text = "Lives: " + lives;
        }
    }

    public void RemoveLives(float amount)
    {
        if (UILives != null)
        {
            lives = lives - amount;
            if (lives >= 0)
            {
                Debug.Log("Player should be respawning.");
                RespawnPlayer();
                UIScore.text = "Score: " + score;
            } else if (lives < 0)
            {
                if (GameManager.instance != null)
                {
                    GameManager.instance.ActivateGameOver();
                }
            }
        
            UILives.text = "Lives: " + lives;
        }
    }
}
