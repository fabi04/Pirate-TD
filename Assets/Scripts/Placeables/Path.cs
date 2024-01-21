using UnityEngine;

public class Path : Placeable {
    public Path(Vector3Int position) {
        this.position = position;
        type = TileType.PATH;
        range = new Vector2Int(0, 0);
    }
}