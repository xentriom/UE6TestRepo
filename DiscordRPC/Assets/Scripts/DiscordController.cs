using UnityEngine;
using Discord;
using System;

public class DiscordController : MonoBehaviour
{
    public static DiscordController Instance { get; private set; }

    private Discord.Discord discord;
    private Discord.ActivityManager activityManager;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        // Check if an instance already exists and destroy this if so
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Set this as the instance and mark it as persistent across scenes
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        discord = new Discord.Discord(1303048708031643751, (System.UInt64)Discord.CreateFlags.Default);
        activityManager = discord.GetActivityManager();
    }

    // Update is called once per frame
    void Update()
    {
        discord?.RunCallbacks();
    }

    // Called when the MonoBehaviour will be destroyed
    void OnDestroy()
    {
        if (Instance == this)
        {
            discord.Dispose();
            Instance = null;
        }
    }

    /// <summary>
    /// Update the Discord Rich Presence
    /// </summary>
    /// <param name="details">The text for details</param>
    /// <param name="state">The text for state</param>
    public void UpdateDiscordActivity(string details, string state = null)
    {
        var activity = new Discord.Activity
        {
            Details = details,
            State = state,
            Timestamps = { Start = DateTimeOffset.UtcNow.ToUnixTimeSeconds() },
            Assets = { LargeImage = "logo", LargeText = "Entropy" }
        };

        activityManager.UpdateActivity(activity, res =>
        {
            if (res == Discord.Result.Ok)
                Debug.Log("Discord Rich Presence updated!");
            else
                Debug.LogError("Failed to update Discord Rich Presence.");
        });
    }
}
