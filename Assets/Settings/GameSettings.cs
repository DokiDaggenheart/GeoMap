using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameSettings : MonoBehaviour
{
    [Inject] private PathModel _pathModel;
    [Inject] private MovementModel _movementModel;
    [Inject] private MoodModel _moodModel;
    [Inject] private EnergyModel _energyModel;
    [Inject] private TimeModel _timeModel;
    [Inject] private WeatherModel _weatherModel;
    [Inject] private TemperatureModel _temperatureModel;

    [Header("PathSettings")]
    [SerializeField] private List<PathSection> _pathList;
    [SerializeField] private float _spacingLength;
    [SerializeField] private GameObject _dotPrefab;

    [Header("EnergySettings")]
    [SerializeField] private float _maxEnergy;
    [SerializeField] private float _velocity;
    [Tooltip("При восстановлении энергии во время игры на этой отметке игрок продолжит движение")]
    [SerializeField]
    private int _startRidingEnergy;

    [Header("EnergyStatesSettings")]
    [Tooltip("Нижний порог первого состояния (энергия)")]
    [SerializeField] private int _firstStateCondition;
    [Tooltip("Прибавка к скорости во время первого состояния")]
    [SerializeField] private int _firstStateVelocity;
    [SerializeField] private int _secondStateCondition;
    [SerializeField] private int _secondStateVelocity;
    [SerializeField] private int _thirdStateCondition;
    [SerializeField] private int _thirdStateVelocity;
    [SerializeField] private int _fourthStateVelocity;
    [SerializeField] private float _energyDiminution;
    [SerializeField] private int _energyAddition;

    [Header("MoodSettings")]
    [SerializeField] private int _happyMultiplier;
    [SerializeField] private int _sadMultiplier;
    [SerializeField] private int _neutralMultiplier;
    [SerializeField] private int _sadCondition;
    [SerializeField] private int _neutralCondition;

    [Header("Time")]
    [SerializeField] private int _timeInGameOn;
    [SerializeField] private int _timeInRealLifeOn;
    [SerializeField] private int _timeInGameOff;
    [SerializeField] private int _timeInRealLifeOff;

    [Header("WeatherSettings")]
    [SerializeField] private float _rainTemperatureCoef;
    [SerializeField] private float _sunTemperatureCoef;
    [SerializeField] private float _cloudTemperatureCoef;
    [SerializeField] private float _rainMoodCoef;
    [SerializeField] private float _sunMoodCoef;
    [SerializeField] private float _cloudMoodCoef;

    [Header("TimeOfDaySettings")]
    [SerializeField] private float _nightCoefficient;
    [SerializeField] private float _morningCoefficient;
    [SerializeField] private float _dayCoefficient;
    [SerializeField] private float _eveningCoefficient;

    [Header("TemperatureSettings")]
    [SerializeField] private TemperatureState _temperatureState;
    [SerializeField] private float _currentTemperature;
    [SerializeField] private float _coldMultiplier;
    [SerializeField] private float _coldCondition;
    [SerializeField] private float _normalMultiplier;
    [SerializeField] private float _normalCondition;
    [SerializeField] private float _hotMultiplier;
    [SerializeField] private float _minTemperature;
    [SerializeField] private float _maxTemperature;
    private void Awake()
    {
        // PathModel settings
        _pathModel.pathList = _pathList;
        _pathModel.spacingLength = _spacingLength;
        _pathModel.dotPrefab = _dotPrefab;

        // EnergyModel settings
        _energyModel.maxEnergy = _maxEnergy;
        _energyModel.energy = _energyModel.maxEnergy;
        _movementModel.velocity = _velocity;
        _energyModel.startRidingEnergy = _startRidingEnergy;
        _energyModel.firstStateCondition = _firstStateCondition;
        _energyModel.firstStateVelocity = _firstStateVelocity;
        _energyModel.secondStateCondition = _secondStateCondition;
        _energyModel.secondStateVelocity = _secondStateVelocity;
        _energyModel.thirdStateCondition = _thirdStateCondition;
        _energyModel.thirdStateVelocity = _thirdStateVelocity;
        _energyModel.fourthStateVelocity = _fourthStateVelocity;
        _energyModel.energyAddition = _energyAddition;
        _energyModel.energyDiminution = _energyDiminution;

        // MoodModel settings
        _moodModel.happyMultiplier = _happyMultiplier;
        _moodModel.sadMultiplier = _sadMultiplier;
        _moodModel.neutralMultiplier = _neutralMultiplier;
        _moodModel.neutralCondition = _neutralCondition;
        _moodModel.sadCondition = _sadCondition;

        // TimeModel settings
        _timeModel.timeInGameOn = _timeInGameOn;
        _timeModel.timeInGameOff = _timeInGameOff;
        _timeModel.timeInRealLifeOn = _timeInRealLifeOn;
        _timeModel.timeInRealLifeOff = _timeInRealLifeOff;
        _timeModel.nightCoefficient = _nightCoefficient;
        _timeModel.morningCoefficient = _morningCoefficient;
        _timeModel.dayCoefficient = _dayCoefficient;
        _timeModel.eveningCoefficient = _eveningCoefficient;

        // WeatherModel settings
        _weatherModel.rainTemperatureCoef = _rainTemperatureCoef;
        _weatherModel.sunTemperatureCoef = _sunTemperatureCoef;
        _weatherModel.cloudTemperatureCoef = _cloudTemperatureCoef;
        _weatherModel.rainMoodCoef = _rainMoodCoef;
        _weatherModel.sunMoodCoef = _sunMoodCoef;
        _weatherModel.cloudMoodCoef = _cloudMoodCoef;

        // TemperatureModel settings
        _temperatureModel.temperatureState = _temperatureState;
        _temperatureModel.currentTemperature = _currentTemperature;
        _temperatureModel.coldMultiplier = _coldMultiplier;
        _temperatureModel.coldCondition = _coldCondition;
        _temperatureModel.normalMultiplier = _normalMultiplier;
        _temperatureModel.normalCondition = _normalCondition;
        _temperatureModel.hotMultiplier = _hotMultiplier;
        _temperatureModel.minTemperature = _minTemperature;
        _temperatureModel.maxTemperature = _maxTemperature;
    }
}