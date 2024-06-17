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
    public List<String> logs;
    public LandscapeData landscape;
    public Transform start;
    public Transform end;
}


