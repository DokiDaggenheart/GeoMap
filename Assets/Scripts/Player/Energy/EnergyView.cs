using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class EnergyView : MonoBehaviour
{
    [Inject] private EnergyModel _energyModel;
    private int currentEnergy;
    [SerializeField] TextMeshProUGUI text;

    private void Update()
    {
        currentEnergy = Convert.ToInt32(_energyModel.energy);
        text.text = "<color=green>" + currentEnergy + "%";
    }
}
