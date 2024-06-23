using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TemperatureController : MonoBehaviour
{
    private float time;
    [Inject] TemperatureModel _temperatureModel;
    [Inject] TimeController _timeController;
    [Inject] WeatherController _weatherController;
    [Inject] MovementController _movementController;

    private void Update()
    {
        time += Time.deltaTime * _timeController.timeScale;
        if (time >= 60)
        {

            AdjustTemperature(_weatherController.WeatherTemperatureCoefficient() * GetCurrentLandscapeCoefficient());
            AdjustTemperature(_timeController.GetCurrentTimeOfDayCoefficient() * GetCurrentLandscapeCoefficient());
            _temperatureModel.currentTemperature = Mathf.Clamp(_temperatureModel.currentTemperature, -5, 40);
            time = 0.0f;
        }
    }
    public void AdjustTemperature(float temperatureChange)
    {
        _temperatureModel.currentTemperature += temperatureChange;
        _temperatureModel.currentTemperature = Mathf.Clamp(_temperatureModel.currentTemperature, _temperatureModel.minTemperature, _temperatureModel.maxTemperature);
        _temperatureModel.UpdateTemperatureState();
    }

    public float TemperatureInfluence()
    {
        switch (_temperatureModel.temperatureState)
        {
            case TemperatureState.cold:
                return _temperatureModel.coldMultiplier;
            case TemperatureState.normal:
                return _temperatureModel.normalMultiplier;
            case TemperatureState.hot:
                return _temperatureModel.hotMultiplier;
        }
        return 0;
    }

    private float GetCurrentLandscapeCoefficient()
    {
        return _movementController.GetCurrentPathSection().landscape.temperatureCoefficient;
    }
}
