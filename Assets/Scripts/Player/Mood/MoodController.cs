using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MoodController : ITickable
{
    [Inject] TemperatureController _temperatureController;
    [Inject] MoodModel _moodModel;
    [Inject] private WeatherController _weatherController;
    private float timer = 0f;
    private const float tickInterval = 1f;
    void ITickable.Tick()
    {
        timer += Time.deltaTime;
        if (timer >= tickInterval)
        {
            _moodModel.currentMood += _temperatureController.TemperatureInfluence();
            _moodModel.currentMood += _weatherController.WeatherMoodCoefficient();
            _moodModel.currentMood = Mathf.Clamp(_moodModel.currentMood, 0, 100);
            _moodModel.UpdateMoodState();
            timer = 0f;
        }
    }

    public float MoodMultiplier()
    {
        float multiplier = 100;
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
