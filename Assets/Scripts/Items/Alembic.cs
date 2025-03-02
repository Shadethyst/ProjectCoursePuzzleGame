using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alembic : Item
{
    private int waterAmount;
    private GameObject next;
    [SerializeField] Vector2 direction;
    // Start is called before the first frame update
    void Start()
    {
        
        List<Collider2D> foundContainer = new List<Collider2D>();
        LayerMask mask = LayerMask.GetMask("Container");
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.layerMask = mask;
        Physics2D.OverlapPoint(direction, contactFilter, foundContainer);
        if(foundContainer != null)
        {
            foundContainer[0].gameObject.GetComponent<Item>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void addWater(int water)
    {
        waterAmount += water;
    }
    public int getWater()
    {
        return waterAmount;
    }
    public override void Interact(Id other)
    {
        base.Interact(other);
        if(other == Id.WATER && waterAmount < 3)
        {
            waterAmount += 1;
        }
        if(waterAmount != 0 && other == Id.FIRE)
        {
            waterAmount = 0;

        }
    }
    public void MoveSteam(Vector2 dir)
    {

    } 
}
