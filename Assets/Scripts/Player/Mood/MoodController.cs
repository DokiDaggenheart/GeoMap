using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MoodController
{
    [Inject] MoodModel _moodModel;

    public float MoodMultiplier()
    {
        float multiplier = 1;
        switch (_moodModel.moodState)
        {
            case MoodState.happy:
                multiplier = _moodModel.happyMultiplier;
                break;
            case MoodState.sad:
                multiplier = _moodModel.sadMultiplier;
                break;
            case MoodState.neutral:
                multiplier = _moodModel.neutralMultiplier;
                break;
        }

        return multiplier / 100;
    }
}
