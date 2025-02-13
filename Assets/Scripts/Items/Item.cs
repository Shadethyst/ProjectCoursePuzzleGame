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
        if (GridManager.instance.getTileAtPos(pos).getItem(id) == null)
        {
            var spawnedItem = Instantiate(this, pos, Quaternion.identity);
            GridManager.instance.getTileAtPos(pos).addItem(spawnedItem.gameObject.GetComponent<Item>(), id);
            GridManager.instance.getTileAtPos(pos).checkInteraction();
        }
            
    }
    private void Awake()
    {
        range = 1;
        id = 0;
        GameManager.Instance.changeInteractor(1);
    }
    public virtual void remove()
    {
        GridManager.instance.getTileAtPos(this.transform.position).removeItem(id);
        GameManager.Instance.changeInteractor(-1);
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
    public int getId() { return id; }

    /*
     * for now items interact upwards -> lower id has logic for how interaction happens with higher id Items
     */
    public virtual void interact(Item interaction)
    {
        GameManager.Instance.doneInteracting();
    }
}
