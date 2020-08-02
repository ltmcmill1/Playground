using Assets.Scripts.src;
using Assets.Scripts.src.WorldGeneration;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[ExecuteInEditMode]
public class WorldGenerationManager : MonoBehaviour
{
    public List<EnvironmentalObjectFactory> objectFactories;
    public int xScale;
    public int zScale;
    public float quadDensity;
    public int heightMultiplier;
    public float smoothness;
    public int seed;
    public bool generateTerrain;


    private SortedDictionary<WorldParam, PerlinNoiseLayer> worldParamNoiseLayers;

    private System.Random terrainRandomGenerator;
    private Vector2 terrainSeed;
    private Vector2Int perlinSeedRange = new Vector2Int(-100000, 100000);

    // Update is called once per frame
    void Update()
    {
        if (generateTerrain)
        {
            DestroyTerrain();
            GenerateTerrain();
        }
    }

    private void GenerateTerrain()
    {
        Mesh mesh = gameObject.GetComponent<MeshFilter>().sharedMesh;
        if (mesh == null)
        {
            UnityEngine.Debug.LogError("Terrain MeshFilter is not set, cannot generate world.");
            return;
        }

        mesh.Clear();

        int xVertexCount = Mathf.RoundToInt(xScale * quadDensity) + 1;
        int zVertexCount = Mathf.RoundToInt(zScale * quadDensity) + 1;
        int vertexCount = xVertexCount * zVertexCount;

        terrainRandomGenerator = new System.Random(seed);
        terrainSeed = new Vector2(terrainRandomGenerator.Next(perlinSeedRange.x, perlinSeedRange.y), terrainRandomGenerator.Next(perlinSeedRange.x, perlinSeedRange.y));

        Vector3[] newVertices = new Vector3[vertexCount];
        Vector3 startPosition = gameObject.transform.position;
        startPosition.x -= xScale / 2;
        startPosition.z -= zScale / 2;
        float densityRandom;

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

                densityRandom = terrainRandomGenerator.Next(0, 100) / 100f;

                // Generate this point's affinity values
                WorldParamAffinities currentWorldStateAffinities = new WorldParamAffinities();
                foreach(KeyValuePair<WorldParam, PerlinNoiseLayer> paramAffinity in worldParamNoiseLayers)
                {
                    currentWorldStateAffinities.AddAffinity(paramAffinity.Key, paramAffinity.Value.Next());
                }

                // Poll each factory
                EnvironmentalObjectFactory winner = null;
                float highestAffinity = 0;
                foreach(EnvironmentalObjectFactory factory in objectFactories)
                {
                    float result = factory.AffinityForWorldState(densityRandom, currentWorldStateAffinities);
                    if(result > highestAffinity)
                    {
                        highestAffinity = result;
                        winner = factory;
                    }
                }

                // Place the winner
                if(winner != null)
                {
                    GameObject newObject = Instantiate(winner.CreateEnvironmentalObject(), newVertices[thisIndex], Quaternion.Euler(0f, 0f, 0f));
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

    private void DestroyTerrain()
    {
        GameObject[] generatedObjects = GameObject.FindGameObjectsWithTag("Generated");
        for (int i = 0; i < generatedObjects.Length; i++)
        {
            DestroyImmediate(generatedObjects[i]);
        }
    }
}
