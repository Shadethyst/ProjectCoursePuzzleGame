using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Herb : MonoBehaviour
{

    public void Update()
    {
        if (this.transform.position == PlayerController.instance.transform.position)
        {
            PlayerController.instance.SetAmbrosiaItems();
            this.gameObject.GetComponent<PlayerPositioner>().movePlayer();
            this.gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       /* if(collision.gameObject.tag == "player")
        {
            Debug.Log("Player found the herb!");
            PlayerController.instance.addItem(this, 1);
            remove();
        }*/
    }
}
