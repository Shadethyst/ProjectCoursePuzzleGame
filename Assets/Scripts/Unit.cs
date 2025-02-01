using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public Tile occupiedTile;


    public void SetOccupiedTile(Tile tile)
    {
        occupiedTile = tile;
    }

    public Tile GetOccupiedTile()
    {
        return occupiedTile;
    }
}
