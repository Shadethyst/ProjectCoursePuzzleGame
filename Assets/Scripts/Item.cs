using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] protected int range;
    public void placeItem(Vector2 pos)
    {
        Instantiate(this, pos, Quaternion.identity);
    }
    private void Awake()
    {

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
    public abstract void interact(Item interaction);
}
