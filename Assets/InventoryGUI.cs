using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.UI;

public class InventoryGUI : MonoBehaviour
{
    [SerializeField] private Image itemImage;

    public static InventoryGUI instance;

    private void Awake()
    {
        instance = this;        
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetImage(Item item)
    {
        itemImage.sprite = item.GetComponent<SpriteRenderer>().sprite;
    }
}
