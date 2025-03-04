using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Brick : Item
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        id = Id.BRICK;

        GameManager.Instance.changeInteractor(1);
    }
    public override void placeItem(Vector2 pos)
    {
        base.placeItem(pos);
        GridManager.instance.getTileAtPos(pos).setWalkable(true);
    }
    public override void remove()
    {
        base.remove();
        GridManager.instance.getTileAtPos(gameObject.transform.position).setWalkable(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Interact(Id other)
    {
        base.Interact(other);
        if (other == Id.FIRE)
        {
            remove();
        }
        if (other == Id.EARTH)
        {
            remove();
        }
        if (other == Id.WATER)
        {
            GridManager.instance.getTileAtPos(gameObject.transform.position).setWalkable(true);
        }
    }
}
