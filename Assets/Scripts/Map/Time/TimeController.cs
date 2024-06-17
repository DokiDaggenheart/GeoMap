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

    private float RealToGameTimeScale()
    {
        if (_timeModel.gameState == GameState.gameOn)
            return _timeModel.timeInGameOn / _timeModel.timeInRealLifeOn;
        else
            return _timeModel.timeInGameOff / _timeModel.timeInRealLifeOff;
    }
}
