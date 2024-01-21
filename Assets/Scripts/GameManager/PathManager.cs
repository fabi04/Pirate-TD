using System;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Manages the retrieval of correct path tiles.
/// </summary>
public class PathManager : MonoBehaviour {
    public List<Tile> pathTiles;

    /// <summary>
    /// Returns the correctly oriented path tile given the neighbours.
    /// </summary>
    /// <param name="neighbours">The neighbours of the tile are needed to calculate the orientation of the tile</param>
    /// <returns></returns>
    public Tile GetPathForNeighbours(int[] neighbours) {
        if (Assert(neighbours, 0, 0, 0, 0)) {
            return pathTiles[0];
        } else if (Assert(neighbours, 0, 0, 0, 1)) {
            return pathTiles[1];
        }else if (Assert(neighbours, 0, 0, 1, 0)) {
            return pathTiles[2];
        }else if (Assert(neighbours, 0, 0, 1, 1)) {
            return pathTiles[3];
        }else if (Assert(neighbours, 0, 1, 0, 0)) {
            return pathTiles[4];
        }else if (Assert(neighbours, 0, 1, 0, 1)) {
            return pathTiles[5];
        }else if (Assert(neighbours, 0, 1, 1, 0)) {
            return pathTiles[6];
        }else if (Assert(neighbours, 0, 1, 1, 1)) {
            return pathTiles[7];
        }else if (Assert(neighbours, 1, 0, 0, 0)) {
            return pathTiles[8];
        }else if (Assert(neighbours, 1, 0, 0, 1)) {
            return pathTiles[9];
        }else if (Assert(neighbours, 1, 0, 1, 0)) {
            return pathTiles[10];
        }else if (Assert(neighbours, 1, 0, 1, 1)) {
            return pathTiles[11];
        }else if (Assert(neighbours, 1, 1, 0, 0)) {
            return pathTiles[12];
        }else if (Assert(neighbours, 1, 1, 0, 1)) {
            return pathTiles[13];
        }else if (Assert(neighbours, 1, 1, 1, 0)) {
            return pathTiles[14];
        }else if (Assert(neighbours, 1, 1, 1, 1)) {
            return pathTiles[15];
        }
        return null;
    }
    
    /// <summary>
    /// Returns the position for a neighbour.
    /// </summary>
    /// <param name="currentPosition"> The position of the tile whose neighbour is to be found.</param>
    /// <param name="neighbour"> The array specifying the neighbour direction.</param>
    /// <returns>The calculated position or the currentPosition if the input is invalid.</returns>
    public Vector3Int GetPositionForNeighbour(Vector3Int currentPosition, int[] neighbour) {
        if (Assert(neighbour, 0, 0, 0, 1)) {
            return new Vector3Int(currentPosition.x + 1, currentPosition.y, 0);
        } else if (Assert(neighbour, 0, 0, 1, 0)) {
            return new Vector3Int(currentPosition.x - 1, currentPosition.y, 0);
        } else if (Assert(neighbour, 0, 1, 0, 0)) {
            return new Vector3Int(currentPosition.x, currentPosition.y + 1, 0);
        } else if (Assert(neighbour, 1, 0, 0, 0)) {
            return new Vector3Int(currentPosition.x, currentPosition.y - 1, 0);
        }
        return currentPosition;
    }

    private bool Assert(int[] neighbours, int i1, int i2, int i3, int i4) {
        return (neighbours[0] == i1 && neighbours[1] == i2 && neighbours[2] == i3 && neighbours[3] == i4);
    }
}