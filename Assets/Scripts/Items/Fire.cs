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
        id = 3;
        range = 1;
        transformElement = new Brick();
        GameManager.Instance.changeInteractor(1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void interact(Item interaction)
    {
        base.interact(interaction);
        if (interaction is Mud)
        {
            interaction.remove();
            transformInto(transformElement);
        }
    }
    public void transformInto(Item change)
    {
        change.placeItem(this.transform.position);
        this.remove();
    }
}
