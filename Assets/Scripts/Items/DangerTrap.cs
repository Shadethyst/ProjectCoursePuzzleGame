using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerTrap : Item
{

    private Vector2 movementDir;
    private Tile nextTile;
    private bool canMove;
    private bool stoppedForever;

    private Collider2D playerCollider;

    private void Awake()
    {
        id = Id.DANGER_TRAP;
        range = 0;
        GameManager.Instance.changeInteractor(1);
        _itemCollider = GetComponent<Collider2D>();
        Debug.Log("setting collider2D " + _itemCollider);
        GameManager.Instance.changeInteractor(1);
        GameManager.Instance.changeMover(1);
        playerCollider = PlayerController.instance.GetComponent<Collider2D>();
        stoppedForever = false;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public override void placeItem(Vector2 pos)
    {
        if (GridManager.instance.getTileAtPos(pos).getItem(id) == false)
        {
            var spawnedItem = Instantiate(this, pos, Quaternion.identity);
            Tile tile = GridManager.instance.getTileAtPos(pos);
            tile.addItem(id);
            Tile occupied = GridManager.instance.getTileAtPos(new Vector2(tile.getCoords().x, tile.getCoords().y - 1.0f));
            Vector2 delta = tile.getCoords() - occupied.getCoords();
            Debug.Log(tile.getCoords() + " vs." + occupied.getCoords());
            Debug.Log(occupied.getCoords());
            spawnedItem.setMovementDir(delta);
            canMove = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
          CheckForPlayer();
          CheckForDamage();
    }

    public override void Interact(Id other)
    {
        base.Interact(other);
    }

    private void moveInDir(Vector2 dir)
    {
        nextTile = GridManager.instance.getTileAtPos((Vector2)gameObject.transform.position - dir);
        if (nextTile && nextTile.Walkable || (nextTile && nextTile.getItem(Id.FIRE)) || (nextTile && nextTile.getItem(Id.MUD)))
        {
            GridManager.instance.getTileAtPos(this.transform.position).setWalkable(true);
            GridManager.instance.getTileAtPos(this.transform.position).removeItem(id);
            gameObject.transform.position = nextTile.transform.position;
            nextTile.addItem(id);
        }
        else if (nextTile && !nextTile.Walkable)
        {
            GridManager.instance.getTileAtPos(this.transform.position).setWalkable(false);
        }
    }

    public void setMovementDir(Vector2 dir)
    {
        movementDir = new Vector2(dir.x, dir.y);
    }

    protected override void OnOnGameStateChanged(GameState state)
    {
        Debug.Log("Game State is changing...");
        base.OnOnGameStateChanged(state);
        if (state == GameState.ItemMovement && canMove && !stoppedForever)
        {
            Debug.Log("moving...");
            moveInDir(movementDir);
        }
    }

    public void CheckForPlayer()
    {

        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, Vector2.down, 50, LayerMask.GetMask("Player"));
        Collider2D colliders = Physics2D.OverlapCircle(transform.position, 5.0f);

        if (raycastHit.collider != null)
        {
            if (raycastHit.collider.gameObject.tag == "Player")
            {
                canMove = true;
            }
        }
    }

    public void CheckForDamage()
    {
        Collider2D colliders = Physics2D.OverlapCircle(transform.position, 0.2f);
        if (colliders == PlayerController.instance.gameObject.GetComponent<Collider2D>() || this.transform.position == PlayerController.instance.transform.position)
        {
            GameManager.Instance.UpdateGameState(GameState.Defeat);
        }
    }
}
