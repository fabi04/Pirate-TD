using UnityEngine;

public class Stone : Resource {
    public Stone(Vector3Int position) {
        this.position = position;
        type = TileType.STONE;
        range = new Vector2Int(0, 0);
    }
}