using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleTile : Tile
{
    // Start is called before the first frame update
    void Start()
    {
        _isWalkable = false;
        _isPlacable = true;
        _isFlowable = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
