using UnityEngine;

public class Tree : Placeable {
    public Tree(Vector3Int position) {
        this.position = position;
        type = TileType.TREE;
        range = new Vector2Int(0, 0);
    }
}