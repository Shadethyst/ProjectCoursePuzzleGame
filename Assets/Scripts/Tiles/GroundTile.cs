using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : Tile
{
    private void Start()
    {
        _isWalkable = true;
        _isPlacable = true;
    }

}
