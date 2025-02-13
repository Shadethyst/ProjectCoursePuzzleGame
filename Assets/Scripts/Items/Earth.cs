using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : Item
{
    [SerializeField] protected Item transformElement;
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
        if (GridManager.instance.getTileAtPos(pos).getItem(id) == null)
        {
            Debug.Log("placing item");
            base.placeItem(pos);
            GridManager.instance.getTileAtPos(pos).addItem(gameObject.GetComponent<Item>(), id);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void interact(Item interaction)
    {
        base.interact(interaction);
    }
}