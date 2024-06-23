using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TimeController : MonoBehaviour
{
    [Inject] private TimeModel _timeModel;
    public float timeScale;

    private void Start()
    {
        ChangeTimeScale();
    }
    private void ChangeTimeScale()
    {
        timeScale = RealToGameTimeScale();
    }

    public float GetCurrentTimeOfDayCoefficient()
    {
        TimeOfDay currentTimeOfDay = _timeModel.GetCurrentTimeOfDay();

        switch (currentTimeOfDay)
        {
            case TimeOfDay.Night:
                return _timeModel.nightCoefficient;
            case TimeOfDay.Morning:
                return _timeModel.morningCoefficient;
            case TimeOfDay.Day:
                return _timeModel.dayCoefficient;
            case TimeOfDay.Evening:
                return _timeModel.eveningCoefficient;
            default:
                return 1.0f;
        }
    }

    private float RealToGameTimeScale()
    {
        if (_timeModel.gameState == GameState.gameOn)
            return _timeModel.timeInGameOn / _timeModel.timeInRealLifeOn;
        else
            return _timeModel.timeInGameOff / _timeModel.timeInRealLifeOff;
    }
}
