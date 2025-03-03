using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static PlayerController;

public abstract class Tile : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected GameObject _highlight;
    public PlayerController occupiedUnit;
    public Tilemap tilemap;


    [SerializeField] protected bool _isWalkable;
    [SerializeField] protected bool _isPlacable;
    [SerializeField] protected bool _isFlowable;

    protected bool defaultWalkState;
    protected bool defaultPlacableState;
    protected bool defaultFlowableState;


    public Item blockingItem;
    
    [SerializeField] private bool[] items;
    
    protected Vector2 Coords;

    public bool Placable => _isPlacable;
    public bool Walkable => (_isWalkable) && blockingItem == null;
    public bool Flowable => _isFlowable;
   /* public virtual void Init(int x, int y)
    {
        items = new Item[10];
        Coords = new Vector2(x, y);
    }*/

    public bool IsPlaceable()
    {
        Tile occupied = PlayerController.instance.getOccupiedTile();
        InventoryItem chosen = PlayerController.instance.getChosenItem();
        return occupied && Placable && (occupied.getCoords().x == this.getCoords().x && System.Math.Abs(this.getCoords().y - occupied.getCoords().y) <= chosen.item.getRange()
                || (occupied.getCoords().y == this.getCoords().y && System.Math.Abs(this.getCoords().x - occupied.getCoords().x) <= chosen.item.getRange()));
                
    }

    protected virtual void OnMouseEnter()
    {
        PlayerController.instance.setHoveredTile(this);

        if(IsPlaceable())
        {
            _highlight.GetComponent<SpriteRenderer>().color = new Color(0, 1, 0, 0.4f);
        }
        else
        {
            _highlight.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.4f);
        }
        _highlight.SetActive(true);
        

    }
    protected virtual void OnMouseExit() {
        _highlight.SetActive(false);
    }
    /*private void OnEnable()
    {
        GameManager.OnGameStateChanged += OnOnGameStateChanged;
    }
    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= OnOnGameStateChanged;
    }*/
    protected virtual void OnOnGameStateChanged(GameState state)
    {
        if (state == GameState.Turn)
        {
            //checkInteraction();
        }
    }

/*    public virtual void checkInteraction()
    {
        foreach (Item interacting in items)
        {
            if (interacting)
            {
                Debug.Log("interacting object" + interacting);
                foreach (Item interacted in items)
                {
                    if (interacting != null && interacted != null && !interacting.Equals(interacted))
                    {
                        Debug.Log("interacted with: " + interacted); 
                        interacting.interact(interacted);
                    }
                }
            }

        }
    }*/


    private void Awake()
    {
        tilemap = GameObject.Find("Grid").transform.GetChild(0).GetComponent<Tilemap>();
        items = new bool[15];

    }

    // Start is called before the first frame update
    void Start()
    {
        GridManager.instance.setTileToDict(this);
        setStartStates();
    }
    protected virtual void setStartStates()
    {
        _isWalkable = defaultWalkState;
        _isPlacable = defaultPlacableState;
        _isFlowable = defaultFlowableState;
    }

    // Update is called once per frame
    void Update()
    {
        if (items.Length <= 0) items = new bool[0];
    }
    public Vector2 getCoords() 
    {
        Vector2 position = new Vector2(this.transform.position.x, this.transform.position.y);
        return position;
    }
    public void setWalkable(bool value)
    {
        _isWalkable = value;
    }
    public bool getWalkable()
    {
        return _isWalkable;
    }
    public void setFlowable(bool value)
    {
        _isFlowable = value;
    }
    public bool getFlowable()
    {
        return _isFlowable;
    }
    public void setPlacable(bool value)
    {
        _isPlacable = value;
    }
    public bool getPlacable()
    {
        return _isPlacable;
    }
    /*
     setters for default states of the tile,
    set the state of walkability, placability and flowability
    to the state that is defined as default for the tile
     */
    public void setDefaultWalkableState()
    {
        _isWalkable = defaultWalkState;
    }
    public void setDefaultPlacableState()
    {
        _isPlacable= defaultPlacableState;
    }
    public void setDefaultFlowableState()
    {
        _isFlowable= defaultFlowableState;
    }

    public void addItem(Item.Id id)
    {
        items[(int)id] = true;
    }
    public bool getItem(Item.Id id)
    {
        return items[(int)id];
    }
    public void removeItem(Item.Id id) {
        items[(int)id] = false;
    }
    
}
