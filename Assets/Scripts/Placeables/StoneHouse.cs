using UnityEngine;

public class StoneHouse : ResourceBuilding {
    public StoneHouse(Vector3Int position) {
        this.position = position;
        type = TileType.LUMBER_HOUSE;
        range = new Vector2Int(6, 6);
        farmableResource = Resources.STONE;
    }
}