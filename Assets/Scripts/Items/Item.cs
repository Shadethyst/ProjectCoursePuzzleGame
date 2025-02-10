using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    /// placement range for the Item, default range is 1
    [SerializeField] protected int range;
    /// id of the Item for placing and finding on a tile
    [SerializeField] protected int id;
    public virtual void placeItem(Vector2 pos)
    {
        Instantiate(this, pos, Quaternion.identity);
    }
    private void Awake()
    {
        range = 1;
        id = 0;
    }
    public virtual void remove()
    {
        GridManager.instance.getTileAtPos(this.transform.position).removeItem(id);
        Destroy(this);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int getRange() {  return range; }
    public void setRange(int range) {  this.range = range; }

    /*
     * for now items interact upwards -> lower id has logic for how interaction happens with higher id Items
     */
    public virtual void interact(Item interaction)
    {

    }
}
