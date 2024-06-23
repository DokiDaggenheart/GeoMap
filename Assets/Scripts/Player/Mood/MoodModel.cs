using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoodModel
{
    public MoodState moodState;
    [Range(0,100)]
    
    public float currentMood = 100;
    public int sadMultiplier;
    public int sadCondition;
    public int neutralMultiplier;
    public int neutralCondition;
    public int happyMultiplier;

    public void UpdateMoodState()
    {
        if (currentMood <= sadCondition)
        {
            moodState = MoodState.sad;
        }
        else if (currentMood > sadCondition && currentMood <= neutralCondition)
        {
            moodState = MoodState.neutral;
        }
        else if (currentMood > neutralCondition)
        {
            moodState = MoodState.happy;
        }
    }
}

public enum MoodState
{
    sad,
    neutral,
    happy
}
