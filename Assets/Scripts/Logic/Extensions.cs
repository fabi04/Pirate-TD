using System;
using System.Collections.Generic;
using UnityEngine;

namespace Extensions
{
    public enum TileType
    {
        GROUND = 1,
        TREE = 2,
        STONE = 3,
        LUMBER_HOUSE = 4,
        PATH = 5,
        NONE = 9
    }
    public static class Vector2IntExtensions
    {
        public static int GetDeltaTo(this Vector2Int from, Vector2Int v2) 
        { 
            return Math.Abs(from.x - v2.x) + Math.Abs(from.y - v2.y);  
        }
    }
}
