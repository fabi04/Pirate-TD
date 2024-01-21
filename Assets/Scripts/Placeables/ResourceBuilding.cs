using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ResourceBuilding : Placeable
{
    public Resources farmableResource {get; protected set;}
   // public abstract void OnFarmResourceClicked(TilemapManager tilemapManager, ResourcesManager resourcesManager, Vector3Int gridCoords);

    protected bool CanFarmResource(Vector3Int position, Resources tileType)
    {
        return position.x > this.position.x - range.x && position.x < this.position.x + range.x && position.y > this.position.y - range.y && position.y < this.position.y + range.y && tileType == farmableResource;
    }

    public void OnFarmResourceClicked(TilemapManager tilemapManager, ResourcesManager resourcesManager, Vector3Int gridCoords)
    {
        Resource clickedPlaceable = tilemapManager.GetPlaceableInTopLayer(gridCoords) as Resource;
        if (CanFarmResource(gridCoords, clickedPlaceable.resourceType)) {
            tilemapManager.RemoveTile(gridCoords);
            resourcesManager.AddResources(clickedPlaceable.resourceType, clickedPlaceable.yieldAmount);
        }
    }
}
