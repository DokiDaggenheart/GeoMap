using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MovementController : MonoBehaviour
{
    [Inject] private PathModel _pathModel;
    [Inject] private MovementModel _movementModel;
    [Inject] private EnergyModel _energyModel;
    [Inject] private EnergyController _energyController;
    [Inject] private MoodController _moodController;
    [Inject] private TimeController _timeController;
    [Inject] private LogSystem _logSystem;
    [Inject] private WeatherModel _weatherModel;
    [Inject] private TemperatureModel _temperatureModel;

    private void Start()
    {
        _logSystem.SetLogList(_pathModel.pathList[pathSectionIndex]);
    }
    public int pathSectionIndex = 0;
    private void Update()
    {
        _movementModel.isRiding = _energyController.CheckEnergy();
        if (_movementModel.isRiding)
        {
            Debug.Log("total velocity is " + _movementModel.totalVelocity);
            GoToNextPoint();

            _movementModel.totalVelocity = _movementModel.velocity
                * _energyController.energyMultiplier()
                * CurrentLandscapeMultiplier(GetCurrentLandscape(), _pathModel.pathList[pathSectionIndex], _movementModel.progress);

            gameObject.transform.position = Vector2.Lerp(_pathModel.pathList[pathSectionIndex].start.position, _pathModel.pathList[pathSectionIndex].end.position, _movementModel.progress);
            _movementModel.progress += (_movementModel.totalVelocity / _pathModel.pathList[pathSectionIndex].length) * (Time.deltaTime*_timeController.timeScale / 3600);
            _energyModel.energy -= _energyModel.energyDiminution * _moodController.MoodMultiplier() * (Time.deltaTime*_timeController.timeScale / 60);

            if (_movementModel.progress < _pathModel.pathList[pathSectionIndex].firstWeatherLength)
                _weatherModel.ChangeWeather(_pathModel.pathList[pathSectionIndex].firstWeather);
            else
                _weatherModel.ChangeWeather(_pathModel.pathList[pathSectionIndex].secondWeather);
        }

        if (!_movementModel.isRiding)
        {
            _movementModel.totalVelocity = 0;
            _energyModel.energy += _energyModel.energyAddition * Time.deltaTime;
        }
    }

    public void GoToNextPoint()
    {
        if(_movementModel.progress >= 0.99)
        {
            Debug.Log("GoNext");
            pathSectionIndex += 1;
            _movementModel.progress = 0;
            _logSystem.SetLogList(_pathModel.pathList[pathSectionIndex]);
        }
    }
    public float CurrentLandscapeMultiplier(LandscapeData landscape, PathSection pathSection, float progress)
    {
        Direction currentDirection;
        if (progress < pathSection.firstDirectionLength)
            currentDirection = pathSection.firstDirection;
        else
            currentDirection = pathSection.secondDirection;
        switch (currentDirection)
        {
            case Direction.down:
                return landscape.landscapeDownMultiplier;
            case Direction.up:
                return landscape.landscapeUpMultiplier;
            case Direction.straight:
                return landscape.landscapeStraightMultiplier;
        }
        Console.WriteLine("LandcapeMultiplierError");
        return 0;
    }

    IEnumerator Rest(float restTime, float energyRecovery)
    {
        _movementModel.isRiding = false;
        _energyModel.energy = (energyRecovery / restTime) / _timeController.timeScale;
        yield return new WaitForSeconds((restTime * 60)/ _timeController.timeScale);
        _movementModel.isRiding = true;
    }
    public LandscapeData GetCurrentLandscape()
    {
        LandscapeData landscape;
        if (_movementModel.progress < _pathModel.pathList[pathSectionIndex].firstLandscapeLength)
            landscape = _pathModel.pathList[pathSectionIndex].firstLandscape;
        else
            landscape = _pathModel.pathList[pathSectionIndex].secondLandscape;

        return landscape;
    }
    public PathSection GetCurrentPathSection()
    {
        return _pathModel.pathList[pathSectionIndex];
    }

    public string GetCurrentSpeed()
    {
        string currentSpeed = _movementModel.totalVelocity + " km/h";
        return currentSpeed;
    }

    public string GetCurrentTemperature()
    {
        string currentTemperaure = _temperatureModel.currentTemperature + " °C";
        return currentTemperaure;
    }
}
