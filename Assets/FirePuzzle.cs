using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePuzzle : MonoBehaviour
{
    private bool activated;

    private void Awake()
    {
        activated = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.state == GameState.Defeat)
        {
            this.activated = false;
            this.GetComponent<SpriteRenderer>().enabled = true;
        }

        if (GameManager.Instance.state != GameState.Defeat && GridManager.instance.getTileAtPos(this.transform.position).getItem(Item.Id.FIRE))
        {
            this.activated = true;
            this.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public bool GetIsActivated()
    {
        return this.activated;
    }
}
