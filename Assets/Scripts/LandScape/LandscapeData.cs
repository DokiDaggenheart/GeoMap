using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Landscape", menuName = "ScriptableObjects/NewLandscape")]
[System.Serializable]
public class LandscapeData : ScriptableObject
{
    public LandscapeType landscapeType;
    public float temperatureCoefficient;
    public List<string> landscapeLogs;
    public float landscapeUpMultiplier;
    public float landscapeStraightMultiplier;
    public float landscapeDownMultiplier;

}

public enum LandscapeType { plane, hills, foothills, desert, forest, swamp }
