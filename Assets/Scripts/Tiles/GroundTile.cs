using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : Tile
{
    void Start()
    {
        setStartStates();
    }
    protected override void setStartStates()
    {
        defaultWalkState = true;
        defaultPlacableState = true;
        defaultFlowableState = true;
        base.setStartStates();
    }

}
