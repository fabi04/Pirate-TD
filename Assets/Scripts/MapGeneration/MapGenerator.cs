using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour {

    public List<Tile> groundTiles;
    public List<Tile> treeTile;
    public List<Tile> stoneTile;
	public List<Tile> bushTiles;

	public Tile pathTile;
    public int mapWidth;
	public int mapHeight;
	public float noiseScale;

	public int octaves;
	[Range(0,1)]
	public float persistance;
	public float lacunarity;

	public int seed;
	public Vector2 offset;

    public Vector2Int positionOffset;

    [Range(0, 1)]
    public float groundThreshold;

    [Range(0, 1)]
    public float treeThreshold;

    [Range(0, 1)]
    public float stoneThreshold;

	[Range(0, 1)]
    public float bushThreshold;

	public bool autoUpdate;

    public void Start() {
        GenerateMap();
    }

	public void GenerateMap() {
		float[,] noiseMap = Noise.GenerateNoiseMap (mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset);
        float[,] stoneNoiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale+ 1, octaves + 1, persistance + 1, lacunarity + 1, offset);
		float[,] bushNoiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed, noiseScale+ 2, octaves + 1, persistance + 1, lacunarity + 1, offset);
		TilemapManager tilemapManager = GetComponent<TilemapManager>();
        for (int y = 0; y < mapHeight; y++) {
			for (int x = 0; x < mapWidth; x++) {
				float currentHeight = noiseMap [x, y];
				Vector3Int position = new Vector3Int(positionOffset.x + x, positionOffset.y + y, 0);
				if (currentHeight >= groundThreshold) {
                    tilemapManager.AddTileToBottomLayer(position, groundTiles[UnityEngine.Random.Range(0, groundTiles.Count)]);
                }
                if (currentHeight >= treeThreshold) {
                    tilemapManager.AddTile(position, treeTile[UnityEngine.Random.Range(0, treeTile.Count)], TileType.TREE);
                } else if( stoneNoiseMap[x,y] >= stoneThreshold && currentHeight >= groundThreshold) {
                    tilemapManager.AddTile(position, stoneTile[UnityEngine.Random.Range(0, stoneTile.Count)], TileType.STONE);
                } else if (bushNoiseMap[x, y] >= bushThreshold && currentHeight >= groundThreshold) {
					tilemapManager.AddTile(position, bushTiles[UnityEngine.Random.Range(0, bushTiles.Count)], TileType.BUSH);
				}
			}
		}
        tilemapManager.AddTile(new Vector3Int(0, 0, 0),pathTile, TileType.PATH);
	}

	void OnValidate() {
		if (mapWidth < 1) {
			mapWidth = 1;
		}
		if (mapHeight < 1) {
			mapHeight = 1;
		}
		if (lacunarity < 1) {
			lacunarity = 1;
		}
		if (octaves < 0) {
			octaves = 0;
		}
	}
}