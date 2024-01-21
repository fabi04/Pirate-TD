using UnityEngine;
using System.Collections;

public class Noise {

	/// <summary>
	/// Generates a noise map using the given parameters.
	/// </summary>
	/// <param name="mapWidth"> The map's width</param>
	/// <param name="mapHeight"> The map's height</param>
	/// <param name="seed"> The random seed</param>
	/// <param name="scale"> Scale value to scale the noise</param>
	/// <returns>An array containing the noise map.</returns>
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset) {
		float[,] noiseMap = new float[mapWidth,mapHeight];

		System.Random prng = new System.Random (seed);
		Vector2[] octaveOffsets = new Vector2[octaves];
		for (int i = 0; i < octaves; i++) {
			float offsetX = prng.Next (-100000, 100000) + offset.x;
			float offsetY = prng.Next (-100000, 100000) + offset.y;
			octaveOffsets [i] = new Vector2 (offsetX, offsetY);
		}

		if (scale <= 0) {
			scale = 0.0001f;
		}

		float maxNoiseHeight = float.MinValue;
		float minNoiseHeight = float.MaxValue;

		float halfWidth = mapWidth / 2f;
		float halfHeight = mapHeight / 2f;


		for (int y = 0; y < mapHeight; y++) {
			for (int x = 0; x < mapWidth; x++) {
		
				float amplitude = 1;
				float frequency = 1;
				float noiseHeight = 0;

				for (int i = 0; i < octaves; i++) {
					float sampleX = (x-halfWidth) / scale * frequency + octaveOffsets[i].x;
					float sampleY = (y-halfHeight) / scale * frequency + octaveOffsets[i].y;

					float perlinValue = Mathf.PerlinNoise (sampleX, sampleY) * 2 - 1;
					noiseHeight += perlinValue * amplitude;

					amplitude *= persistance;
					frequency *= lacunarity;
				}

				if (noiseHeight > maxNoiseHeight) {
					maxNoiseHeight = noiseHeight;
				} else if (noiseHeight < minNoiseHeight) {
					minNoiseHeight = noiseHeight;
				}
				noiseMap [x, y] = noiseHeight;
			}
		}

        float[] falloff = GenerateFalloff(mapWidth, mapHeight, 2, 3);

		for (int y = 0; y < mapHeight; y++) {
			for (int x = 0; x < mapWidth; x++) {
				noiseMap [x, y] = Mathf.InverseLerp (minNoiseHeight, maxNoiseHeight, noiseMap [x, y]);
                noiseMap [x, y] = Mathf.Clamp01(noiseMap[x, y] - falloff[x * mapWidth + y] );
			}
		}

		return noiseMap;
	}


    private static float[] GenerateFalloff(int width, int length, float a, float b)
	{
		float[] map = new float[width * length];

		for (int index = 0, z = 0; z < length; z++)
		{
			for (int x = 0; x < width; x++, index++)
			{
				float xValue = (x / (float)width * 2) - 1;
				float yValue = (z / (float)length * 2) - 1;

				float value = Mathf.Max(Mathf.Abs(xValue), Mathf.Abs(yValue));
				map[index] = Mathf.Pow(value, a) / (Mathf.Pow(value, a) + Mathf.Pow(b - b * value, a));
			}
		}
		return map;
	}

}