using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameSettings : MonoBehaviour
{
    [Inject] private PathModel _pathModel;
    [Inject] private MovementModel _movementModel;
    [Inject] private MoodModel _moodModel;
    [Inject] private EnergyModel _energyModel;

    [Header("PathSettings")]
    [SerializeField] private List<PathSection> _pathList;
    [SerializeField] private float _spacingLength;
    [SerializeField] private GameObject _dotPrefab;
    [Header("PathSettings")]
    [SerializeField] private float _maxEnergy;
    [SerializeField] private float _velocity;
    [Tooltip("При восстановлении энергии во время игры на этой отметке игрок продолжит движение")]
    [SerializeField]
    private int _startRidingEnergy;
    [Header("EnergyStatesSettings")]
    [Tooltip("Нижний порок первого состояния (энергия)")]
    [SerializeField] private int _firstStateCondition;
    [Tooltip("Прибавка к скорости во время первого состояния")]
    [SerializeField] private int _firstStateVelocity;
    [SerializeField] private int _secondStateCondition;
    [SerializeField] private int _secondStateVelocity;
    [SerializeField] private int _thirdStateCondition;
    [SerializeField] private int _thirdStateVelocity;
    [SerializeField] private int _fourthStateVelocity;
    [SerializeField] private int _energyDiminution;
    [SerializeField] private int _energyAddition;
    [Header("MoodSettings")]
    [SerializeField] private int _happyMultiplier;
    [SerializeField] private int _sadMultiplier;
    [SerializeField] private int _neutralMultiplier;
    [Header("Time")]
    public int _timeInGameOn;
    public int _timeInRealLifeOn;
    public int _timeInGameOff;
    public int _timeInRealLifeOff;



    private void Awake()
    {
        _pathModel.pathList = _pathList;
        _pathModel.spacingLength = _spacingLength;
        _pathModel.dotPrefab = _dotPrefab;
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
        _moodModel.happyMultiplier = _happyMultiplier;
        _moodModel.sadMultiplier = _sadMultiplier;
        _moodModel.neutralMultiplier = _neutralMultiplier;
        _energyModel.energyAddition = _energyAddition;
        _energyModel.energyDiminution = _energyDiminution;
    }
}
