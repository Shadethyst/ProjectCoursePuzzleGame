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
    private InputAction interact;

    private GridManager gridManager;
    private GameManager gameManager;
    private Unit unit;
    private GameState currentGameState;
    [SerializeField] protected Item chosenItem;

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
            gameManager = GameObject.Find("MapManager").GetComponent<GameManager>();
            gridManager = GameObject.Find("MapManager").GetComponent<GridManager>();
            unit = this.GetComponent<Unit>();
            occupiedTile = unit.occupiedTile;
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
            instance = this;
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
                playerRenderer.flipX = false;
            }
            else if (moveRight.IsPressed() && !inputGiven)
            {
                nextTile = gridManager.getTileAtPos(new Vector2(occupiedTile.transform.position.x + distance, occupiedTile.transform.position.y));
                inputGiven = true;
                playerRenderer.flipX = true;
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
            /**
            * tries to add an element to the clicked tile,
            * checks that the direction pressed is a cardinal direction and
            * checks the whether the cardinal direction is in range defined in the item being placed by comparing hoveredTile and occupiedTile objects coordinates
            *
             */
            if (interact.IsPressed() && 
                ((occupiedTile.getCoords().x == hoveredTile.getCoords().x && System.Math.Abs(hoveredTile.getCoords().y - occupiedTile.getCoords().y) <= chosenItem.getRange())
                || (occupiedTile.getCoords().y == hoveredTile.getCoords().y && System.Math.Abs(hoveredTile.getCoords().x - occupiedTile.getCoords().x) <= chosenItem.getRange()))
                )
            {
                chosenItem.placeItem(hoveredTile.getCoords());
            }
        }
        if (nextTile && nextTile.Walkable && inputGiven)
        {
            gameManager.UpdateGameState(GameState.Turn);

            if (GameManager.Instance.state == GameState.Turn)
            {
                playerAnimator.SetBool("isWalking", true);
                player.position = Vector2.MoveTowards(player.position, nextTile.transform.position, speed * Time.deltaTime);
                if (player.position == nextTile.transform.position)
                {
                    playerAnimator.SetBool("isWalking", false);
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
    public void setAdjacencies(bool adjacency)
    {
        if (GridManager.instance.getTileAtPos(new Vector2(occupiedTile.getCoords().x + 1, occupiedTile.getCoords().y)) != null)
        {
            GridManager.instance.getTileAtPos(new Vector2(occupiedTile.getCoords().x + 1, occupiedTile.getCoords().y)).setAdjacent(adjacency);
        }
        if (GridManager.instance.getTileAtPos(new Vector2(occupiedTile.getCoords().x - 1, occupiedTile.getCoords().y)) != null)
        {
            GridManager.instance.getTileAtPos(new Vector2(occupiedTile.getCoords().x -1, occupiedTile.getCoords().y)).setAdjacent(adjacency);
        }
        if (GridManager.instance.getTileAtPos(new Vector2(occupiedTile.getCoords().x, occupiedTile.getCoords().y + 1)) != null)
        {
            GridManager.instance.getTileAtPos(new Vector2(occupiedTile.getCoords().x, occupiedTile.getCoords().y + 1)).setAdjacent(adjacency);
        }
        if (GridManager.instance.getTileAtPos(new Vector2(occupiedTile.getCoords().x, occupiedTile.getCoords().y - 1)) != null)
        {
            GridManager.instance.getTileAtPos(new Vector2(occupiedTile.getCoords().x, occupiedTile.getCoords().y - 1)).setAdjacent(adjacency);
        }
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
    public Tile getHoveredTile() { return hoveredTile; }
    public void setHoveredTile(Tile tile) {  hoveredTile = tile; }
}