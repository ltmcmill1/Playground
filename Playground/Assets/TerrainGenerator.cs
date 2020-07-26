using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TerrainGenerator : MonoBehaviour
{
	public float xScale;
	public float zScale;
	public float quadDensity;
	public float heightMultiplier;
	public float smoothness;
	public int seed;
	public GameObject tree;
	public GameObject mushroom;
	public bool generateTerrain;

	private Vector2 terrainSeed;
	private Vector2 forestSeed;
	private Vector2 perlinSeedRange = new Vector2(-100000, 100000);

	void Update()
    {
		if (generateTerrain)
		{
			DestroyTerrain();
			GenerateTerrain();
		}
    }

	void GenerateTerrain()
	{
		Mesh mesh = gameObject.GetComponent<MeshFilter>().sharedMesh;
		mesh.Clear();

		int xVertexCount = Mathf.RoundToInt(xScale * quadDensity) + 1;
		int zVertexCount = Mathf.RoundToInt(zScale * quadDensity) + 1;
		int vertexCount = xVertexCount * zVertexCount;

		Random.InitState(seed);
		terrainSeed = new Vector2(Random.Range(perlinSeedRange.x, perlinSeedRange.y), Random.Range(perlinSeedRange.x, perlinSeedRange.y));
		forestSeed = new Vector2(Random.Range(perlinSeedRange.x, perlinSeedRange.y), Random.Range(perlinSeedRange.x, perlinSeedRange.y));

		Vector3[] newVertices = new Vector3[vertexCount];
		Vector3 startPosition = gameObject.transform.position;
		startPosition.x -= xScale / 2;
		startPosition.z -= zScale / 2;
		float objectCreationRandom;

		int thisIndex;
		for (int i = 0; i < xVertexCount; i++)
		{
			for (int j = 0; j < zVertexCount; j++)
			{
				thisIndex = i * xVertexCount + j;
				newVertices[thisIndex] = startPosition;
				newVertices[thisIndex].x += i * (xScale / xVertexCount);
				newVertices[thisIndex].z += j * (zScale / zVertexCount);
				newVertices[thisIndex].y -= heightMultiplier * Mathf.PerlinNoise(smoothness * newVertices[thisIndex].x + terrainSeed.x, smoothness * newVertices[thisIndex].z + terrainSeed.y);
				if (Mathf.PerlinNoise(smoothness * newVertices[thisIndex].x + forestSeed.x, smoothness * newVertices[thisIndex].z + forestSeed.y) >= 0.5f)
				{
					objectCreationRandom = Random.Range(0, 1f);
					if (objectCreationRandom > 0.98f)
					{
						GameObject newTree = Instantiate(tree, newVertices[thisIndex], Quaternion.Euler(-90f, 0f, 0f));
						newTree.transform.localScale = newTree.transform.localScale * Random.Range(0.25f, 1.75f);
					}
					else if (objectCreationRandom > 0.95f)
					{
						GameObject newMushroom = Instantiate(mushroom, newVertices[thisIndex], Quaternion.Euler(-90f, 0f, 0f));
						newMushroom.transform.localScale = newMushroom.transform.localScale * Random.Range(0.75f, 3f);
					}
				}
			}
		}

		Vector2[] newUV = new Vector2[vertexCount];
		for (int i = 0; i < newUV.Length; i++)
		{
			newUV[i] = Vector2.zero;
		}

		int[] newTriangles = new int[3 * (xVertexCount - 1) * (zVertexCount - 1) * 2];

		int quadStart = 0;
		for (int i = 0; i < newTriangles.Length - 6; i += 6)
		{
			if (quadStart % zVertexCount == zVertexCount - 1)
			{
				quadStart++;
			}
			newTriangles[i] = quadStart;
			newTriangles[i + 1] = quadStart + 1;
			newTriangles[i + 2] = quadStart + zVertexCount;
			newTriangles[i + 3] = quadStart + 1;
			newTriangles[i + 4] = quadStart + zVertexCount + 1;
			newTriangles[i + 5] = quadStart + zVertexCount;

			quadStart++;
		}

		mesh.vertices = newVertices;
		mesh.uv = newUV;
		mesh.triangles = newTriangles;
		mesh.RecalculateNormals();
		gameObject.GetComponent<MeshCollider>().sharedMesh = mesh;
	}

	void DestroyTerrain()
	{
		GameObject[] generatedObjects = GameObject.FindGameObjectsWithTag("Generated");
		for (int i = 0; i < generatedObjects.Length; i++)
		{
			DestroyImmediate(generatedObjects[i]);
		}
	}
}
