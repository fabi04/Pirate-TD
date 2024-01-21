using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft.Json;

public class Serializer : MonoBehaviour
{
    [SerializeField] TextAsset lumberAsset;
    [SerializeField] TextAsset treeAsset;
    [SerializeField] TextAsset stoneAsset;
    public static Serializer instance;

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(this);
        }
    }

    public PlaceableData DeserializeBuilding(TileType type)
    {
        switch(type)
        {
            case TileType.TREE:
            {
                return JsonConvert.DeserializeObject<ResourceData>(treeAsset.text);
            }
            case TileType.STONE:
            {
                return JsonConvert.DeserializeObject<ResourceData>(stoneAsset.text);
            }
            case TileType.LUMBER_HOUSE:
            {
                return JsonConvert.DeserializeObject<BuildingData>(lumberAsset.text);
            }
        }
        return null;
    }
}
