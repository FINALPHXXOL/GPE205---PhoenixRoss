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
    //public GameObject AIControllerPrefab;
    public Transform playerSpawnTransform;
   // public Transform enemyAISpawnTranform;
    public List<PlayerController> players;
    public List<AIController> enemyAIs;
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
            Destroy(gameObject);
        }

        players = new List<PlayerController>();
        enemyAIs = new List<AIController>();
    }

    // Update is called once per frame
    void Start()
    {
        SpawnPlayer();
        //SpawnAI();
    }

    public void SpawnPlayer()
    {
        GameObject newPlayerObj = Instantiate(playerControllerPrefab, Vector3.zero, Quaternion.identity);
        GameObject newPawnObj = Instantiate(tankPawnPrefab, playerSpawnTransform.position, playerSpawnTransform.rotation);

        Controller newController = newPlayerObj.GetComponent<Controller>();
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();

        newController.pawn = newPawn;
    }
    /*public void SpawnAI()
    {
        GameObject newAIObj = Instantiate(AIControllerPrefab, Vector3.zero, Quaternion.identity);
        GameObject newPawnObj = Instantiate(tankPawnPrefab, enemyAISpawnTranform.position, enemyAISpawnTranform.rotation);

        Controller newController = newAIObj.GetComponent<Controller>();
        Pawn newPawn = newPawnObj.GetComponent<Pawn>();

        newController.pawn = newPawn;
    }*/
}
