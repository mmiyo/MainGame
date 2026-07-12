using System;
using System.Numerics;
using Unity.Mathematics;
using UnityEditor.PackageManager.UI;
using UnityEngine;

public class PlanetGen : MonoBehaviour
{
    private float width = 3;
    private float length = 3;

    [SerializeField] private float horizontalScale = 2;
    [SerializeField] private float verticalScale = 2;
    [SerializeField] private GameObject planetPrefab;

    void Start()
    {
        GeneratePlanets();
    }

    private void GeneratePlanets()
    {
        for (float x = 0; x < width; x++)
        {   
            for(float z = 0; z < length; z++)
            {   
                UnityEngine.Vector3 pos = new UnityEngine.Vector3(x * horizontalScale, noiseGeneration(x, z, UnityEngine.Random.Range(5f, 10f)) * verticalScale, z * horizontalScale);
                GameObject gen = Instantiate(planetPrefab, pos, UnityEngine.Quaternion.identity);

                gen.transform.SetParent(transform);

            }
    }
    }


    private float noiseGeneration(float xCoord, float zCoord, float noiseScale)
    {   
        float xNoise = (xCoord + transform.position.x) / noiseScale;
        float zNoise = (zCoord + transform.position.z) / noiseScale;

        float yCoord = Mathf.PerlinNoise(xNoise, zNoise);

        return yCoord;
    }
}
