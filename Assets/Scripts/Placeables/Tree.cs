using UnityEngine;

public class Tree : Resource {
    public Tree(Vector3Int position) {
        this.position = position;
        type = TileType.TREE;
        range = new Vector2Int(0, 0);
        yieldAmount = 20;
        resourceType = Resources.WOOD;
    }
}