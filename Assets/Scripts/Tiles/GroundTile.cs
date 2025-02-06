using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : Tile
{
    private void Awake()
    {
        _isWalkable = true;
        _isPlacable = true;
    }

}
