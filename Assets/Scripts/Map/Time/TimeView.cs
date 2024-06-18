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

    public TextMeshProUGUI timerText;

    private void Start()
    {
        StartCoroutine(UpdateGameTime());
    }
    IEnumerator UpdateGameTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(60f / _timeController.timeScale);

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
        }
    }

    void UpdateTimerText()
    {
        timerText.text = string.Format("{0:00}:{1:00}", _timeModel.gameHours, _timeModel.gameMinutes);
    }
}
