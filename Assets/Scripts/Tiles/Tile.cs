using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected GameObject _highlight;
    [SerializeField] protected bool _isWalkable;
    [SerializeField] protected bool _isPlacable;
    [SerializeField] protected bool _isAdjacent;
    [SerializeField] protected Material _adjacentMaterial;
    [SerializeField] protected Material _nonAdjacentMaterial;
    public PlayerController occupiedUnit;
    public Item blockingItem;
    private Item[] items;
    protected Vector2 Coords;
    
    public bool Placable => _isPlacable && blockingItem == null;
    public bool Walkable => _isWalkable && blockingItem == null;
    public virtual void Init(int x, int y)
    {
        items = new Item[10];
        Coords = new Vector2(x, y);
    }
    private void OnMouseEnter()
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
    private void OnMouseExit() {
        _highlight.SetActive(false);
    }
    private void OnEnable()
    {
        GameManager.OnGameStateChanged += OnOnGameStateChanged;
    }
    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= OnOnGameStateChanged;
    }
    private void OnOnGameStateChanged(GameState state)
    {
        if(state == GameState.Turn)
        {
            checkInteraction();
        }
    }



    private void checkInteraction()
    {
        foreach (Item interacting in items)
        {
            foreach (Item interacted in items)
            {
                if (interacting != null && interacted != null && !interacting.Equals(interacted))
                {
                    interacting.interact(interacted);
                }
            }

        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public Vector2 getCoords() { return Coords; }
    public bool getAdjacent()
    {
        return _isAdjacent;
    }
    public void setAdjacent(bool adjacent) {
    _isAdjacent = adjacent;
    }
    
}
