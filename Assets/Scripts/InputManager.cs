using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using structures;

// This holds all our globally accessible structures, classes, enums, etc
namespace structures
{
    // Represents the swipe gestures
    public enum Direction
    {
        north, northeast, east, southeast, south, southwest, west, northwest, none
    }
}

public class InputManager : MonoBehaviour
{
 
    #region Variables

    // Statics
 
    // States
    public Direction direction;                 // Represents the current direction
    public List<GameObject> swipeObjects;       // The list of objects on the swipe 

    #endregion Variables

    #region Hooks
    // Use this for initialization
	void Start () {
        swipeObjects = new List<GameObject>();
        direction = Direction.none;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonUp(0))
        {
            TouchReleased();
        }

    }

    public void TouchedDown(int ID)
    {
        print("Touched down in controller");
        // Find the object
        GameObject gridObject = GameObject.Find("Grid_" + ID.ToString());

        // Only add the object if it has the potential for a full swipe gesture (AKA not #5)
        if (gridObject.GetComponent<GridObject>().isBorderItem)
        {
            print("is border item");
            swipeObjects.Add(gridObject);

            // Turn on the object
            gridObject.GetComponent<GridObject>().held = true;
        }

        direction = Direction.none;
    }
    
    // Handles the logic for when it gets a message that an object has been entered
    public void TouchEntered(int ID)
    {
        // Only performed held actions if we have pushed a first item inside and have less than 3 items
        if (swipeObjects.Count > 0 && swipeObjects.Count < 3)
        {
            // Find the object
            GameObject gridObject = GameObject.Find("Grid_" + ID.ToString());

            // For simplicity, store the script of the initial object zero
            GridObject scriptOfInitial = swipeObjects[0].GetComponent<GridObject>();

            // Now we have two options: Are we on the first item (and have no direction)                          // FIRST ITEM
            if (direction == Direction.none)                                                                      //
            {                                                                                                     //
                // Now we branch again: Is this a valid neighbor                                                  //   // VALID NEIGHBOR
                if (scriptOfInitial.isNeighbor(ID))                                                               //   // 
                {                                                                                                 //   // 
                    Direction dir = scriptOfInitial.getNeighborDirection(ID);                                     //   // 
                    if (dir == Direction.none) Debug.LogWarning("ERROR: Found a neighbor but not a direction");   //   // 
                                                                                                                  //   //   
                    // Last check: Is this direction a viable first initial direction                             //   //   // VALID DIRECTION 
                    if (scriptOfInitial.containsInitialGesture(dir))                                              //   //   // 
                    {                                                                                             //   //   // 
                        // Now we want to add the object, mark the object as held, and update the direction       //   //   //   
                        swipeObjects.Add(gridObject);                                                             //   //   //   
                        gridObject.GetComponent<GridObject>().held = true;                                        //   //   //   
                        direction = dir;                                                                          //   //   //   
                    }  
                       
                }
                // If it's an invalid neighbor, we don't have to do anything
            }                                                                                                      
            // Or are we on the second item (and have a direction)                                                 // WE HAVE A SECOND ITEM
            else                                                                                                   //
            {                                                                                                      //
                // Does this item's neighbor and direction match object [1]'s valid neighbor/direction pair?       //  // THIRD ITEM IS VALID
                if (swipeObjects[1].GetComponent<GridObject>().isValidNeighbor(ID, direction))                     //  //
                {                                                                                                  //  //
                    // Now we want to add the object, mark the object as held                                      //  //
                    swipeObjects.Add(gridObject);                                                                  //  //
                    gridObject.GetComponent<GridObject>().held = true;                                             //  //
                }                                                                                                  //   
                // If not, is this new neighbor a neighbor of item [0]?                                            //  // ITEM IS OTHER NEIGHBOR
                else if (scriptOfInitial.isNeighbor(ID))                                                           //  //
                {                                                                                                  //  //
                    Direction dir = scriptOfInitial.getNeighborDirection(ID);                                      //  //
                    if (dir == Direction.none) Debug.LogWarning("ERROR: Found a neighbor but not a direction");    //  //
                                                                                                                   //  //
                    // Last check: Is this direction a viable first initial direction                              //  //
                    if (scriptOfInitial.containsInitialGesture(dir))                                               //  //
                    {                                                                                              //  //
                        // Remove original object 2                                                                //  //
                        swipeObjects[1].GetComponent<GridObject>().held = false;                                   //  //
                        swipeObjects.RemoveAt(1);                                                                  //  // 
                                                                                                                   //  // 
                        // Now we want to add the object, mark the object as held, and update the direction        //  //
                        swipeObjects.Add(gridObject);                                                              //  //
                        gridObject.GetComponent<GridObject>().held = true;                                         //  //
                        direction = dir;                                                                           //  //
                    }   
                }       
            }
        }
    }

    void TouchReleased()
    {
        print("Release");
        // Release all GridObjects
        GameObject[] gridObjects = GameObject.FindGameObjectsWithTag("GridObject");

        // Reset all held states
        foreach (GameObject g in gridObjects)
        {
            g.GetComponent<GridObject>().held = false;
        }

        // Perform action on release
        if (swipeObjects.Count >= 3)
        {
            foreach (GameObject g in swipeObjects)
            {
                g.GetComponent<GridObject>().glow();
            }
        }

        swipeObjects.Clear();

        direction = Direction.none;

    }
    #endregion Hooks
}
