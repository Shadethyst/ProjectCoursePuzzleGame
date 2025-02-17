using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : Item
{
    [SerializeField] protected Mud mud;
    private void Awake()
    {
        id = Id.EARTH;
        GameManager.Instance.changeInteractor(1);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public override void placeItem(Vector2 pos)
    {
        Tile tile = GridManager.instance.getTileAtPos(pos);
        /// add this check to playercontroller instead!!
        if (tile.getItem(id) == false)
        {
            base.placeItem(pos);
            tile.addItem(id);
            tile.setWalkable(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Interact(Id other)
    {
        base.Interact(other);
        // TODO earth(2) interacts with water(1) and fire(3), should interact up only
        if(other == Id.WATER)
        {
            remove();
            mud.placeItem(this.transform.position);
        }
        if(other == Id.FIRE)
        {
            remove();
        }
    }
}