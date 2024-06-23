using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LogSystem : MonoBehaviour
{
    [Inject] private TimeModel _timeModel;
    [Inject] private MovementModel _movementModel;
    [Inject] private MovementController _movementController;
    public float tolerance;
    private List<LogEntry> currentLogList;
    private int logListCurrentIndex = 0;

    private void Update()
    {
        if ( _movementModel.progress >= currentLogList[logListCurrentIndex].triggerValue)
        {
            Debug.Log(currentLogList[logListCurrentIndex].log.Happy);

            logListCurrentIndex++;
        }
    }

    public void SetLogList(PathSection pathSection)
    {
        currentLogList = new List<LogEntry>();
        int i = 1;
        foreach(Log log in pathSection.logs)
        {
            float triggerValue = (Convert.ToSingle(pathSection.length) / Convert.ToSingle(pathSection.logs.Count + 1)) * i /pathSection.length;
            LogEntry newItem = new LogEntry(log, triggerValue);
            currentLogList.Add(newItem);
            i++;
        }
        logListCurrentIndex = 0;
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
