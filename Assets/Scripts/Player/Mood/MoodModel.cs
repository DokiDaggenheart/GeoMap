using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoodModel
{
    public MoodState moodState;
    public int sadMultiplier;
    public int neutralMultiplier;
    public int happyMultiplier;
}

public enum MoodState
{
    sad,
    neutral,
    happy
}
