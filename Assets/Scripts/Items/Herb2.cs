using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Herb2 : Item
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "player")
        {
            PlayerController.instance.addItem(this, 1);
        }
    }
}
