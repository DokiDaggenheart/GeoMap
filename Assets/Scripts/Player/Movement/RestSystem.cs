using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class RestSystem : MonoBehaviour
{
    [Inject] private MovementModel _movementModel;
    [Inject] private EnergyModel _energyModel;
    [Inject] private TimeController _timeController;
    [Inject] private InventorySystem _inventorySystem;

    [SerializeField] private Button foodButton;
    [SerializeField] private Button bedButton;
    [SerializeField] private TextMeshProUGUI addingEnergyText;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private GameObject startingPanel;
    [SerializeField] private GameObject waitingPanel;
    [SerializeField] private TextMeshProUGUI currentEnergyText;

    private bool foodUsed;
    private bool bedUsed;

    private int addingEnergy;
    private int time;

    private void Start()
    {
        Refresh();
    }
    public void BedButtonListener()
    {
        bedUsed = !bedUsed;
        Refresh();
    }

    public void FoodButtonListener()
    {
        foodUsed = !foodUsed;
        Refresh();
    }

    public void StartRest()
    {
        StartCoroutine(Rest());
    }

    public void Refresh()
    {
        addingEnergy = 25;
        if (bedUsed)
            addingEnergy += 25;
        if (foodUsed)
            addingEnergy += 25;
        time = 2;
        addingEnergyText.text = "Восстановление энергии: " + addingEnergy + "%";
        timeText.text = "Время восстановления: " + time + " часов";
    }

    public IEnumerator Rest()
    {
        waitingPanel.SetActive(true);
        startingPanel.SetActive(false);
        if (foodUsed)
            _inventorySystem.UseFood();
        foodUsed = false;
        bedUsed = false;
        int hoursToMinutes = 120;
        int minutes = 0;
        float energyPerSecond = Convert.ToSingle(addingEnergy) / Convert.ToSingle(hoursToMinutes);
        while (minutes < hoursToMinutes)
        {
            yield return new WaitForSeconds(60.0f / _timeController.timeScale);
            _energyModel.energy += energyPerSecond;
            minutes++;
            currentEnergyText.text = "current energy: " + Mathf.FloorToInt(_energyModel.energy) + "%";
        }
        _movementModel.isRiding = true;
        startingPanel.SetActive(true);
        waitingPanel.SetActive(false);
        this.gameObject.SetActive(false);
    }
}
