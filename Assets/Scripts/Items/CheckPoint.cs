using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using static UnityEditor.Progress;

public class CheckPoint : MonoBehaviour
{
    private Boolean passed;
    [SerializeField] List<Item> items;
    [SerializeField] List<int> amounts;
    [SerializeField] List<Tilemap> ReloadedMaps;
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
        }
    }
}
