using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    [SerializeField] protected SpriteRenderer spriteRenderer;
    [SerializeField] protected GameObject _highlight;
    [SerializeField] protected bool _isWalkable;
    [SerializeField] protected bool _isPlacable;
    public Unit occupiedUnit;
    public Item blockingItem;

    public bool Placable => _isPlacable && blockingItem == null;
    public bool Walkable => _isWalkable && blockingItem == null;
    public virtual void Init(int x, int y)
    {

    }
    private void OnMouseEnter()
    {
        _highlight.SetActive(true);
    }
    private void OnMouseExit() {
        _highlight.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
