using Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CustomTile : TileBase
{
    public TileType type;

    public CustomTile(TileType type)
    {
        this.type = type;
    }
}
