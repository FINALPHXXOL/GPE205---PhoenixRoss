using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject[] gridPrefabs;
    public int rows;
    public int cols;
    public float roomWidth = 50.0f;
    public float roomHeight = 50.0f;
    private Room[,] grid;
    public int mapSeed;
    public bool isMapOfTheDay;
    public bool isRandomSeed;
    public int mapgenCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        //GenerateMap();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int DateToInt(DateTime dateToUse)
    {
        // Add our date up and return it
        return dateToUse.Year + dateToUse.Month + dateToUse.Day + dateToUse.Hour + dateToUse.Minute + dateToUse.Second + dateToUse.Millisecond;
    }

    /// <summary>
    /// Return a random from our array of prefabs
    /// </summary>
    /// <returns></returns>
    public GameObject RandomRoomPrefab()
    {
        return gridPrefabs[UnityEngine.Random.Range(0, gridPrefabs.Length)];
    }

    public void GenerateMap()
    {
        

        if (!isRandomSeed)
        {
            // Set our seed
            UnityEngine.Random.seed = mapSeed;
        }
        if (isMapOfTheDay)
        {
            UnityEngine.Random.seed = DateToInt(DateTime.Now.Date);
            mapSeed = DateToInt(DateTime.Now.Date);
        }

        DateToInt(DateTime.Now);

        //Clear out the grid
        grid = new Room[cols, rows];

        //For each grid row...
        for (int currentRow = 0; currentRow < rows; currentRow++)
        {
            //For each column in that row...
            for (int currentCol = 0; currentCol < cols; currentCol++)
            {
                #region Generate

                //Figure out the location
                float xPosition = roomWidth * currentCol;
                float zPosition = roomHeight * currentRow;
                Vector3 newPosition = new Vector3(xPosition, 0.0f, zPosition);

                //Create a map tile at that position
                GameObject tempRoomObj = Instantiate(RandomRoomPrefab(), newPosition, Quaternion.identity) as GameObject;

                //Set the map tile's parent
                tempRoomObj.transform.parent = this.transform;

                //Give it a meaningful name
                tempRoomObj.name = "Room_" + currentCol + ", " + currentRow;

                //Get the Room object reference
                Room tempRoom = tempRoomObj.GetComponent<Room>();

                //Save it to the grid array
                grid[currentCol, currentRow] = tempRoom;

                #endregion
                
                #region Doors

                // Open the doors
                // If we are on the bottom row, open the north door
                if (currentRow == 0)
                {
                    tempRoom.doorNorth.SetActive(false);
                }
                else if (currentRow == rows - 1)
                {
                    // Otherwise, if we are on the top row, open the south door
                    Destroy(tempRoom.doorSouth);
                }
                else
                {
                    // Otherwise, we are in the middle, so open both doors
                    Destroy(tempRoom.doorNorth);
                    Destroy(tempRoom.doorSouth);
                }

                if (currentCol == 0)
                {
                    tempRoom.doorEast.SetActive(false);
                }
                else if (currentCol == cols - 1)
                {
                    Destroy(tempRoom.doorWest);
                }
                else
                {
                    Destroy(tempRoom.doorWest);
                    Destroy(tempRoom.doorEast);
                }

                #endregion
                
            }
        }
        GameManager.instance.SpawnPlayer();
        GameManager.instance.SpawnAI();
        if (GameManager.instance.isMultiplayer == true)
        {
            GameManager.instance.SpawnPlayer2();
        }
    }

    public void DeleteMap(GameObject parentObject)
    {
        // Iterate through all child objects of the parent object
        for (int i = parentObject.transform.childCount - 1; i >= 0; i--)
        {
            // Get a reference to the current child object
            GameObject childObject = parentObject.transform.GetChild(i).gameObject;
            
            // Destroy the child object
            Destroy(childObject);
        }
    }
}
