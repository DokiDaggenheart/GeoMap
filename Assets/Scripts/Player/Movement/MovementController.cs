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

    [SerializeField] private GameObject RestButton;
    [SerializeField] private GameObject restPanel;


    public AudioSource musicSource;
    public AudioClip[] musicClips;


    public Animator roadAnimator;
    private float distanceTraveled;
    public int pathSectionIndex = 0;
    private int pathAnimationIndex = 0;
    private int roadLandcsapeSection = 1;
    private bool isRestButtonActive;

    private void Start()
    {
        _logSystem.SetLogList(_pathModel.pathList[pathSectionIndex]);
        _movementModel.isRiding = true;
        ChangeMusic(0);
    }
    private void Update()
    {
        _energyController.CheckEnergy();

        if (_energyModel.energy < 20)
            isRestButtonActive = true;
        else
            isRestButtonActive = false;

        if (isRestButtonActive)
            RestButton.GetComponent<Image>().color = Color.green;
        else
            RestButton.GetComponent<Image>().color = Color.grey;

        if (_movementModel.isRiding)
        {
            GoToNextPoint();

            _movementModel.totalVelocity = _movementModel.velocity
                * _energyController.energyMultiplier()
                * CurrentLandscapeMultiplier(GetCurrentLandscape(), _pathModel.pathList[pathSectionIndex], _movementModel.progress);

            gameObject.transform.position = Vector2.Lerp(_pathModel.pathList[pathSectionIndex].start.position, _pathModel.pathList[pathSectionIndex].end.position, _movementModel.progress);
            _movementModel.progress += (_movementModel.totalVelocity / _pathModel.pathList[pathSectionIndex].length) * (Time.deltaTime * _timeController.timeScale / 3600);
            _energyModel.energy -= _energyModel.energyDiminution * _moodController.MoodMultiplier() * (Time.deltaTime * _timeController.timeScale / 60);

            distanceTraveled += _movementModel.totalVelocity * (Time.deltaTime * _timeController.timeScale / 3600);

            if (_movementModel.progress < _pathModel.pathList[pathSectionIndex].firstWeatherLength)
                _weatherModel.ChangeWeather(_pathModel.pathList[pathSectionIndex].firstWeather);
            else
                _weatherModel.ChangeWeather(_pathModel.pathList[pathSectionIndex].secondWeather);

            if (_movementModel.progress < _pathModel.pathList[pathSectionIndex].firstLandscapeLength && roadLandcsapeSection != 2)
            {
                roadLandcsapeSection = 2;
                ChangeRoadAnimation();
            }
            else if (_movementModel.progress >= _pathModel.pathList[pathSectionIndex].firstLandscapeLength && roadLandcsapeSection != 1)
            {
                roadLandcsapeSection = 1;
                ChangeRoadAnimation();
            }
        }

        if (!_movementModel.isRiding)
        {
            _movementModel.totalVelocity = 0;
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
            ChangeMusic(pathSectionIndex);
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
        int roundedSpeed = (int)Math.Floor(_movementModel.totalVelocity);
        string currentSpeed =  roundedSpeed + " km/h";
        Debug.Log("current speed" + currentSpeed);
        return currentSpeed;
    }

    public string GetCurrentTemperature()
    {
        string currentTemperaure = Mathf.Round(_temperatureModel.currentTemperature * 10) / 10 + " °C";
        return currentTemperaure;
    }

    public string GetCurrentNameOfSpace()
    {
        string currentNameOfSpace = _pathModel.pathList[pathSectionIndex].name;
        return currentNameOfSpace;
    }
    
    public string GetCurrentLandscapeName()
    {
        string currentLandscapeName = GetCurrentLandscape().name;
        return currentLandscapeName;
    }

    public string GetCurrentDirection()
    {
        string direction;
        if (_movementModel.progress < _pathModel.pathList[pathSectionIndex].firstDirectionLength)
            direction = _pathModel.pathList[pathSectionIndex].firstDirection.ToString();
        else
            direction = _pathModel.pathList[pathSectionIndex].secondDirection.ToString(); ;

        return direction;
    }

    public string GetCurrentMood()
    {
        string currentMood = _moodController.GetCurrentMoodState().ToString();
        return currentMood;
    }

    public string GetTravelledDistance()
    {
        float distance = Mathf.Round(distanceTraveled * 10) / 10;
        string travelledDistance = distance + " km";
        return travelledDistance;
    }

    private void ChangeRoadAnimation()
    {
        pathAnimationIndex++;
        roadAnimator.SetInteger("pathSectionIndex", pathAnimationIndex);

        Debug.Log($"Animation changed to {pathAnimationIndex}");
    }

    public void OpenRestPanel()
    {
        if(_energyModel.energy < 20)
        {
            _logSystem.logWindowIsVisible = true;
            _logSystem.LogWindowVisibilityChanging();
            restPanel.SetActive(true);
            _movementModel.isRiding = false;
        }
    }

    public void ChangeMusic(int index)
    {
        musicSource.clip = musicClips[index];
        musicSource.Play();
    }
}
