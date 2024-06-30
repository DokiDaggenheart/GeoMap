using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Zenject;

public class UISystem : MonoBehaviour
{
    [Inject] private MovementController _movementController;

    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI temperatureText;
    [SerializeField] private TextMeshProUGUI nameOfSpaceText;
    [SerializeField] private TextMeshProUGUI landscapeText;
    [SerializeField] private TextMeshProUGUI directionText;
    [SerializeField] private Image weatherIcon;

    private void Update()
    {
        speedText.text = _movementController.GetCurrentSpeed();
        temperatureText.text = _movementController.GetCurrentTemperature();
        nameOfSpaceText.text = _movementController.GetCurrentNameOfSpace();
        landscapeText.text = _movementController.GetCurrentLandscapeName();
        directionText.text = _movementController.GetCurrentDirection();

    }
}
