using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeepTile : Tile
{
    protected override void setStartStates()
    {
        defaultWalkState = false;
        defaultPlacableState = false;
        defaultFlowableState = true;
        base.setStartStates();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
