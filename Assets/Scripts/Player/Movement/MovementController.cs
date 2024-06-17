using System;
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
    private int i = 0;
    private void Update()
    {
        _movementModel.isRiding = _energyController.CheckEnergy();
        Debug.Log(_movementModel.isRiding + "Speed:" + _movementModel.totalVelocity);
        if (_movementModel.isRiding)
        {
            GoToNextPoint();
            _movementModel.totalVelocity = _movementModel.velocity
                * _energyController.energyMultiplier()/*
                * _moodController.MoodMultiplier()*/
                * _pathModel.pathList[i].landscape.landscapeMultiplier;

            gameObject.transform.position = Vector2.Lerp(_pathModel.pathList[i].start.position, _pathModel.pathList[i].end.position, _movementModel.progress);
            _movementModel.progress += (_movementModel.totalVelocity / _pathModel.pathList[i].length) * (Time.deltaTime/_timeController.timeScale);
            _energyModel.energy -= _energyModel.energyDiminution * (Time.deltaTime/60);
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
            i += 1;
            _movementModel.progress = 0;
        }
    }

}
