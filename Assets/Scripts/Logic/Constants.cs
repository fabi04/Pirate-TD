using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants
{
    public const int MAP_WIDTH = 50;
    public const int MAP_HEIGHT = 50;
    public const int CAMERA_CLIP_DIMENSION = (int)(MAP_HEIGHT * 1.5);
    public const float CAMERA_SENSITIVITY = 12f;
    public const float MOUSE_SCROLL_SENSITIVITY = 1f;
}

public enum Resources
{
    STONE, WOOD, POPULATION
}

public enum TileType
{
    NONE,
    GROUND ,
    TREE,

    BUSH,
    STONE,
    LUMBER_HOUSE ,
    PATH ,
    TOWNHALL ,
    ALL,
}
