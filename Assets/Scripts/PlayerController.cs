using System;
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
    public static PlayerController instance;
    private PlayerInput playerInput;
    private Animator playerAnimator;
    private Transform player;
    private SpriteRenderer playerRenderer;

    private InputAction moveLeft;
    private InputAction moveRight;
    private InputAction moveUp;
    private InputAction moveDown;
    private InputAction browseLeft;
    private InputAction browseRight;
    private InputAction interact;
    private InputAction stay;

    private GridManager gridManager;
    private GameManager gameManager;
    private Unit unit;
    private GameState currentGameState;
    [SerializeField] protected Item chosenItem;
    [SerializeField] protected Item[] inventory;
    private int itemSelectionCounter = 0;

    private Tile occupiedTile;
    private Tile nextTile;
    private Tile hoveredTile;

    private bool inputGiven;
    private float distance = 1.0f;
    private float speed = 2.0f;
    

    private void Awake()
    {
        try
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            gridManager = GameObject.Find("GameManager").GetComponent<GridManager>();
            nextTile = null;
            playerInput = GetComponent<PlayerInput>();
            playerAnimator = this.GetComponent<Animator>();
            player = this.gameObject.transform;
            playerRenderer = player.GetComponent<SpriteRenderer>();
            moveLeft = playerInput.actions.FindAction("MoveLeft");
            moveRight = playerInput.actions.FindAction("MoveRight");
            moveUp = playerInput.actions.FindAction("MoveUp");
            moveDown = playerInput.actions.FindAction("MoveDown");
            interact = playerInput.actions.FindAction("Interact");
            browseLeft = playerInput.actions.FindAction("BrowseLeft");
            browseRight = playerInput.actions.FindAction("BrowseRight");
            stay = playerInput.actions.FindAction("StayInPlace");
            instance = this;
            unit = GetComponent<Unit>();
            chosenItem = inventory[itemSelectionCounter];
            InventoryGUI.instance.SetImage(chosenItem);
        } 
        catch
        {
            Debug.Log("Initialization not completed");
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckCurrentPosition();
        if(GameManager.Instance.state == GameState.Movement)
        {
            player.position = Vector2.MoveTowards(player.position, nextTile.transform.position, speed * Time.deltaTime);
            if (player.position == nextTile.transform.position)
            {
                playerAnimator.SetBool("isWalking", false);
                player.position = nextTile.transform.position;
                occupiedTile = nextTile;
                unit.SetOccupiedTile(occupiedTile);
                nextTile = null;
                gameManager.UpdateGameState(GameState.ItemMovement);
            }
        }

        if (GameManager.Instance.state == GameState.LevelComplete)
        {
            StartCoroutine(StopPlayerAnimation(1.0f));
        }


    }

    IEnumerator StopPlayerAnimation(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        playerAnimator.SetBool("isWalking", false);
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
            setOccupiedTile(occupiedTile);
            unit.SetOccupiedTile(occupiedTile);
        }
        
        return instance.getOccupiedTile();
    }
    public Tile getOccupiedTile()
    {
        return occupiedTile;
    }
    public void setOccupiedTile(Tile tile)
    {
        occupiedTile = tile;
    }
    public Item getChosenItem()
    {
        return chosenItem;
    }

    public void handleItemBrowsing(InputAction.CallbackContext context)
    {
        if (context.performed) 
        {
            if (context.action.id == browseLeft.id)
            {
                if (itemSelectionCounter == 0)
                {
                    itemSelectionCounter = inventory.Length - 1;
                }
                else
                {
                    itemSelectionCounter--;
                }
            }
            else if (context.action.id == browseRight.id)
            {
                if (itemSelectionCounter == inventory.Length - 1)
                {
                    itemSelectionCounter = 0;
                }
                else
                {
                    itemSelectionCounter++;
                }
            }

            chosenItem = inventory[itemSelectionCounter];
            InventoryGUI.instance.SetImage(chosenItem);
        }
    }


    /**
    * tries to add an element to the clicked tile,
    * checks that the direction pressed is a cardinal direction and
    * checks the whether the cardinal direction is in range defined in the item being placed by comparing hoveredTile and occupiedTile objects coordinates
    *
    */
    public void handlePlacement(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            var delta = hoveredTile.getCoords() - occupiedTile.getCoords();
            bool inRange = (delta.x == 0 && Math.Abs(delta.y) <= chosenItem.getRange())
                || (delta.y == 0 && Math.Abs(delta.x) <= chosenItem.getRange());

            if (occupiedTile && GameManager.Instance.state == GameState.WaitForInput && hoveredTile && inRange)
            {
                gameManager.UpdateGameState(GameState.Placement);
            }
        }
    }
    public void handleMovement(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if(GameManager.Instance.state == GameState.WaitForInput)
            {
                Vector2 occupied = occupiedTile.getCoords();
                var left = occupied - distance * Vector2.right;
                var right = occupied + distance * Vector2.right;
                var down = occupied - distance * Vector2.up;
                var up = occupied + distance * Vector2.up;

                if (context.action.id == moveLeft.id && gridManager.getTileAtPos(left))
                {
                    nextTile = gridManager.getTileAtPos(left);
                    playerRenderer.flipX = false;
                }
                else if (context.action.id == moveRight.id && gridManager.getTileAtPos(right))
                {
                    nextTile = gridManager.getTileAtPos(right);
                    playerRenderer.flipX = true;
                }
                else if (context.action.id == moveDown.id && gridManager.getTileAtPos(down))
                {
                    nextTile = gridManager.getTileAtPos(down);
                }
                else if (context.action.id == moveUp.id && gridManager.getTileAtPos(up))
                {
                    nextTile = gridManager.getTileAtPos(up);
                }

                if (nextTile && nextTile.Walkable)
                {
                    GameManager.Instance.UpdateGameState(GameState.Movement);
                    playerAnimator.SetBool("isWalking", true);
                }
                else
                {
                    nextTile = null;
                }
            }

        }

    }
    public void handleStayInPlace(InputAction.CallbackContext context)
    {
        if(context.performed && context.action.id == stay.id && GameManager.Instance.state == GameState.WaitForInput)
        {
            GameManager.Instance.UpdateGameState(GameState.ItemMovement);
        }
    }
    public void movePlayer()
    {
        inputGiven = true;
    }
    public void placeSelected()
    {
        chosenItem.placeItem(hoveredTile.transform.position);
       // chosenItem.placeItem(hoveredTile.getCoords());
        gameManager.UpdateGameState(GameState.ItemMovement);
    }
    public Tile getHoveredTile() { return hoveredTile; }
    public void setHoveredTile(Tile tile) {  hoveredTile = tile; }

}
