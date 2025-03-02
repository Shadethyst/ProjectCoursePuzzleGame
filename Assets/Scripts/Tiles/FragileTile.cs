using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragileTile : Tile
{
    [SerializeField] GameObject brokenVisual;
    [SerializeField] GameObject baseVisual;
    protected override void setStartStates()
    {
        defaultWalkState = true;
        defaultPlacableState = false;
        defaultFlowableState = false;
        base.setStartStates();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            this.setWalkable(false);
            baseVisual.SetActive(false);
            brokenVisual.SetActive(true);

        }   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
