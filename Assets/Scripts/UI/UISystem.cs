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
    [SerializeField] private Image WetherIcon;

    private void Update()
    {
        speedText.text = _movementController.GetCurrentSpeed();
        temperatureText.text = _movementController.GetCurrentTemperature();
    }
}
