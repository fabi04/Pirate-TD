using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Tilemaps;
using Unity.VisualScripting;
using System.Linq;

/// <summary>
/// Manages the tilemap and all actions that are done with it.
/// </summary>
public class TilemapManager : MonoBehaviour
{
    [SerializeField] Tilemap topLayer;
    [SerializeField] Tilemap bottomLayer;
    [SerializeField] Tilemap selectionLayer;
    [SerializeField] Tilemap rangeSelectionLayer;
    [SerializeField] TextMeshProUGUI text;

    private Vector3Int oldMousePosition;
    public Tile selectedTile;
    public Tile bottomTile;
    public Tile[] pathTiles;

    private Dictionary<Vector3Int, Placeable> tiles = new Dictionary<Vector3Int, Placeable>();

    private Dictionary<Vector3Int, bool> selectedRange = new Dictionary<Vector3Int, bool>();

    public void Update()
    {
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        text.text = "" + bottomLayer.WorldToCell(worldPoint).x + bottomLayer.WorldToCell(worldPoint).y;
    }

    /// <summary>
    /// Adds a tile to the tilemap. 
    /// </summary>
    /// <param name="position">The position to add the tile at.</param>
    /// <param name="tile">The tile to add.</param>
    /// <param name="type">The tile's type.</param>
    public void AddTile(Vector3Int position, Tile tile, TileType type)
    {
        if (type == TileType.PATH) {
            AddPath(position, tile, true);
            return;
        } 
        topLayer.SetTile(position, tile);
        tiles.Add(position, Placeable.CreatePlaceableFromType(type, position));
    }

    /// <summary>
    /// Adds a path to the tilemap and recursively updates the neighbours to adapt their tiles. 
    /// </summary>
    /// <param name="position">The position to add the path at.</param>
    /// <param name="tile">The Path tile.</param>
    /// <param name="callRecursive">Should be true in the initial call to update all neighbours.</param>
    private void AddPath(Vector3Int position, Tile tile, bool callRecursive) {
        int[] neighbours = NeighboursWithPath(position);
        tile = GetComponent<PathManager>().GetPathForNeighbours(neighbours);
        topLayer.SetTile(position, tile);
        if (callRecursive) {
            tiles.Add(position, Placeable.CreatePlaceableFromType(TileType.PATH, position));
            for (int i = 0; i < neighbours.Length; i++) {
                if (neighbours[i] == 1) {
                    int[] single_neighbours = new int[4];
                    single_neighbours[i] = 1;
                    Vector3Int neighbourPosition = GetComponent<PathManager>().GetPositionForNeighbour(position, single_neighbours);
                    AddPath(neighbourPosition, tile, false);
                }
            }
        }
    }

    public void AddTileToBottomLayer(Vector3Int position, Tile tile)
    {
        bottomLayer.SetTile(position, tile);
    }

    public void RemoveTile(Vector3Int position)
    {
        topLayer.SetTile(position, null);
        tiles.Remove(position);
    }

    /// <summary>
    /// Finds all directly adjacent tiles that contain a path.
    /// </summary>
    /// <param name="position">The position to search for neighbours.</param>
    /// <returns>An array containg the neighbours with 1 indicating a neighbour in the corresponding direction.</returns>
    public int[] NeighboursWithPath(Vector3Int position) {
        int[] res = new int[4];
        int[] vertNeighbours = GetNeighbours(position, true);
        int[] horNeighbours = GetNeighbours(position, false);

        horNeighbours.CopyTo(res, 0);
        vertNeighbours.CopyTo(res, 2);

        return res;
    }

    /// <summary>
    /// Returns whether the given position has a neigbour that is a path.
    /// </summary>
    /// <param name="position">The position to search for neighbours</param>
    public bool HasPathAsNeighbour(Vector3Int position) {
        return NeighboursWithPath(position).Contains(1);
    }

    private int[] GetNeighbours(Vector3Int position, bool vertical) {
        int[] res = new int[2];
        int index = 0;
        for(int i = -1; i <= 1; i += 2) {
            Placeable placeable;
            if (tiles.TryGetValue(new Vector3Int(vertical ? position.x + i : position.x, vertical ? position.y : position.y + i, 0), out placeable)) {
                if (placeable.type == TileType.PATH) {
                    res[index] = 1;
                } else {
                    res[index] = 0;
                }
            }
            index ++;
        }
        return res;
    }

    public Vector2Int GetMapBoundaries()
    {
        return new Vector2Int(bottomLayer.cellBounds.max.x, bottomLayer.cellBounds.max.y);
    }

    public Vector2Int GetMapBoundariesMin()
    {
        return new Vector2Int(bottomLayer.cellBounds.min.x, bottomLayer.cellBounds.min.x);
    }

    /// <summary>
    /// Draws the selected square at the position the mouse is pointing to.
    /// </summary>
    /// <param name="worldPos">The mouse position.</param>
    public void MouseOverTile(Vector3 worldPos) {
        Vector3Int position = ConvertWorldToCell(worldPos);
        if (!GetTileInBottomLayer(position)) {
            return;
        }
        if (oldMousePosition != null) {
            selectionLayer.SetTile(oldMousePosition, null);
        }
        selectionLayer.SetTile(position, selectedTile);
        oldMousePosition = position;
    }

    public void DeselectTile(Vector3Int position)
    {
        selectionLayer.SetTile(position, null);
        topLayer.SetColor(position, new Color(1f, 1f, 1f, 1f));
    }

    /// <summary>
    /// Deselects the tile at the given position and its range.
    /// </summary>
    public void DeselectTileWithRange(Vector3Int position)
    {
        DeselectTile(position);
         if (selectedRange.Count > 0) {
            foreach(KeyValuePair<Vector3Int, bool> pos in selectedRange) {
                rangeSelectionLayer.SetTile(pos.Key, null);
            }
        }
        selectedRange.Clear();
    }

    /// <summary>
    /// Selects a tile and all tiles in the placeable's range.
    /// </summary>
    public void SelectTileWithRange(Vector3Int position) {
        SelectTile(position);
        SelectRange(position);
    }

    /// <summary>
    /// Selects the tile at the given position.
    /// </summary>
    public void SelectTile(Vector3Int position)
    {
        selectionLayer.SetTile(position, selectedTile);
        topLayer.SetTileFlags(position, TileFlags.None);
        topLayer.SetColor(position, new Color(1f, 1f, 1f, 0.75f));
    }

    /// <summary>
    /// Select a range from the given position and the given range.
    /// </summary>
    public void SelectRange(Vector3Int position, Vector2Int range) {
        DeselectTileWithRange(position);
        for (int i = position.x - range.x; i < position.x + range.x; i++) {
            for (int j = position.y - range.y; j < position.y + range.y; j++) {
                Vector3Int pos = new Vector3Int(i, j, 0);
                if (GetTileInBottomLayer(pos)) {
                    rangeSelectionLayer.SetTile(pos, selectedTile);
                    rangeSelectionLayer.SetColor(pos, new Color(1f, 1f, 1f, 0.5f));
                    selectedRange.Add(pos, true);
                }
            }
        }
    }

    /// <summary>
    /// Selects a range if there is a placeable at the given positions. The placeable's range is used.
    /// </summary>
    public void SelectRange(Vector3Int position) {
        if (tiles.TryGetValue(position, out Placeable placeable)) {
            SelectRange(position, placeable.range);
        }
    }

    public Tile GetTileInTopLayer(Vector3Int position)
    {
        Vector3Int coords = new Vector3Int(position.x, position.y, 0);
        return topLayer.GetTile<Tile>(coords);
    }

    public TileType GetTileType(Vector3Int position) {
        Placeable placeable;
        if (tiles.TryGetValue(position, out placeable)) {
            return placeable.type;
        }
        return TileType.NONE;
    }

    public Tile GetTileInBottomLayer(Vector3Int position)
    {
        return bottomLayer.GetTile<Tile>(position);
    }

    public Vector3Int ConvertWorldToCell(Vector3 world)
    {
        return bottomLayer.WorldToCell(world);
    }

    public Vector2 ConvertCellToWorld(Vector3Int world)
    {
        return new Vector2(bottomLayer.CellToWorld(world).x, bottomLayer.CellToWorld(world).y);
    }

    public Vector3 ConvertCellToWorld(Vector2Int coords)
    {
        Vector3Int coords3 = new Vector3Int(coords.x, coords.y, 0);
        return bottomLayer.CellToWorld(coords3);
    }

    public Vector2 GetCellCenterWorldTop(Vector3Int gridCoords)
    {
        return topLayer.GetCellCenterWorld(gridCoords);
    }

    public bool IsTileAvailable(Vector2Int position)
    {
        Vector3Int coords = new Vector3Int(position.x, position.y, 0);
//        Player player = PlayerManager.instance.currentPlayer;
        
      //  bool isOwn = player.position.x - 25 <= position.x && player.position.x + 25 >= position.x && player.position.y - 25 <= position.y && player.position.y + 25 >= position.y;
        return topLayer.GetTile<Tile>(coords) == null && bottomLayer.GetTile<Tile>(coords) != null;
    }

    public TileType GetBuildingTypeOnTile(Vector2Int position)
    {
        Vector3Int coords = new Vector3Int(position.x, position.y, 0);
        CustomTile tile = topLayer.GetTile<CustomTile>(coords);
        if (tile != null)
        {
            return tile.type;
        }
        return TileType.NONE;
    }

}

