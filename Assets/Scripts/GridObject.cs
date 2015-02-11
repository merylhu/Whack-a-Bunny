using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using structures;

public class GridObject : MonoBehaviour
{
    #region Variables
    // Statics
    public int ID = 0;                                      // the ID of the grid object [1-9]
    public Dictionary<int, Direction> neighbors;            // Key Value Pairs that represent the enemies, to verify swipe gestures
    public List<Direction> allowedGestures;                 // Represents the allowed gestures for this grid as a starting held gesture
    private float timerTime;                                // Represents how much time passes before the color is reset on click
    public bool isBorderItem;                               // Whether we are on the border
    public InputManager inputManager;                       // Our Game Controller
    // Stub - GameplayManager gameplayManager, TODO Initialize

    // State
    float selectedtimer;                                    // Dynamic Timer for selected
    float glowingtimer;                                     // Dynamic timer for glowing
    public bool selected;                                   // Whether this block has been selected
    public bool glowing;                                    // Whether the item has to glow
    public bool held;                                       // Whether this is held in a swipe motion
    #endregion Variables

    #region Hooks
    // Start Function
    private void Start () {

        SetUpVariables();
        SetUpNeighbors();
	}
	
	// Update Function
    private void Update()
    {
        HandleColorStates();
    }
    #endregion Hooks

    #region Helpers

    // Handles the timer
    void HandleTimerSelected()
    {
        selectedtimer -= Time.deltaTime;
        if (selectedtimer < 0)
        {
            selected = false;
        }
    }

    // Handles the timer
    void HandleTimerGlowing()
    {
        glowingtimer -= Time.deltaTime;
        if (glowingtimer < 0)
        {
            glowing = false;
        }
    }

    // Hard-codes the appropriate values on start
    void SetUpVariables()
    {
        inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();
        timerTime = .4f;
        selected = false;
        glowingtimer = timerTime;
        selectedtimer = timerTime;
    }

    // Sets the appropriate neighbor relationships based on ID
    void SetUpNeighbors()
    {
        neighbors = new Dictionary<int, Direction>();
        allowedGestures = new List<Direction>();

        switch(ID)
        {
            case 1:
                neighbors.Add(2, Direction.east);
                neighbors.Add(5, Direction.southeast);
                neighbors.Add(4, Direction.south);
                allowedGestures.Add(Direction.east);
                allowedGestures.Add(Direction.southeast);
                allowedGestures.Add(Direction.south);
                isBorderItem = true;
                break;
            case 2:
                neighbors.Add(1, Direction.west);
                neighbors.Add(3, Direction.east);
                neighbors.Add(4, Direction.southwest);
                neighbors.Add(5, Direction.south);
                neighbors.Add(6, Direction.southeast);
                allowedGestures.Add(Direction.south);
                isBorderItem = true;
                break;
            case 3:
                neighbors.Add(2, Direction.west);
                neighbors.Add(5, Direction.southwest);
                neighbors.Add(6, Direction.south);
                allowedGestures.Add(Direction.west);
                allowedGestures.Add(Direction.southwest);
                allowedGestures.Add(Direction.south);
                isBorderItem = true;
                break;
            case 4:
                neighbors.Add(1, Direction.north);
                neighbors.Add(2, Direction.northeast);
                neighbors.Add(5, Direction.east);
                neighbors.Add(7, Direction.south);
                neighbors.Add(8, Direction.southeast);
                allowedGestures.Add(Direction.east);
                isBorderItem = true;
                break;
            case 5:
                neighbors.Add(1, Direction.northwest);
                neighbors.Add(2, Direction.north);
                neighbors.Add(3, Direction.northeast);
                neighbors.Add(4, Direction.west);
                neighbors.Add(6, Direction.east);
                neighbors.Add(7, Direction.southwest);
                neighbors.Add(8, Direction.south);
                neighbors.Add(9, Direction.southeast);
                isBorderItem = false;
                break;
            case 6:
                neighbors.Add(2, Direction.northwest);
                neighbors.Add(3, Direction.north);
                neighbors.Add(5, Direction.west);
                neighbors.Add(8, Direction.southwest);
                neighbors.Add(9, Direction.south);
                allowedGestures.Add(Direction.west);
                isBorderItem = true;
                break;
            case 7:
                neighbors.Add(4, Direction.north);
                neighbors.Add(5, Direction.northeast);
                neighbors.Add(8, Direction.east);
                allowedGestures.Add(Direction.north);
                allowedGestures.Add(Direction.northeast);
                allowedGestures.Add(Direction.east);
                isBorderItem = true;
                break;
            case 8:
                neighbors.Add(4, Direction.northwest);
                neighbors.Add(5, Direction.north);
                neighbors.Add(6, Direction.northeast);
                neighbors.Add(7, Direction.west);
                neighbors.Add(9, Direction.east);
                allowedGestures.Add(Direction.north);
                isBorderItem = true;
                break;
            case 9:
                neighbors.Add(5, Direction.northwest);
                neighbors.Add(6, Direction.north);
                neighbors.Add(8, Direction.west);
                allowedGestures.Add(Direction.west);
                allowedGestures.Add(Direction.northwest);
                allowedGestures.Add(Direction.north);
                isBorderItem = true;
                break;
            default:
                Debug.LogWarning("Grid Object: ID Out of bounds.");
                break;
        }
    }

    // Handles the colors for the block based on held and selected
    void HandleColorStates()
    {
        // If this block isn't held, do basic tap logic for color
        if (!held)
        {
            if (glowing)
            {
                gameObject.GetComponent<SpriteRenderer>().color = new Color(227f / 255f, 212f / 255f, 101f / 8f, 255f / 255f);
                HandleTimerGlowing();
            }
            else if (selected)
            {
                gameObject.GetComponent<SpriteRenderer>().color = new Color(249f / 255f, 84f / 255f, 101f / 255f, 255f / 255f);
                HandleTimerSelected();
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().color = new Color(100f / 255f, 95f / 255f, 238f / 255f, 255f / 255f);
            }
        }
        // Otherwise do held logic for color
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color(221f/ 255f, 191f / 255f, 20f / 255f, 255f / 255f);
        }
    }

    /* PUBLIC HELPERS */

    // Helper that checks if a neighbor is a valid neighbor in a direction.
    public bool isValidNeighbor(int neighbor, Direction dir)
    {
        Direction value;
        bool succeed = neighbors.TryGetValue(neighbor, out value);
        if (succeed && value == dir)
        {
            return true;
        }
        else return false;
    }

    // returns if a grid contains a neighbor
    public bool isNeighbor(int neighbor)
    {
        return neighbors.ContainsKey(neighbor);
    }

    // Grabs the direction of a neighbor, returning none if not found
    public Direction getNeighborDirection(int neighbor)
    {
        Direction value;
        bool succeed = neighbors.TryGetValue(neighbor, out value);
        if (!succeed) return Direction.none;
        else return value;
    }

    // returns if this gesture is in our allowed initial gestures
    public bool containsInitialGesture(Direction dir)
    {
        return allowedGestures.Contains(dir);
    }

    public void glow()
    {
        glowing = true;
        glowingtimer = timerTime;
    }
    #endregion Helpers

    #region Handlers
 
    // If released on this block, not just clicked
    void OnMouseUpAsButton()
    {
        selected = true;
        selectedtimer = timerTime;
    }

    // If we enter the zone
    void OnMouseEnter()
    {
        inputManager.TouchEntered(ID);
    }

    void OnMouseDown()
    {
        inputManager.TouchedDown(ID);
        print("Messaged controller");
    }
    #endregion Handlers
}
