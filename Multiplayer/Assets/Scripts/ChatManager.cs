using System;
using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class ChatManager : MonoBehaviour
{
    public TMP_InputField chatInput;
    public TMP_Text chatDisplay;
    private readonly Queue<string> messageQueue = new();
    private readonly int maxMessages = 5;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            BroadcastChatMessage();
        }
    }

    void BroadcastChatMessage()
    {
        string message = chatInput.text;
        if (!string.IsNullOrEmpty(message))
        {
            AddMessageToQueue(FormatMessage(message));
            chatInput.text = "";
        }
    }

    private void AddMessageToQueue(string message)
    {
        if (messageQueue.Count >= maxMessages)
        {
            messageQueue.Dequeue();
        }
        messageQueue.Enqueue(message);
        UpdateChatDisplay();
    }

    private void UpdateChatDisplay()
    {
        chatDisplay.text = "";
        foreach (string msg in messageQueue)
        {
            chatDisplay.text += msg + "\n";
        }
    }

    private string FormatMessage(string message)
    {
        string timestamp = DateTime.Now.ToString("HH:mm:ss:fff");
        string color = GetColorForTime();
        string formattedMessage = $"<color={color}>{timestamp}</color>: {message}";
        return formattedMessage;
    }

    private string GetColorForTime()
    {
        int hour = DateTime.Now.Hour;
        if (hour < 12)
        {
            return "#00FF00";
        }
        else if (hour < 18)
        {
            return "#FFFF00";
        }
        else
        {
            return "#FF0000";
        }
    }
}
