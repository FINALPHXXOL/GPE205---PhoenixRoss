using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerController : Controller
{
    public KeyCode moveForwardKey;
    public KeyCode moveBackwardKey;
    public KeyCode rotateClockwiseKey;
    public KeyCode rotateCounterClockwiseKey;
    public KeyCode shootKey;

    // Start is called before the first frame update
    public override void Start()
    {
        if (GameManager.instance != null)
        {
            if (GameManager.instance.playerSpawnTransform != null)
            {
                GameManager.instance.players.Add(this);
                GameManager.instance.playerCount = GameManager.instance.players.Count;
            }
        }

        base.Start();
        
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        if (pawn != null)
        {
            ProcessInputs();
        }
    }

    public void OnDestroy()
    {
        if (GameManager.instance != null)
        {
            if (GameManager.instance.playerSpawnTransform != null)
            {
                GameManager.instance.players.Remove(this);
            }
        }
    }

    public override void RespawnPlayer()
    {
        GameManager.instance.playerSpawnTransform = GameManager.instance.FindRandomSpawn();

        GameObject newPawnObj = Instantiate(GameManager.instance.tankPawnPrefab, GameManager.instance.playerSpawnTransform.position, GameManager.instance.playerSpawnTransform.rotation);

        Pawn newPawn = newPawnObj.GetComponent<Pawn>();

        this.pawn = newPawn;
        newPawn.controller = this;

        Debug.Log("Player respawned.");

    }

    public override void AddToScore(float amount)
    {
        score = amount + score;
        if (UIScore != null)
        {
            UIScore.text = "Score: " + score;
        }
    }

    public override void RemoveScore(float amount)
    {
        score = score - amount;
        if (UIScore != null)
        {
            UIScore.text = "Score: " + score;
        }
    }

    public override void AddLives(float amount)
    {
        lives = amount + lives;
        if (lives >= 0)
        {
            RespawnPlayer();
        }
        if (UILives != null)
        {
            UILives.text = "Lives: " + lives;
        }
    }

    public override void RemoveLives(float amount)
    {
        int playerDeaths = 0;
        lives = lives - amount;
        if (lives >= 0)
        {
            Debug.Log("Player should be respawning.");
            RespawnPlayer();
        } else if (lives < 0)
        {
            if (GameManager.instance != null)
            {
                
                foreach (PlayerController obj in GameManager.instance.players)
                {

                    if (obj.lives < 0)
                    {
                        playerDeaths = playerDeaths + 1;
                        GameManager.instance.playerCount = GameManager.instance.players.Count - playerDeaths;
                        if (GameManager.instance.playerCount <= 0)
                        {
                            GameManager.instance.ActivateGameOver();
                        }

                    }
                }
                GameManager.instance.ActivateGameOver();
            }
        }
        if (UILives != null)
        {
            UILives.text = "Lives: " + lives;
        }
    }

    public void ProcessInputs()
    {
        if (Input.GetKey(moveForwardKey))
        {
            pawn.MoveForward();
        }

        if (Input.GetKey(moveBackwardKey))
        {
            pawn.MoveBackward();
        }

        if (Input.GetKey(rotateClockwiseKey))
        {
            pawn.RotateClockwise();
        }

        if (Input.GetKey(rotateCounterClockwiseKey))
        {
            pawn.RotateCounterClockwise();
        }

        if (Input.GetKeyDown(shootKey))
        {
            pawn.Shoot();
        }
    }
}
