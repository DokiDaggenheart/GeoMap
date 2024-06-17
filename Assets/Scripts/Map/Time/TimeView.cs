using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Zenject;

public class TimeView : MonoBehaviour
{
    [Inject] TimeController _timeController;

    private int gameHours = 0;
    private int gameMinutes = 0;

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

            gameMinutes++;
            if (gameMinutes >= 60)
            {
                gameMinutes = 0;
                gameHours++;
                if (gameHours >= 24)
                {
                    gameHours = 0;
                }
            }

            UpdateTimerText();
        }
    }

    void UpdateTimerText()
    {
        timerText.text = string.Format("{0:00}:{1:00}", gameHours, gameMinutes);
    }
}
