using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyModel
{
    public float maxEnergy;
    [Range(0, 100)] public float energy;
    public int startRidingEnergy;
    public int firstStateCondition;
    public int firstStateVelocity;
    public int secondStateCondition;
    public int secondStateVelocity;
    public int thirdStateCondition;
    public int thirdStateVelocity;
    public int fourthStateVelocity;
    public int energyAddition;
    public int energyDiminution;
}
