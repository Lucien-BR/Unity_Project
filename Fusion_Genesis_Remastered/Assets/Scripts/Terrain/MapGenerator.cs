﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int mapWidth;
    public int mapHeight;
    public float noiseScale;

    public int octaves;
    public float persistance;
    public float lacunarity;

    public void GenerateMap() {
        float[,] noiseMap = Noise.generateNoiseMap(mapWidth, mapHeight, noiseScale, octaves, persistance, lacunarity);

        MapDisplay display = FindObjectOfType<MapDisplay>();

        display.DrawNoiseMap(noiseMap);
    }
}