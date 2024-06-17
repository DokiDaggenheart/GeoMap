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
    private int i = 0;
    private void Update()
    {
        _movementModel.isRiding = _energyController.CheckEnergy();
        Debug.Log(_movementModel.isRiding);
        if (_movementModel.isRiding)
        {
            GoToNextPoint();
            _movementModel.totalVelocity = _movementModel.velocity
                * _energyController.energyMultiplier()
                * _moodController.MoodMultiplier()
                * CurrentLandscapeMultiplier(_pathModel.pathList[i].landscape, _pathModel.pathList[i]);

            gameObject.transform.position = Vector2.Lerp(_pathModel.pathList[i].start.position, _pathModel.pathList[i].end.position, _movementModel.progress);
            _movementModel.progress += (_movementModel.totalVelocity / (_pathModel.pathList[i].end.position - _pathModel.pathList[i].start.position).magnitude) * Time.deltaTime;
            _energyModel.energy -= _energyModel.energyDiminution * Time.deltaTime;
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

    private float CurrentLandscapeMultiplier(LandscapeData landscape, PathSection pathSection) 
    {
        switch (pathSection.direction)
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
}
