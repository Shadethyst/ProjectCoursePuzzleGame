using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Water : Item
{
    [SerializeField] protected Item transformElement;
    private float movementDir;
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
    }
    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.state == GameState.Turn)
        {
            gameObject.transform.position = Vector2.MoveTowards(gameObject.transform.position, nextTile.transform.position, movementSpeed * Time.deltaTime);
        }
        if (nextTile && gameObject.transform.position == nextTile.transform.position)
        {
            GameManager.Instance.UpdateGameState(GameState.WaitForInput);
            nextTile = null;
        }
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
        base.placeItem(pos);
        GridManager.instance.getTileAtPos(pos).addItem(gameObject.GetComponent<Item>(), id);
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
    }
    /*
     * spawns new element based on the specified change Item,
     * (might want to implement differently tbh)
     */
    public void transformInto(Item change)
    {
        var spawnedElement = Instantiate(change, this.transform.position, Quaternion.identity);
        spawnedElement.name = "interactedElement";
        this.remove();
    }
    /*
     * chooses new tile to move to based on the Waters movementDir variable
     */
    private void moveInDir()
    {
        switch (movementDir)
        {
            case 0:
                nextTile = GridManager.instance.getTileAtPos(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y+1));
                break;
            case 1:
                nextTile = GridManager.instance.getTileAtPos(new Vector2(gameObject.transform.position.x+1, gameObject.transform.position.y));
                break;
            case 2:
                nextTile = GridManager.instance.getTileAtPos(new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 1));
                break;
            case 3:
                nextTile = GridManager.instance.getTileAtPos(new Vector2(gameObject.transform.position.x - 1, gameObject.transform.position.y));
                break;
            default:
                break;
        }
    }
    protected void OnOnGameStateChanged(GameState state)
    {
        if(state == GameState.Turn)
        {
            moveInDir();
        }
    }
}
