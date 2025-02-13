using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTile : Tile
{
    private void Start()
    {
        _isWalkable = false;
        _isPlacable = false;
        _isFlowable = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
