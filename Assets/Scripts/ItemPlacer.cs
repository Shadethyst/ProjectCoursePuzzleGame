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
        Debug.Log("position: " + GridManager.instance.getTileAtPos(gameObject.transform.position));
        if (GridManager.instance.getTileAtPos(gameObject.transform.position))
        {
            Debug.Log("Placing item..." + placementItem);
            bool tryGet = placementItem.GetComponent<Item>();
            Debug.Log(placementItem + " caught? " + tryGet);
            Debug.Log(placementItem + " " + placementItem.transform.position);
            placementItem.GetComponent<Item>().placeItem(gameObject.transform.position);
            gameObject.SetActive(false);
        }
    }
}
