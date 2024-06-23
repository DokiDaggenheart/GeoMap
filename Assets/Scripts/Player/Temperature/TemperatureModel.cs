using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemperatureModel
{
    public TemperatureState temperatureState;
    public float currentTemperature;
    public float coldMultiplier;
    public float coldCondition;
    public float normalMultiplier;
    public float normalCondition;
    public float hotMultiplier;
    public float minTemperature;
    public float maxTemperature;

    public void UpdateTemperatureState()
    {
        if (currentTemperature <= coldCondition)
        {
            temperatureState = TemperatureState.cold;
        }
        else if (currentTemperature > coldCondition && currentTemperature <= normalCondition)
        {
            temperatureState = TemperatureState.normal;
        }
        else if (currentTemperature > normalCondition)
        {
            temperatureState = TemperatureState.hot;
        }
    }
}

public enum TemperatureState { hot, normal, cold}
