using System;
using Extensions;
using Unity;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class BuildingData : PlaceableData
{
    public Dictionary<Resources, int> cost { get;  set; }
}

