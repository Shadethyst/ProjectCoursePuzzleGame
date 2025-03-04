using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CheckPoint : MonoBehaviour
{
    private Boolean passed;
    [SerializeField] private int checkPointId;
    [SerializeField] private GameObject dataSaver;
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
        if (collision.gameObject.tag == "Player" && !passed)
        {
            Debug.Log("player passed");
            passed = true;

            dataSaver.GetComponent<DataSaver>().AddItems(items, amounts);
            dataSaver.GetComponent<DataSaver>().SetRespawnPosition(this.transform.position);

        }
    }
    public GameObject GetActiveTilemap()
    {
        return tilemap.gameObject;
    }
}

