using UnityEngine;

public class GameManager : MonoBehaviour
{
    public void OnGameStart()
    {
        if (DiscordController.Instance != null)
            DiscordController.Instance.UpdateDiscordActivity("Entering StarShip", "Earth");
    }

    public void OnPause()
    {
        if (DiscordController.Instance != null)
            DiscordController.Instance.UpdateDiscordActivity("Paused", "Taking a break");
    }

    public void OnRoomEnter()
    {
        string[] planets = new string[] { "Mercury", "Venus", "Mars", "Jupiter", "Saturn", "Uranus", "Neptune", "Pluto" };

        System.Random random = new();
        string planetName = planets[random.Next(planets.Length)];

        if (DiscordController.Instance != null)
            DiscordController.Instance.UpdateDiscordActivity($"Exploring {planetName}", "Milky Way");
    }
}
