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

    private void Update()
    {
        time += Time.deltaTime / _timeController.timeScale;
        if(time >= 60)
        {
            AdjustTemperature(_weatherController.WeatherTemperatureCoefficient());
            AdjustTemperature(_timeController.GetCurrentTimeOfDayCoefficient());
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
}
