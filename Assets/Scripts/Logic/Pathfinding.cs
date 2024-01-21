using Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    private static TilemapManager tilemap;
    private class MapTile : IComparable<MapTile>
    {
        public readonly Vector2Int position;
        public readonly TileType type;
        private int g;
        public int f => g + H;

        public int G { get; set; }

        public int H { get; set; }

        public MapTile parent;

        public MapTile(Vector2Int position, int g, int h, TileType type = TileType.NONE)
        {
            this.position = position;
            this.type = type;
            this.g = g;
            this.H = h;
        }

        public int CompareTo(MapTile other) 
        { 
            if (f > other.f)
            {
                return -1;
            }
            if (f == other.f)
            {
                return 0;
            }
            return 1;
        }

        public bool Equals(MapTile other) 
        {
            return position.x == other.position.x && position.y == other.position.y;
        }

        public override int GetHashCode()
        {
            return 7 * position.x + 11 * position.y;
        }
    }

    public void Start() {
        tilemap = GetComponent<TilemapManager>();
    }
    public static bool PathToNextBuilding(Vector2Int from, out List<Vector2Int> path, TileType type)
    {
        List<MapTile> openList = new List<MapTile>();
        List<MapTile> closedList = new List<MapTile> ();

        Vector2Int mapMaxBounds = tilemap.GetMapBoundaries();
        Vector2Int mapMinBounds = tilemap.GetMapBoundariesMin();

        MapTile firstSquare = new MapTile(from, 0, 0);
        openList.Add(firstSquare);

        bool found = false;

        do
        {
            openList = openList.OrderBy(e => e.G).ToList();
            MapTile currentTile = openList.First();
            closedList.Add(currentTile);
            openList.Remove(currentTile);
            List<MapTile> neighbourTiles = new List<MapTile>();

            for (int delta = -1; delta < 2; delta += 2)
            {
                Vector2Int dxposition = new Vector2Int(currentTile.position.x + delta, currentTile.position.y);
                Vector2Int dyposition = new Vector2Int(currentTile.position.x, currentTile.position.y + delta);

                if (dxposition.x > mapMinBounds.x && dxposition.x < mapMaxBounds.x)
                {
                    MapTile tile = GetNeighbourTile(dxposition, currentTile, type);
                    if (tile != null)
                    {
                        neighbourTiles.Add(tile);
                    }    
                }
                if (dyposition.y > mapMinBounds.y && dyposition.y < mapMaxBounds.y)
                {
                    MapTile tile = GetNeighbourTile(dyposition, currentTile, type);
                    if (tile != null)
                    {
                        neighbourTiles.Add(tile);
                    }
                }
            }

            if (neighbourTiles.Any(s => s.type.Equals(type)))
            {
                MapTile targetTile = neighbourTiles.Find(s => s.type.Equals(type));
                targetTile.parent = currentTile;
                openList.Add(targetTile);
                found = true;
                break;
            }
            foreach (MapTile tile in neighbourTiles)
            {
                if (closedList.Any(s => s.Equals(tile)))
                {
                    continue;
                }

                MapTile openMatch = openList.SingleOrDefault(m => m.Equals(tile));
                if (openMatch == null)
                {
                    tile.parent = currentTile;
                    openList.Add(tile);
                }
                else
                {
                    int nextPathG = currentTile.G + 1;
                    if (nextPathG < openMatch.G)
                    {
                        int matchIndex = openList.IndexOf(openMatch);
                        openList[matchIndex].G = nextPathG;
                        openList[matchIndex].parent = currentTile;
                    }
                }
            }


        } while (openList.Count() != 0);

        path = new List<Vector2Int>();
        if (found)
        {
            MapTile lastTile = openList.Last();
            while (lastTile != null)
            {
                path.Add(lastTile.position);
                lastTile = lastTile.parent;
            }
            //path.RemoveAt(path.Count - 1);

            //Remove the "to" node

            path.RemoveAt(0);
            path.Reverse();
        }
        return found;
    }

    private static MapTile GetNeighbourTile(Vector2Int position, MapTile currentTile, TileType searchType)
    {
        return null;
        //Tile tile = TilemapManager.instance.GetTileInTopLayer(position);
        //if (tile != null)
        //{
        //    if (tile.type == searchType)
        //    {
        //        return new MapTile(position, currentTile.G + 1, 0, searchType);
        //    }
        //    return null;
        //}
        //Tile bottomTile = TilemapManager.instance.GetTileInBottomLayer(position);
        //if (bottomTile != null) 
        //{
        //    return new MapTile(position, currentTile.G + 1, 0);
        //}
        //return null;
    }
   
    public static bool GetPath(Vector2Int from, Vector2Int to, out List<Vector2Int> path)
    {
        Vector2Int mapMaxBounds = tilemap.GetMapBoundaries();
        Vector2Int maxMinBounds = tilemap.GetMapBoundariesMin();

        List<MapTile> openList = new List<MapTile>();
        List<MapTile> closedList = new List<MapTile>();

        MapTile firstSquare = new MapTile(from, 0, from.GetDeltaTo(to));
        openList.Add(firstSquare);

        bool found = false;

        do
        {
            openList = openList.OrderBy(e => e.f).ToList();
            MapTile currentPathSquare = openList.First();
            closedList.Add(currentPathSquare);
            openList.Remove(currentPathSquare);
            List<MapTile> neighborSquares = new List<MapTile>();

            for (int delta = -1; delta < 2; delta += 2)
            {
                Vector2Int dxposition = new Vector2Int(currentPathSquare.position.x + delta, currentPathSquare.position.y);
                Vector2Int dyposition = new Vector2Int(currentPathSquare.position.x, currentPathSquare.position.y + delta);

                if (dxposition.x > maxMinBounds.x && dxposition.x < mapMaxBounds.x && tilemap.IsTileAvailable(dxposition)) 
                {
                    MapTile mapTile = new MapTile(dxposition, currentPathSquare.G + 1, dxposition.GetDeltaTo(to));
                    neighborSquares.Add(mapTile);
                }
                if (dyposition.y > maxMinBounds.y && dyposition.y < mapMaxBounds.y && tilemap.IsTileAvailable(dyposition))
                {
                    MapTile mapTile = new MapTile(dyposition, currentPathSquare.G + 1, dyposition.GetDeltaTo(to));
                    neighborSquares.Add(mapTile);
                }
            }

            if (neighborSquares.Any(s => s.position.Equals(to)))
            {
                MapTile targetTile = neighborSquares.Single(s => s.position.Equals(to));
                targetTile.parent = currentPathSquare;
                openList.Add(targetTile);
                found = true;
                break;
            }

            foreach (MapTile tile in neighborSquares)
            {
                if (closedList.Any(s => s.Equals(tile)))
                {
                    continue;
                }

                MapTile openMatch = openList.SingleOrDefault(m =>m.Equals(tile));

                if (openMatch == null)
                {
                    tile.parent = currentPathSquare;
                    openList.Add(tile);
                }
                else
                {
                    int nextPathG = currentPathSquare.G + 1;
                    if (nextPathG < openMatch.G)
                    {
                        int matchIndex = openList.IndexOf(openMatch);
                        openList[matchIndex].G = nextPathG;
                        openList[matchIndex].parent = currentPathSquare;
                    }
                }
            }

        } while (openList.Count() != 0);

        path = new List<Vector2Int>();
        if (found)
        {
            MapTile lastTile = openList.Last();
            while (lastTile != null)
            {
                path.Add(lastTile.position);
                lastTile = lastTile.parent;
            }
            //path.RemoveAt(path.Count - 1);

            //Remove the "to" node

           // path.RemoveAt(0);

            path.Reverse();
        }
        return found;
    }
       
}
