using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Water : Item
{
    [SerializeField] protected Item transformElement;
    private int movementDir;
    private float movementSpeed;
    private Tile nextTile;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        id = 1;
        range = 1;
        movementSpeed = 2f;
        movementDir = 2;
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
            GridManager.instance.getTileAtPos(pos).addItem(spawnedItem.gameObject.GetComponent<Item>(), id);
            GridManager.instance.getTileAtPos(pos).checkInteraction();
            if(GridManager.instance.getTileAtPos(pos).getCoords().y-1 == PlayerController.instance.getOccupiedTile().getCoords().y)
            {
                spawnedItem.setMovementDir(0);
            }
            else if (GridManager.instance.getTileAtPos(pos).getCoords().y + 1 == PlayerController.instance.getOccupiedTile().getCoords().y)
            {
                spawnedItem.setMovementDir(2);
            }
            else if (GridManager.instance.getTileAtPos(pos).getCoords().x + 1 == PlayerController.instance.getOccupiedTile().getCoords().x)
            {
                spawnedItem.setMovementDir(3);
            }
            else if (GridManager.instance.getTileAtPos(pos).getCoords().x - 1 == PlayerController.instance.getOccupiedTile().getCoords().x)
            {
                spawnedItem.setMovementDir(1);
            }
            Debug.Log("set movement direction: " + movementDir);
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
    private void moveInDir(int direction)
    {
        Debug.Log("moving in dir: " + direction);
        if (direction == 0 && GridManager.instance.getTileAtPos(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 1)) != null)
        {
            nextTile = GridManager.instance.getTileAtPos(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y + 1));
            Debug.Log("cased direction: " + 0);
        }
        else if(direction == 1 && GridManager.instance.getTileAtPos(new Vector2(gameObject.transform.position.x + 1, gameObject.transform.position.y)) != null)
        {
            nextTile = GridManager.instance.getTileAtPos(new Vector2(gameObject.transform.position.x + 1, gameObject.transform.position.y));
            Debug.Log("cased direction: " + 1);
        }
        else if(direction == 2 && GridManager.instance.getTileAtPos(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 1)) != null)
        {
            nextTile = GridManager.instance.getTileAtPos(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 1));
            Debug.Log("cased direction: " + 2);
        }
        else if(direction == 3 && GridManager.instance.getTileAtPos(new Vector2(gameObject.transform.position.x - 1, gameObject.transform.position.y)) != null)
        {
            nextTile = GridManager.instance.getTileAtPos(new Vector2(gameObject.transform.position.x - 1, gameObject.transform.position.y));
            Debug.Log("cased direction: " + 3);
        }
        /*
        switch (direction)
        {
            case 0:
                nextTile = GridManager.instance.getTileAtPos(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y+1));
                Debug.Log("cased direction: " + 0);
                break;
            case 1:
                nextTile = GridManager.instance.getTileAtPos(new Vector2(gameObject.transform.position.x+1, gameObject.transform.position.y));
                Debug.Log("cased direction: " + 1);
                break;
            case 2:
                nextTile = GridManager.instance.getTileAtPos(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 1));
                Debug.Log("cased direction: " + 2);
                break;
            case 3:
                nextTile = GridManager.instance.getTileAtPos(new Vector2(gameObject.transform.position.x - 1, gameObject.transform.position.y));
                Debug.Log("cased direction: " + 3);
                break;
            default:
                break;
        }*/
        if (nextTile.Flowable)
        {
            gameObject.transform.position = nextTile.transform.position;
            GridManager.instance.getTileAtPos(gameObject.transform.position).removeItem(id);
            GridManager.instance.getTileAtPos(nextTile.transform.position).addItem(gameObject.GetComponent<Item>(), id);
        }
        nextTile = null;

    }
    public void setMovementDir(int dir)
    {
        movementDir = dir;
    }
    protected void OnOnGameStateChanged(GameState state)
    {
        if(state == GameState.ItemMovement)
        {
            moveInDir(movementDir);
        }
    }
}
