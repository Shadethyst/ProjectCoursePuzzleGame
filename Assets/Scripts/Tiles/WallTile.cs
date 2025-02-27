using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTile : Tile
{
    void Start()
    {
        setStartStates();
    }
    protected override void setStartStates()
    {
        defaultWalkState = false;
        defaultPlacableState = false;
        defaultFlowableState = false;
        base.setStartStates();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
