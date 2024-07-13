using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnergyController
{
    [Inject] EnergyModel _energyModel;
    [Inject] MovementModel _movementModel;
    public float energyMultiplier()
    {
        float multiplier;

        if (_energyModel.energy <= _energyModel.maxEnergy && _energyModel.energy > _energyModel.firstStateCondition)
            multiplier = _energyModel.firstStateVelocity;
        else if (_energyModel.energy <= _energyModel.firstStateCondition && _energyModel.energy > _energyModel.secondStateCondition)
            multiplier = _energyModel.secondStateVelocity;
        else if (_energyModel.energy <= _energyModel.secondStateCondition && _energyModel.energy > _energyModel.thirdStateCondition)
            multiplier =  _energyModel.thirdStateVelocity;
        else multiplier = _energyModel.fourthStateVelocity;

        return multiplier / 100;
    }

    public void CheckEnergy()
    {
        if (_energyModel.energy > 100)
            _energyModel.energy = 100;
        if (_energyModel.energy < 0)
        {
            _movementModel.isRiding = false;
            _energyModel.energy = 0;
        }
    }
}
