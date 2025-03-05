using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragileTile : Tile
{
    private GameObject baseVisual;
    private GameObject brokenVisual;
    protected override void setStartStates()
    {
        defaultWalkState = true;
        defaultPlacableState = false;
        defaultFlowableState = false;
        base.setStartStates();

        baseVisual = this.transform.GetChild(1).gameObject;
        brokenVisual = this.transform.GetChild(2).gameObject;
        baseVisual.SetActive(true);
        brokenVisual.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            this.setWalkable(false);
            brokenVisual.SetActive(true);
            baseVisual.SetActive(false);
        }   
    }

   /* // Update is called once per frame
    void Update()
    {
        if (this.transform.position == PlayerController.instance.transform.position)
        {
            this.setWalkable(false);
            brokenVisual.SetActive(true);
            baseVisual.SetActive(false);
        }
    }*/
}
