using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlacer : MonoBehaviour
{
    [SerializeField] public GameObject placementItem;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void OnEnable()
    {

    }


    // Update is called once per frame
    void Update()
    {
        if (GridManager.instance.getTileAtPos(gameObject.transform.position))
        {
            placementItem.GetComponent<Item>().placeItem(gameObject.transform.position);
            this.enabled = false;
        }
    }
}
