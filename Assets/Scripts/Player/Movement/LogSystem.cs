using System.Collections.Generic;
using UnityEngine;
using Zenject;
using TMPro;
using System;
using UnityEngine.UI;

public class LogSystem : MonoBehaviour
{
    public GameObject logEntryPrefab; 
    public Transform logContent; 
    public Color importantLogColor = Color.yellow;
    public Color defaultLogColor = Color.white;
    public Image viewPort;
    public Image logWindow;
    public AudioSource logNotificationSource;

    [Inject] private TimeModel _timeModel;
    [Inject] private MovementModel _movementModel;
    [Inject] private MoodController _moodController;

    private List<LogEntry> currentLogList;
    private int logListCurrentIndex = 0;
    public bool logWindowIsVisible = false;

    private void Update()
    {
        if (_movementModel.progress >= currentLogList[logListCurrentIndex].triggerValue)
        {
            Log currentLog = currentLogList[logListCurrentIndex].log;
            string logText = GetLogTextByMood(currentLog);
            AddLogEntry(logText, currentLog.isImporntant);

            logListCurrentIndex++;
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if(scroll != 0)
        {
            ScrollContent(-scroll);
        }
    }

    public void SetLogList(PathSection pathSection)
    {
        currentLogList = new List<LogEntry>();
        int i = 1;
        foreach (Log log in pathSection.logs)
        {
            float triggerValue = (Convert.ToSingle(pathSection.length) / Convert.ToSingle(pathSection.logs.Count + 1)) * i / pathSection.length;
            LogEntry newItem = new LogEntry(log, triggerValue);
            currentLogList.Add(newItem);
            i++;
        }
        logListCurrentIndex = 0;
    }

    private string GetLogTextByMood(Log log)
    {
        // Используйте здесь вашу логику для получения настроения персонажа
        MoodState currentMood = _moodController.GetCurrentMoodState(); // Предположим, что у вас есть такой параметр

        switch (currentMood)
        {
            case MoodState.happy:
                return log.Happy;
            case MoodState.neutral:
                return log.Neutral;
            case MoodState.sad:
                return log.Sad;
            default:
                return log.Neutral;
        }
    }

    public void AddLogEntry(string text, bool isImportant)
    {
        GameObject logEntryObject = Instantiate(logEntryPrefab, logContent);
        TextMeshProUGUI logEntryText = logEntryObject.GetComponent<TextMeshProUGUI>();
        logEntryText.text = $"[{_timeModel.CurrentTime()}] {text}";
        logEntryText.color = isImportant ? importantLogColor : defaultLogColor;
        logNotificationSource.Play();
        ScrollContent(0.5f);
    }

    public void LogWindowVisibilityChanging()
    {
    }

    public void ShowLogWindow()
    {
        viewPort.color = new Color(viewPort.color.r, viewPort.color.g, viewPort.color.b, 255);
        logWindow.color = new Color(logWindow.color.r, logWindow.color.g, logWindow.color.b, 255);
        logWindowIsVisible = true;
    }

    public void HideLogWindow()
    {
        viewPort.color = new Color(viewPort.color.r, viewPort.color.g, viewPort.color.b, 0);
        logWindow.color = new Color(logWindow.color.r, logWindow.color.g, logWindow.color.b, 0);
        logWindowIsVisible = false;
    }

    private void ScrollContent(float y)
    {
        logContent.GetComponent<RectTransform>().position = new Vector3(logContent.GetComponent<RectTransform>().position.x, logContent.GetComponent<RectTransform>().position.y + y);
    }
}
public struct LogEntry
{
    public LogEntry(Log _log, float _trigger)
    {
        log = _log;
        triggerValue = _trigger;
    }
    public Log log;
    public float triggerValue;
}
