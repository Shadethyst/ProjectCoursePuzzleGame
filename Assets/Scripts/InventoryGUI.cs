using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.U2D;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class InventoryGUI : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text itemCount;

    public static InventoryGUI instance;

    private void Awake()
    {
        instance = this;   
    }

    // Start is called before the first frame update
    void Start()
    {
        //itemCount.text = "#";
    }

    // Update is called once per frame
    void Update()
    {
       if (GameManager.Instance.state == GameState.Story || GameManager.Instance.state == GameState.LevelComplete)
       {
            this.gameObject.SetActive(false);
       }
       else
       {
            this.gameObject.SetActive(true);
       }
    }

    public void SetImage(Item item)
    {
        itemImage.sprite = item.GetComponent<SpriteRenderer>().sprite;
    }

    public void SetCount(int count)
    {
        itemCount.text = count.ToString();
    }
}
