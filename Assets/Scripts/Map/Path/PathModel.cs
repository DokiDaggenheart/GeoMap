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
    public Direction direction;
    public int length;
    [Header("Weather")]
    public Weather firstWeather;
    [Range(0.0f, 1.0f)]
    public float firstWeatherLength;
    public Weather secondWeather;
}

[System.Serializable]
public struct Log
{
    public string Sad;
    public string Neutral;
    public string Happy;
}
public enum Direction { up, down, straight}

