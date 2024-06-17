using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeModel
{
    public GameState gameState = GameState.gameOn;

    public int timeInGameOn = 3;
    public int timeInRealLifeOn = 1;

    public int timeInGameOff;
    public int timeInRealLifeOff;
}

public enum GameState { gameOn, gameOff}