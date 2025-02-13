using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : Item
{
    [SerializeField] protected Mud mud;
    private void Awake()
    {
        id = 2;
        GameManager.Instance.changeInteractor(1);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public override void placeItem(Vector2 pos)
    {
        /// add this check to playercontroller instead!!
        if (GridManager.instance.getTileAtPos(pos).getItem(id) == false)
        {
            Debug.Log("placing item" + this);
            base.placeItem(pos);
            GridManager.instance.getTileAtPos(pos).addItem(id);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void interact(int interaction)
    {
        base.interact(interaction);
        if(interaction == 1)
        {
            remove();
            mud.placeItem(this.transform.position);
        }
        if(interaction == 3)
        {
            remove();
        }
    }
}