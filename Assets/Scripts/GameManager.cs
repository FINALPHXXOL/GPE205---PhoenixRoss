using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class GameManager : MonoBehaviour
{

    #region Variables
    public static GameManager instance;

    //Prefabs
    public GameObject playerControllerPrefab;
    public GameObject tankPawnPrefab;
    public GameObject AIControllerPrefab;
    public GameObject AITankPawnPrefab;
    public Transform playerSpawnTransform;
    public Transform enemyAISpawnTranform;
    public List<PlayerController> players;
    public List<AIController> enemyAIs;
    public List<PawnSpawnPoint> spawns;
    public AudioSource deathAudio;
    public AudioClip deathClip;
    public GameObject mapGenerator;
    public GameObject newPlayerObj;
    public GameObject newAIObj;
    public float highscore;
    public int enemyCount;
    public int playerCount;
    private int index;
    private int listlength;
    #endregion Variables

    // Game States
    public GameObject AllMenus;
    public GameObject TitleScreenStateObject;
    public GameObject MainMenuStateObject;
    public GameObject OptionsScreenStateObject;
    public GameObject CreditsScreenStateObject;
    public GameObject GameplayStateObject;
    public GameObject GameOverScreenStateObject;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        players = new List<PlayerController>();
        enemyAIs = new List<AIController>();
        spawns = new List<PawnSpawnPoint>();
        
    }

    // Update is called once per frame
    void Start()
    {
        ActivateTitleScreen();
    }

    private void DeactivateAllStates()
    {
        // Deactivate all Game States
        TitleScreenStateObject.SetActive(false);
        MainMenuStateObject.SetActive(false);
        OptionsScreenStateObject.SetActive(false);
        CreditsScreenStateObject.SetActive(false);
        GameplayStateObject.SetActive(false);
        GameOverScreenStateObject.SetActive(false);
    }
    public void DestroyAllPlayerControllers()
    {
        // Loop through the list of objects to destroy
        foreach (PlayerController obj in players)
        {
            // Destroy the current object
            Destroy(obj.gameObject);
        }

        // Clear the list to remove all references to the destroyed objects
        //objectsToDestroy.Clear();
    }

    public void DestroyAllAIControllers()
    {
        // Loop through the list of objects to destroy
        foreach (AIController obj in enemyAIs)
        {
            // Destroy the current object
            Destroy(obj.gameObject);
        }

        // Clear the list to remove all references to the destroyed objects
        //objectsToDestroy.Clear();
    }

    public void DestroyAllPawns()
    {
        Pawn[] objectsToDelete = FindObjectsOfType<Pawn>();

        foreach (Pawn obj in objectsToDelete)
        {
            Destroy(obj.gameObject);
        }
    }
    public void PlayDeathSound()
    {
        deathAudio.PlayOneShot(deathClip);
    }

    public void QuitTheGame()
    {
        #if UNITY_STANDALONE
            Application.Quit();
        #endif
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }

    public void ActivateTitleScreen()
    {
        AllMenus.SetActive(true);
        // Deactivate all states
        DeactivateAllStates();
        // Activate the title screen
        TitleScreenStateObject.SetActive(true);
    }

    public void ActivateMainMenuScreen()
    {
        AllMenus.SetActive(true);
        // Deactivate all states
        DeactivateAllStates();
        // Activate the main menu screen
        MainMenuStateObject.SetActive(true);
    }

    public void ActivateOptionsScreen()
    {
        AllMenus.SetActive(true);
        // Deactivate all states
        DeactivateAllStates();
        // Activate the options screen
        OptionsScreenStateObject.SetActive(true);
    }
    
    public void ActivateCredits()
    {
        AllMenus.SetActive(true);
        // Deactivate all states
        DeactivateAllStates();
        // Activate the credits screen
        CreditsScreenStateObject.SetActive(true);
    }

    public void ActivateGameplay()
    {
        // generate map
        MapGenerator map = mapGenerator.GetComponent<MapGenerator>();
        map.GenerateMap();

        // Deactivate all states
        DeactivateAllStates();
        AllMenus.SetActive(false);
        // Activate the gameplay screen
        GameplayStateObject.SetActive(true);
        
    }

    public void FindHighScore()
    {
        foreach (PlayerController obj in players)
        {
           
           if (obj.score > highscore)
           {
                highscore = obj.score;
            }
        }
    }
    public void ActivateGameOver()
    {
        FindHighScore();
        GameOverScreenUI scoreui = GameOverScreenStateObject.GetComponent<GameOverScreenUI>();
        scoreui.SetHighScore();
        
        //figure out a way to delete every single instantiated player controller
        DestroyAllPlayerControllers();
        DestroyAllAIControllers();
        DestroyAllPawns();
        //Destroy(newPlayerObj); // So these can delete
        //Destroy(newAIObj);
        // delete map
        MapGenerator map = mapGenerator.GetComponent<MapGenerator>();
        map.DeleteMap(mapGenerator);

        AllMenus.SetActive(true);
        // Deactivate all states
        DeactivateAllStates();
        // Activate the game over screen
        GameOverScreenStateObject.SetActive(true);
    }

    public Transform FindRandomSpawn()
    {
        Transform itemTransform = spawns[UnityEngine.Random.Range(0, spawns.Count)].transform;
        return itemTransform;
    }

    public void SpawnPlayer()
    {
        playerSpawnTransform = FindRandomSpawn();
        newPlayerObj = Instantiate(playerControllerPrefab, Vector3.zero, Quaternion.identity);
        GameObject newPawnObj = Instantiate(tankPawnPrefab, playerSpawnTransform.position, playerSpawnTransform.rotation);

        Controller newController = newPlayerObj.GetComponent<Controller>();
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();

        newController.pawn = newPawn;
        newPawn.controller = newController;
    }
    
    public void SpawnAI()
    {
        enemyAISpawnTranform = FindRandomSpawn();
        newAIObj = Instantiate(AIControllerPrefab, Vector3.zero, Quaternion.identity);
        GameObject newPawnObj = Instantiate(AITankPawnPrefab, enemyAISpawnTranform.position, enemyAISpawnTranform.rotation);

        Controller newController = newAIObj.GetComponent<Controller>();
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();

        newController.pawn = newPawn;
        newPawn.controller = newController;

        enemyCount = enemyAIs.Count;
    }
}
