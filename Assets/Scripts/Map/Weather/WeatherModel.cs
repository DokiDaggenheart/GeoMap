using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherModel
{
    public Weather weatherState;
    public float rainTemperatureCoef;
    public float sunTemperatureCoef;
    public float cloudTemperatureCoef;
    public float rainMoodCoef;
    public float sunMoodCoef;
    public float cloudMoodCoef;

    public void ChangeWeather(Weather newWeather)
    {
        weatherState = newWeather;
    }
}

public enum Weather { rainy, sunny, cloudy }
