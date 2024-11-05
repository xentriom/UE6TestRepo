using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void OnRestart()
    {
        if (DiscordController.Instance != null)
            DiscordController.Instance.UpdateDiscordActivity("Exploring Earth", "Earth");
    }

    public void OnPause()
    {
        if (DiscordController.Instance != null)
            DiscordController.Instance.UpdateDiscordActivity("Cruising Through the Cosmos", "Galaxy");
    }

    public void OnRoomEnter()
    {
        string[] planets = new string[] { "Mercury", "Venus", "Mars", "Jupiter", "Saturn", "Uranus", "Neptune", "Pluto" };

        System.Random random = new();
        string planetName = planets[random.Next(planets.Length)];

        if (DiscordController.Instance != null)
            DiscordController.Instance.UpdateDiscordActivity($"Exploring {planetName}", planetName);
    }
}
