using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (DiscordController.Instance != null)
            DiscordController.Instance.UpdateDiscordActivity("In Main Menu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnGameStart()
    {
        if (DiscordController.Instance != null)
            DiscordController.Instance.UpdateDiscordActivity("Exploring: Europa");
    }

    public void OnPause()
    {
        if (DiscordController.Instance != null)
            DiscordController.Instance.UpdateDiscordActivity("Paused", "Taking a break");
    }

    public void OnRoomEnter()
    {
        var rooms = new string[] { "Saturn", "Pluto", "Mars", "Jupiter", "Uranus", "Neptune", "Mercury", "Venus" };
        var random = new System.Random();
        var randomIndex = random.Next(rooms.Length);
        var roomName = rooms[randomIndex];

        if (DiscordController.Instance != null)
            DiscordController.Instance.UpdateDiscordActivity($"Exploring: {roomName}");
    }
}
