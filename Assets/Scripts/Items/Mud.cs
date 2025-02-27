using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mud : Item
{
    [SerializeField] public Brick brick;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        id = Id.MUD;
        range = 0;
        GameManager.Instance.changeInteractor(1);
    }
    public override void placeItem(Vector2 pos)
    {
        base.placeItem(pos);
        GridManager.instance.getTileAtPos(pos).setWalkable(false);
        GridManager.instance.getTileAtPos(pos).setPlacable(true);
    }
    public override void remove()
    {
        base.remove();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Interact(Id other)
    {
        base.Interact(other);
        // TODO fire(3) should interact with mud(5)
        if(other == Id.FIRE)
        {
            brick.placeItem(this.transform.position);
            remove();
        }
    }
}
