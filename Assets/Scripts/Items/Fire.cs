using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : Item
{
    private Item transformElement;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        id = Id.FIRE;
        range = 1;
        transformElement = new Brick();
        GameManager.Instance.changeInteractor(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void placeItem(Vector2 pos)
    {
        base.placeItem(pos);
        GridManager.instance.getTileAtPos(pos).setWalkable(false);
    }
    public override void remove()
    {
        base.remove();
    }

    public override void Interact(Id other)
    {
        base.Interact(other);
        if(other == Id.WATER || other == Id.EARTH)
        {
            remove();
            GridManager.instance.getTileAtPos(this.transform.position).setDefaultWalkableState();
        }
        if (other == Id.MUD || other == Id.ROCK)
        {
            remove();
        }
    }
    public void transformInto(Item change)
    {
        change.placeItem(this.transform.position);
        this.remove();
    }
}
