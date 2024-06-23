using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PathModel
{
    public float spacingLength;
    public GameObject dotPrefab;
    public List<PathSection> pathList;
}

[System.Serializable]
public struct PathSection
{
    public string name;
    public List<Log> logs;
    public LandscapeData landscape;
    public Transform start;
    public Transform end;
    public int length;
    [Header("Weather")]
    public Weather firstWeather;
    [Range(0.0f, 1.0f)]
    public float firstWeatherLength;
    public Weather secondWeather;
    [Header("Direction")]
    public Direction firstDirection;
    [Range(0.0f, 1.0f)]
    public float firstDirectionLength;
    public Direction secondDirection;
}

[System.Serializable]
public struct Log
{
    public bool isImporntant;
    public string Sad;
    public string Neutral;
    public string Happy;
}
public enum Direction { up, down, straight}

