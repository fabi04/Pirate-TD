
using UnityEngine;

public class LumberHouse : Placeable {
    public LumberHouse(Vector3Int position) {
        this.position = position;
        type = TileType.LUMBER_HOUSE;
        range = new Vector2Int(10, 10);
    }
}