using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Zenject;

public class UISystem : MonoBehaviour
{
    [Inject] private MovementController _movementController;
    [Inject] private WeatherModel _weatherModel;
    [Inject] private LogSystem _logSystem;

    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI temperatureText;
    [SerializeField] private TextMeshProUGUI nameOfSpaceText;
    [SerializeField] private TextMeshProUGUI landscapeText;
    [SerializeField] private TextMeshProUGUI directionText;
    [SerializeField] private TextMeshProUGUI moodText;
    [SerializeField] private TextMeshProUGUI distanceText;
    [SerializeField] private Image weatherIcon;
    [SerializeField] private List<Sprite> weatherIcons;
    [SerializeField] private List<GameObject> lowerButtons;
    [SerializeField] private GameObject InventoryPanel;
    [SerializeField] private GameObject AchievementPanel;

    private Dictionary<Weather, Sprite> weatherIconMapping;

    private void Start()
    {
        weatherIconMapping = new Dictionary<Weather, Sprite>
    {
        { Weather.sunny, weatherIcons[0] },
        { Weather.cloudy, weatherIcons[1] },
        { Weather.rainy, weatherIcons[2] }
    };
    }

    private void Update()
    {
        speedText.text = _movementController.GetCurrentSpeed();
        temperatureText.text = _movementController.GetCurrentTemperature();
        nameOfSpaceText.text = _movementController.GetCurrentNameOfSpace();
        landscapeText.text = _movementController.GetCurrentLandscapeName();
        directionText.text = _movementController.GetCurrentDirection();
        moodText.text = _movementController.GetCurrentMood();
        distanceText.text = _movementController.GetTravelledDistance();
        ChangeWeatherIcon();
    }

    private void ChangeWeatherIcon()
    {
        Weather currentWeather = _weatherModel.weatherState;
        if (weatherIconMapping.TryGetValue(currentWeather, out Sprite newIcon))
            weatherIcon.sprite = newIcon;
    }

    public void UnpressButtons()
    {
        HideAllWindows();
        foreach (GameObject button in lowerButtons)
        {
            button.GetComponent<Image>().color = Color.white;
            button.GetComponentInChildren<TextMeshProUGUI>().color = Color.black;
        }
    }

    public void HideAllWindows()
    {
        _logSystem.HideLogWindow();
        InventoryPanel.SetActive(false);
        AchievementPanel.SetActive(false);
    }
}
