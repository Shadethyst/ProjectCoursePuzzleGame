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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        id = 1;
        range = 1;
        
        GameManager.Instance.changeInteractor(1);
        GameManager.Instance.changeMover(1);
        
    }
    // Update is called once per frame
    void Update()
    {
    }
    private void OnEnable()
    {
        GameManager.OnGameStateChanged += OnOnGameStateChanged;
    }
    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= OnOnGameStateChanged;
    }
    /*
     * if the Tile does not contain an instance of the item, places a new item into the space
     */
    public override void placeItem(Vector2 pos)
    {
        /// add this check to playercontroller instead!!
        if(GridManager.instance.getTileAtPos(pos).getItem(id) == null)
        {
            Debug.Log("placing item");
            var spawnedItem = Instantiate(this, pos, Quaternion.identity);

            Tile tile = GridManager.instance.getTileAtPos(pos);
            
            tile.addItem(spawnedItem.gameObject.GetComponent<Item>(), id);
            tile.checkInteraction();

            Tile occupied = PlayerController.instance.getOccupiedTile();

            Vector2 delta = tile.getCoords() - occupied.getCoords();

            spawnedItem.setMovementDir(delta);

            Debug.Log("set movement direction: " + delta);
        }
    }
    public override void interact(Item interaction)
    {
        
        if (interaction is Earth)
        {
            interaction.remove();
            transformInto(transformElement);
        }
        if(interaction is Fire)
        {
            interaction.remove();
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
            GridManager.instance.getTileAtPos(gameObject.transform.position).removeItem(id);
            nextTile.addItem(gameObject.GetComponent<Item>(), id);
        }
        nextTile = null;

    }
    public void setMovementDir(Vector2 dir)
    {
        // Mathf.Sign(0) == 1, Math.Sign(0) == 0 :rolleyes:
        movementDir = new Vector2(Math.Sign(dir.x), Math.Sign(dir.y));
    }
    protected void OnOnGameStateChanged(GameState state)
    {
        if(state == GameState.ItemMovement)
        {
            moveInDir(movementDir);
        }
    }
}
