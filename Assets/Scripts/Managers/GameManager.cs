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
    public GameObject playerController2Prefab;
    public GameObject tankPawnPrefab;
    public GameObject AIControllerPrefab;
    public GameObject AITankPawnPrefab;
    public GameObject AIControllerPrefab2;
    public GameObject AITankPawnPrefab2;
    public GameObject AIControllerPrefab3;
    public GameObject AITankPawnPrefab3;
    public GameObject AIControllerPrefab4;
    public GameObject AITankPawnPrefab4;
    public Transform playerSpawnTransform;
    public Transform enemyAISpawnTranform;
    public List<PlayerController> players;
    public List<AIController> enemyAIs;
    public List<PawnSpawnPoint> spawns;
    public AudioSource deathAudio;
    public AudioClip deathClip;
    public GameObject mapGenerator;
    public Camera minimapCamera;
    public float highscore;
    public int enemyCount;
    public int playerCount;
    private int index;
    private int listlength;
    public bool isMultiplayer;
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
        if (minimapCamera != null)
        {
            if (isMultiplayer == false)
            {
                Rect rect = new Rect(0.78f, 0.02f, 0.21f, 0.21f);

                minimapCamera.rect = rect;

                minimapCamera.depth = 40;
            }
        }
        
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
        GameObject newPlayerObj = Instantiate(playerControllerPrefab, Vector3.zero, Quaternion.identity);
        GameObject newPawnObj = Instantiate(tankPawnPrefab, playerSpawnTransform.position, playerSpawnTransform.rotation);

        Controller newController = newPlayerObj.GetComponent<Controller>();
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();

        newController.pawn = newPawn;
        newPawn.controller = newController;
        newPawn.isPlayerOnePawn = true;
        newController.isPlayerOneController = true;

        if (newPawn.camera1 != null)
        {
            if (newPawn.isPlayerOnePawn == true && isMultiplayer == true)
            {
                Rect rect = new Rect(0, 0, 0.5f, 1);

                newPawn.camera1.rect = rect;

                newPawn.camera1.depth = 20;
            }
        }
    }

    public void SpawnPlayer2()
    {
        playerSpawnTransform = FindRandomSpawn();
        GameObject newPlayerObj = Instantiate(playerController2Prefab, Vector3.zero, Quaternion.identity);
        GameObject newPawnObj = Instantiate(tankPawnPrefab, playerSpawnTransform.position, playerSpawnTransform.rotation);

        Controller newController = newPlayerObj.GetComponent<Controller>();
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();

        newController.pawn = newPawn;
        newPawn.controller = newController;

        if (newPawn.camera1 != null)
        {
                Rect rect = new Rect(0.5f, 0, 0.5f, 1);

                newPawn.camera1.rect = rect;

                newPawn.camera1.depth = 20;
        }
    }
    
    public void SpawnAI()
    {
        enemyAISpawnTranform = FindRandomSpawn();
        GameObject newAIObj = Instantiate(AIControllerPrefab, Vector3.zero, Quaternion.identity);
        GameObject newPawnObj = Instantiate(AITankPawnPrefab, enemyAISpawnTranform.position, enemyAISpawnTranform.rotation);

        Controller newController = newAIObj.GetComponent<Controller>();
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();

        newController.pawn = newPawn;
        newPawn.controller = newController;

        enemyAISpawnTranform = FindRandomSpawn();
        GameObject newAIObj2 = Instantiate(AIControllerPrefab2, Vector3.zero, Quaternion.identity);
        GameObject newPawnObj2 = Instantiate(AITankPawnPrefab2, enemyAISpawnTranform.position, enemyAISpawnTranform.rotation);

        Controller newController2 = newAIObj2.GetComponent<Controller>();
        Pawn newPawn2 = newPawnObj2.GetComponent<Pawn>();

        newController2.pawn = newPawn2;
        newPawn2.controller = newController2;

        enemyAISpawnTranform = FindRandomSpawn();
        GameObject newAIObj3 = Instantiate(AIControllerPrefab3, Vector3.zero, Quaternion.identity);
        GameObject newPawnObj3 = Instantiate(AITankPawnPrefab3, enemyAISpawnTranform.position, enemyAISpawnTranform.rotation);

        Controller newController3 = newAIObj3.GetComponent<Controller>();
        Pawn newPawn3 = newPawnObj3.GetComponent<Pawn>();

        newController3.pawn = newPawn3;
        newPawn3.controller = newController3;

        enemyAISpawnTranform = FindRandomSpawn();
        GameObject newAIObj4 = Instantiate(AIControllerPrefab4, Vector3.zero, Quaternion.identity);
        GameObject newPawnObj4 = Instantiate(AITankPawnPrefab4, enemyAISpawnTranform.position, enemyAISpawnTranform.rotation);

        Controller newController4 = newAIObj4.GetComponent<Controller>();
        Pawn newPawn4 = newPawnObj4.GetComponent<Pawn>();

        newController4.pawn = newPawn4;
        newPawn4.controller = newController4;

        //enemyCount = enemyAIs.Count;
    }
}
