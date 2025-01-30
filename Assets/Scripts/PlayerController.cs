using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;



/*
 *  PlayerController 
 *  - Core Idea:
 *  1) The player collides with the current tile
 *  2) The current tile's data will be saved to currentTile
 *  3) When the player is ready to move, they can use arrows or WASD
 *  4) The position of the next tile will be calculated based on the distance from currentTile
 *  5) The player will automatically move to the direction of the input (one direction at a time)
 *  6) When the player has stopped, there will be a short cooldown moment
 *  7) The process restarts from the step 1
 */
public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;
    private Transform player;
    private InputAction moveLeft;
    private InputAction moveRight;
    private InputAction moveUp;
    private InputAction moveDown;

    private Vector2 currentPosition;
    private Vector2 nextPosition;

    private bool goingLeft;
    private bool goingRight;
    private bool goingUp;
    private bool goingDown;

    private bool readyToMove;

    private GameManager gameManager; // Coming Soon
    private Tile currentTile;
    private Vector2 nextTilePosition; // Coming Soon


    private void Awake()
    {
        
        gameManager = GetComponent<GameManager>();
        playerInput = GetComponent<PlayerInput>();
        player = this.gameObject.transform;
        moveLeft = playerInput.actions.FindAction("MoveLeft");
        moveRight = playerInput.actions.FindAction("MoveRight");
        moveUp = playerInput.actions.FindAction("MoveUp");
        moveDown = playerInput.actions.FindAction("MoveDown");
    }

    // Start is called before the first frame update
    void Start()
    {
        ResetPlayerPosition(7.0f, player.position.y);
        readyToMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveLeft.IsPressed() && readyToMove)
        {
            readyToMove = false;
            goingLeft = true;
            readyToMove = false;
            nextPosition = new Vector2(currentTile.transform.position.x - 1.0f, currentTile.transform.position.y);
        }
        else if (moveRight.IsPressed() && readyToMove)
        {
            readyToMove = false;
            goingRight = true;
            nextPosition = new Vector2(currentTile.transform.position.x + 1.0f, currentTile.transform.position.y);
        }
        else if (moveUp.IsPressed() && readyToMove)
        {
            readyToMove = false;
            goingUp = true;
            nextPosition = new Vector2(currentTile.transform.position.x, currentTile.transform.position.y + 1.0f);
        }
        else if (moveDown.IsPressed() && readyToMove)
        {
            readyToMove = false;
            goingDown = true;
            nextPosition = new Vector2(currentTile.transform.position.x, currentTile.transform.position.y - 1.0f);
        }


        if (goingLeft || goingRight || goingUp || goingDown)
        {
            player.position = Vector2.MoveTowards(player.position, nextPosition, 2.0f * Time.deltaTime);

            if (player.position.x == nextPosition.x && player.position.y == nextPosition.y)
            {
                ResetMovements();
                StartCoroutine(RechargeMovement(0.001f));
            }
        }
    }

    public void ResetPlayerPosition(float x, float y)
    {
        player.position = new Vector2(x,y);
    }

    public void ResetMovements()
    {
        goingLeft = false;
        goingRight = false;
        goingUp = false;
        goingDown = false;
    }

    private IEnumerator RechargeMovement(float rechargeTime)
    {
        yield return new WaitForSeconds(rechargeTime);
        readyToMove = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.transform.position == player.position)
        {
            currentTile = collision.GetComponentInParent<Tile>();
            currentPosition = currentTile.transform.position;
        }
    }
}