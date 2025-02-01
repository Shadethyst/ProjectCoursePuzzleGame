using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;



/* PlayerController
 * 
 * - Updated from the previous version
 * - Idea so far:
 * --> Checks the currently Occupied Tile using GridManager
 * --> When it is the player's turn, they can try moving
 * --> If the next tile (calculated by the distance of Occupied Tile) is walkable, the walking will happen
 */
public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private Transform player;
    private InputAction moveLeft;
    private InputAction moveRight;
    private InputAction moveUp;
    private InputAction moveDown;

    private GridManager gridManager;
    private GameManager gameManager;
    private Unit unit;
    private GameState currentGameState;

    private Tile occupiedTile;
    private Tile nextTile;

    private bool inputGiven;
    private float distance = 1.0f;
    private float speed = 2.0f;
    

    private void Awake()
    {
        try
        {
            gameManager = GameObject.Find("MapManager").GetComponent<GameManager>();
            gridManager = GameObject.Find("MapManager").GetComponent<GridManager>();
            unit = this.GetComponent<Unit>();
            occupiedTile = unit.occupiedTile;
            nextTile = null;
            playerInput = GetComponent<PlayerInput>();
            player = this.gameObject.transform;
            moveLeft = playerInput.actions.FindAction("MoveLeft");
            moveRight = playerInput.actions.FindAction("MoveRight");
            moveUp = playerInput.actions.FindAction("MoveUp");
            moveDown = playerInput.actions.FindAction("MoveDown");
        } 
        catch
        {
            Debug.Log("Initialization not completed");
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        inputGiven = false;
    }

    // Update is called once per frame
    void Update()
    {
        currentGameState = gameManager.state;
        occupiedTile = CheckCurrentPosition();

        if (currentGameState == GameState.WaitForInput)
        {
            if (moveLeft.IsPressed() && !inputGiven)
            {
                nextTile = gridManager.getTileAtPos(new Vector2(occupiedTile.transform.position.x - distance, occupiedTile.transform.position.y));
                inputGiven = true;
            }
            else if (moveRight.IsPressed() && !inputGiven)
            {
                nextTile = gridManager.getTileAtPos(new Vector2(occupiedTile.transform.position.x + distance, occupiedTile.transform.position.y));
                inputGiven = true;
            }
            else if (moveUp.IsPressed() && !inputGiven)
            {
                nextTile = gridManager.getTileAtPos(new Vector2(occupiedTile.transform.position.x, occupiedTile.transform.position.y + distance));
                inputGiven = true;
            }
            else if (moveDown.IsPressed() && !inputGiven)
            {
                nextTile = gridManager.getTileAtPos(new Vector2(occupiedTile.transform.position.x, occupiedTile.transform.position.y - distance));
                inputGiven = true;
            }
        }
        if (nextTile && nextTile.Walkable && inputGiven)
        {
            gameManager.UpdateGameState(GameState.Turn);

            if (currentGameState == GameState.Turn)
            {
                player.position = Vector2.MoveTowards(player.position, nextTile.transform.position, speed * Time.deltaTime);
                if (player.position == nextTile.transform.position)
                {
                    occupiedTile = nextTile;
                    unit.SetOccupiedTile(occupiedTile);
                    nextTile = null;
                    gameManager.UpdateGameState(GameState.WaitForInput);
                }
            }
        }
        else
        {
            inputGiven = false;
            nextTile = null;
        }
    }

    public void ResetPlayerPosition(float x, float y)
    {
        player.position = new Vector2(x,y);
    }

    public Tile CheckCurrentPosition()
    {
        if (!occupiedTile)
        {
            occupiedTile = gridManager.getTileAtPos(player.position);
            unit.SetOccupiedTile(occupiedTile);
        }
        
        return unit.GetOccupiedTile();
    }
}