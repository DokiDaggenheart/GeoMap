using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class WeatherController
{
    [Inject] private WeatherModel _weatherModel;

    public float WeatherTemperatureCoefficient()
    {
        switch (_weatherModel.weatherState)
        {
            case Weather.rainy:
                return _weatherModel.rainTemperatureCoef;
            case Weather.sunny:
                return _weatherModel.sunTemperatureCoef;
            case Weather.cloudy:
                return _weatherModel.cloudTemperatureCoef;
            default:
                return 0f;
        }
    }

    public float WeatherMoodCoefficient()
    {
        switch (_weatherModel.weatherState)
        {
            case Weather.rainy:
                return _weatherModel.rainMoodCoef;
            case Weather.sunny:
                return _weatherModel.sunMoodCoef;
            case Weather.cloudy:
                return _weatherModel.cloudMoodCoef;
            default:
                return 0f;
        }
    }
}
