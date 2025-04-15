using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DebugViewer : MonoBehaviour
{
    public TextMeshProUGUI debugText; // Assign in inspector
    public int maxLogs = 5;

    private Queue<string> logQueue = new Queue<string>();

    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }

    private void Start()
    {
        if (debugText == null)
        {
            debugText = GetComponent<TextMeshProUGUI>();
        }
        Debug.Log("TEST LOG");
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        // Add new log to the queue
        logQueue.Enqueue(logString);

        // Trim the queue if it exceeds the max
        while (logQueue.Count > maxLogs)
        {
            logQueue.Dequeue();
        }

        // Display logs
        debugText.text = string.Join("\n", logQueue);
    }
}
