using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MoodController : MonoBehaviour
{
    [Inject] private TemperatureController _temperatureController;
    [Inject] private MoodModel _moodModel;
    [Inject] private WeatherController _weatherController;
    [Inject] private TimeController _timeController;
    private float timer = 0f;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 60.0f / _timeController.timeScale)
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
        _moodModel.UpdateMoodState();
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
        multiplier = multiplier / 100;
        return multiplier;
    }


}
