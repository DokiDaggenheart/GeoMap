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

    private void Update()
    {
        time += Time.deltaTime * _timeController.timeScale;
        if(time >= 60)
        {
            _timeModel.gameMinutes++;
            if (_timeModel.gameMinutes >= 60)
            {
                _timeModel.gameMinutes = 0;
                _timeModel.gameHours++;
                if (_timeModel.gameHours >= 24)
                {
                    _timeModel.gameHours = 0;
                    _timeModel.gameDays++;
                }
            }
            UpdateTimerText();
            time = 0;
        }
    }
    void UpdateTimerText()
    {
        timerText.text = _timeModel.CurrentTime();
    }
}
