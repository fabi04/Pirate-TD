using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using System;


[CreateAssetMenu(menuName = "Objects/BuildingObject", fileName = "new Building")]
public class PlaceableSO: ScriptableObject
{
    public int id;
    public string title;
    public string description;
    public Sprite previewImage;

    public Tile tile;

    public TileType type;
    
    public int hitpoints;

    public List<Resources> resources;
    public List<int> cost;

}
