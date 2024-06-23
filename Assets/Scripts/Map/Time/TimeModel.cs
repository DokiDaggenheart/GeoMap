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

    public int gameHours = 0;
    public int gameMinutes = 0;
    public int gameDays = 0;

    public float nightCoefficient;
    public float morningCoefficient;
    public float dayCoefficient;
    public float eveningCoefficient;

    private readonly (int start, int end) night = (0, 4);
    private readonly (int start, int end) morning = (5, 10);
    private readonly (int start, int end) day = (11, 17);
    private readonly (int start, int end) evening = (18, 21);

    public TimeOfDay GetCurrentTimeOfDay()
    {
        if (gameHours >= night.start && gameHours <= night.end)
        {
            return TimeOfDay.Night;
        }
        else if (gameHours >= morning.start && gameHours <= morning.end)
        {
            return TimeOfDay.Morning;
        }
        else if (gameHours >= day.start && gameHours <= day.end)
        {
            return TimeOfDay.Day;
        }
        else if (gameHours >= evening.start && gameHours <= evening.end)
        {
            return TimeOfDay.Evening;
        }
        else
        {
            return TimeOfDay.Night;
        }
    }

    public string CurrentTime()
    {
        return string.Format("{0:00}:{1:00}", gameHours, gameMinutes);
    }
}
public enum TimeOfDay
{
    Night,
    Morning,
    Day,
    Evening
}
public enum GameState { gameOn, gameOff}