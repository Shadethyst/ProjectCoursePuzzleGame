using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

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
    
    private bool[] items;
    
    protected Vector2 Coords;

    public bool Placable => _isPlacable;
    public bool Walkable => (_isWalkable) && blockingItem == null;
    public bool Flowable => _isFlowable;
   /* public virtual void Init(int x, int y)
    {
        items = new Item[10];
        Coords = new Vector2(x, y);
    }*/
    protected virtual void OnMouseEnter()
    {
        PlayerController.instance.setHoveredTile(this);

        if (PlayerController.instance.getOccupiedTile() && ((PlayerController.instance.getOccupiedTile().getCoords().x == this.getCoords().x && System.Math.Abs(this.getCoords().y - PlayerController.instance.getOccupiedTile().getCoords().y) <= PlayerController.instance.getChosenItem().getRange())
                || (PlayerController.instance.getOccupiedTile().getCoords().y == this.getCoords().y && System.Math.Abs(this.getCoords().x - PlayerController.instance.getOccupiedTile().getCoords().x) <= PlayerController.instance.getChosenItem().getRange()))
                && Placable)
        {
            _highlight.GetComponent<SpriteRenderer>().color = Color.green;
        }
        else
        {
            _highlight.GetComponent<SpriteRenderer>().color = Color.white;
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
        items = new bool[10];
    }

    // Start is called before the first frame update
    void Start()
    {
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
        Vector3 position3D = tilemap.WorldToCell(new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z));
        return (Vector2)position3D;
    }
    public void setWalkable(bool value)
    {
        _isWalkable = value;
    }
    public bool getWalkable()
    {
        return _isWalkable;
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

    public void addItem(int id)
    {
        items[id] = true;
    }
    public bool getItem(int id)
    {
        return items[id];
    }
    public void removeItem(int id) {
        items[id] = false;
    }
    
}
