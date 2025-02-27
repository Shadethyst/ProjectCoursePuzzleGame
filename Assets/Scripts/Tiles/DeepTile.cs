using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeepTile : Tile
{
    // Start is called before the first frame update
    void Start()
    {
        setStartStates();
    }
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
