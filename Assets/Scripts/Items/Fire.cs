using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class Fire : Item
{
    private Item transformElement;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        id = 3;
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

    public override void interact(int interaction)
    {
        base.interact(interaction);
        if(interaction == 1 || interaction == 2)
        {
            remove();
            GridManager.instance.getTileAtPos(this.transform.position).setDefaultWalkableState();
        }
        if (interaction == 5)
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
