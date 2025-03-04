using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CheckPoint : MonoBehaviour
{
    private Boolean passed;
    [SerializeField] int checkPointId;
    [SerializeField] List<Item> items;
    [SerializeField] List<int> amounts;
    [SerializeField] List<Tilemap> ReloadedMaps;
    [SerializeField] GameObject tilemap;
    GameObject[] environmentItems;
    [SerializeField] GameObject grid;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("collided with an object");
        if(collision.gameObject.tag == "Player" && !passed)
        {
            Debug.Log("player passed");
            passed = true;
            foreach (Item item in items)
            {
               collision.gameObject.GetComponent<PlayerController>().setItem(item, amounts[items.IndexOf(item)]);
            }

            environmentItems = GameObject.FindGameObjectsWithTag("Item");
            foreach (GameObject environmentItem in environmentItems)
            {
                Destroy(environmentItem);
                Instantiate(environmentItem);
            }
        }
    }

  /*  public IEnumerator ReloadMap()
    {
        yield return new WaitForSeconds(2f);
        Transform[] itemPlacers = tilemap.transform.GetChild(0).GetChild(0).GetComponentsInChildren<Transform>();
        Debug.Log("ItemPlacers: " + itemPlacers);
        foreach (Transform itemPlacer in itemPlacers)
        {
            itemPlacer.gameObject.GetComponent<ItemPlacer>().ReloadItems();
        }
    }*/
}
