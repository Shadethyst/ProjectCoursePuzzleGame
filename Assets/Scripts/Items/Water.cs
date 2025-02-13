using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Water : Item
{
    [SerializeField] protected Item transformElement;
    private Vector2 movementDir;
    private Tile nextTile;
    private bool canMove;

    // Start is called before the first frame update
    void Start()
    {
        id = 1;
        range = 1;
        GameManager.Instance.changeInteractor(1);
        _itemCollider = GetComponent<Collider2D>();
        Debug.Log("setting collider2D " + _itemCollider);
        GameManager.Instance.changeInteractor(1);
        GameManager.Instance.changeMover(1);
    }

    private void Awake()
    {

        
    }
    // Update is called once per frame
    void Update()
    {
    }
    protected override void OnEnable()
    {
        GameManager.Instance.changeInteractor(1);
        _itemCollider = GetComponent<Collider2D>();
        Debug.Log("setting collider2D " + _itemCollider);
        GameManager.OnGameStateChanged += OnOnGameStateChanged;
    }
    protected override void OnDisable()
    {
        GameManager.OnGameStateChanged -= OnOnGameStateChanged;
    }
    /*
     * if the Tile does not contain an instance of the item, places a new item into the space
     */
    public override void placeItem(Vector2 pos)
    {
        if (GridManager.instance.getTileAtPos(pos).getItem(id) != false)
        {
            Debug.Log("there is water in the spot!");
        }
        /// add this check to playercontroller instead!!
        else {
            Debug.Log("placing item");
            var spawnedItem = Instantiate(this, pos, Quaternion.identity);

            Tile tile = GridManager.instance.getTileAtPos(pos);
            
            tile.addItem(id);

            Tile occupied = PlayerController.instance.getOccupiedTile();

            Vector2 delta = tile.getCoords() - occupied.getCoords();

            spawnedItem.setMovementDir(delta);
            
            Debug.Log("set movement direction: " + delta);

            canMove = false;
        }
    }
    public override void interact(int interaction)
    {
        
        if (interaction == 2)
        {
            transformInto(transformElement);
        }
        if(interaction == 3)
        {
            remove();
        }
        base.interact(interaction);
    }
    /*
     * spawns new element based on the specified change Item,
     * (might want to implement differently tbh)
     */
    public void transformInto(Item change)
    {
        change.placeItem(this.transform.position);
        this.remove();
    }
    /*
     * chooses new tile to move to based on the Waters movementDir variable
     */
    private void moveInDir(Vector2 dir)
    {
        Debug.Log("moving in dir: " + dir);

        nextTile = GridManager.instance.getTileAtPos((Vector2)gameObject.transform.position + dir);

        if (nextTile && nextTile.Flowable)
        {
            gameObject.transform.position = nextTile.transform.position;
            Debug.Log("old tile water state: " + GridManager.instance.getTileAtPos(this.transform.position).getItem(id));
            GridManager.instance.getTileAtPos(this.transform.position).removeItem(id);
            nextTile.addItem(id);
            Debug.Log(nextTile.getItem(id));
        }
        nextTile = null;

    }
    public void setMovementDir(Vector2 dir)
    {
        // Mathf.Sign(0) == 1, Math.Sign(0) == 0 :rolleyes:
        movementDir = new Vector2(Math.Sign(dir.x), Math.Sign(dir.y));
    }
    protected override void OnOnGameStateChanged(GameState state)
    {
        base.OnOnGameStateChanged(state);
        if(state == GameState.ItemMovement && canMove)
        {
            moveInDir(movementDir);
        }
        if(state == GameState.Turn && canMove == false)
        {
            canMove = true;
        }
    }
}
