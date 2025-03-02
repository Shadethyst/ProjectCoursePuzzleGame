using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleTile : Tile
{

    protected override void setStartStates()
    {
        
        defaultWalkState = false;
        defaultPlacableState = true;
        defaultFlowableState = true;
        base.setStartStates();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
