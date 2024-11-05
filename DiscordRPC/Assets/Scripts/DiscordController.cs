using UnityEngine;
using Discord;
using System;

public class DiscordController : MonoBehaviour
{
    public static DiscordController Instance { get; private set; }

    private Discord.Discord discord;
    private Discord.ActivityManager activityManager;
    private long appStartTimestamp;

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
        appStartTimestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        UpdateDiscordActivity("Exploring Earth", "Earth");
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
            activityManager = null;
            discord?.Dispose();
            discord = null;
            Instance = null;
        }
    }

    /// <summary>
    /// Update the Discord Rich Presence
    /// </summary>
    /// <param name="details">The text for details</param>
    /// <param name="state">The text for state</param>
    public void UpdateDiscordActivity(string details, string smallImageKey)
    {
        var activity = new Discord.Activity
        {
            Details = details,
            Timestamps = { Start = appStartTimestamp },
            Assets = 
            { 
                LargeImage = "logo", LargeText = "xentriom's",
                SmallImage = smallImageKey.ToLower(), SmallText = smallImageKey 
            }
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
