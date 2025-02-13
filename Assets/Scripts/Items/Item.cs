using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public abstract class Item : MonoBehaviour
{

    /// placement range for the Item, default range is 1
    [SerializeField] protected int range;
    /// id of the Item for placing and finding on a tile
    [SerializeField] protected int id;
    protected Collider2D _itemCollider;
    protected List<Collider2D> results;
    public virtual void placeItem(Vector2 pos)
    {
        if (GridManager.instance.getTileAtPos(pos).getItem(id) != false)
        {

        }
        else
        {
            var spawnedItem = Instantiate(this.gameObject, pos, Quaternion.identity);
            spawnedItem.SetActive(true);
            GridManager.instance.getTileAtPos(pos).addItem(id);
            //GridManager.instance.getTileAtPos(pos).checkInteraction();
        }
            
    }
    private void Awake()
    {

    }
    protected virtual void OnEnable()
    {
        GameManager.Instance.changeInteractor(1);
        _itemCollider = GetComponent<Collider2D>();
        Debug.Log("setting collider2D " + _itemCollider);
        GameManager.OnGameStateChanged += OnOnGameStateChanged;
    }
    protected virtual void OnDisable()
    {
        GameManager.OnGameStateChanged -= OnOnGameStateChanged;
    }
    public virtual void remove()
    {
        GridManager.instance.getTileAtPos(this.transform.position).removeItem(id);
        GameManager.Instance.changeInteractor(-1);
        gameObject.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {
        range = 1;
        id = 0;
        _itemCollider = GetComponent<Collider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void findItems(Vector2 position, float radius)
    {
        
    }
    public int getRange() {  return range; }
    public void setRange(int range) {  this.range = range; }
    public int getId() { return id; }

    protected virtual void OnOnGameStateChanged(GameState state)
    {
        if (state == GameState.Turn)
        {
            findInteractibles();
        }
    }
    public virtual void findInteractibles()
    {
        Debug.Log("trying to find interactions");
        LayerMask mask = LayerMask.GetMask("Item");
        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.layerMask = mask;
        results = new List<Collider2D>();
        _itemCollider.OverlapCollider(contactFilter, results);
        if(results != null)
        {
            foreach (Collider2D item in results)
            {
                if (item.gameObject.GetComponent<Item>() != null && item.gameObject.GetComponent<Item>().getId() > id)
                {
                    Debug.Log("found interactible item! " + item.gameObject.GetComponent<Item>());
                    item.gameObject.GetComponent<Item>().interact(id);
                    interact(item.gameObject.GetComponent<Item>().getId());
                }

            }
        }
        GameManager.Instance.doneInteracting();
    }
    /*
     * for now items interact upwards -> lower id has logic for how interaction happens with higher id Items
     */
    public virtual void interact(int id)
    {
        
    }
}
