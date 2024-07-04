using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

public class TimeView : MonoBehaviour
{
    [Inject] TimeController _timeController;
    [Inject] TimeModel _timeModel;
    private float time;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI timeOfDayText;
    public TextMeshProUGUI dayText;
    private void Update()
    {
        time += Time.deltaTime * _timeController.timeScale;
        if(time >= 60)
        {
            time = 0;
            Debug.Log("minutes++");
            _timeModel.gameMinutes++;
            if (_timeModel.gameMinutes >= 60)
            {
                _timeModel.gameMinutes = 0;
                _timeModel.gameHours++;
                UpdateTimeOfDayText();
                if (_timeModel.gameHours >= 24)
                {
                    _timeModel.gameHours = 0;
                    _timeModel.gameDays++;
                    UpdateDayText();
                }
            }
            UpdateTimerText();
        }
    }
    private void UpdateTimerText()
    {
        timerText.text = _timeModel.CurrentTime();
    }

    private void UpdateDayText()
    {
        dayText.text = "Day " + _timeModel.gameDays;
    }

    private void UpdateTimeOfDayText()
    {
        timeOfDayText.text = _timeModel.GetCurrentTimeOfDay().ToString();
    }
}
