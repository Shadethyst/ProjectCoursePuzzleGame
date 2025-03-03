using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DangerTrap : Item
{
    private void Awake()
    {
        id = Id.DANGER_TRAP;
        range = 0;
        GameManager.Instance.changeInteractor(1);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public override void placeItem(Vector2 pos)
    {
        Tile tile = GridManager.instance.getTileAtPos(pos);
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
        if (other == Id.FIRE)
        {
            this.remove();
        }
    }
}
