using Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class Placeable {
    public Vector3Int position {get; protected set;}
    public Vector2Int range {get; protected set;}
    public TileType type {get; protected set;}

   public static Placeable CreatePlaceableFromType(TileType type, Vector3Int position) {
        switch(type) {
            case TileType.LUMBER_HOUSE:
                return new LumberHouse(position);
            case TileType.PATH: 
                return new Path(position);
            case TileType.STONE: 
                return new Stone(position);
            case TileType.TREE:
                return new Tree(position);
        }
        return new Stone(position);
   }
}
