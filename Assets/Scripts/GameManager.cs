using System.Collections;
using System.Collections.Generic;
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
    private int index;
    private int listlength;
    #endregion Variables
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
            //Destroy(gameObject);
        }

        players = new List<PlayerController>();
        enemyAIs = new List<AIController>();
        spawns = new List<PawnSpawnPoint>();
        
    }

    // Update is called once per frame
    void Start()
    {

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
    }
    public void SpawnAI()
    {
        enemyAISpawnTranform = FindRandomSpawn();
        GameObject newAIObj = Instantiate(AIControllerPrefab, Vector3.zero, Quaternion.identity);
        GameObject newPawnObj = Instantiate(AITankPawnPrefab, enemyAISpawnTranform.position, enemyAISpawnTranform.rotation);

        Controller newController = newAIObj.GetComponent<Controller>();
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();

        newController.pawn = newPawn;
    }
}
