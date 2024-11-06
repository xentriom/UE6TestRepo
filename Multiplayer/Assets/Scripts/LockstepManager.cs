using UnityEngine;
using System.Collections.Generic;

public class LockstepManager : MonoBehaviour
{
    private readonly Queue<PlayerInput> inputQueue = new();
    private float lockstepInterval = 0.1f;
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= lockstepInterval)
        {
            ProcessLockstep();
            timer = 0f;
        }
    }

    void ProcessLockstep()
    {
        while (inputQueue.Count > 0)
        {
            PlayerInput input = inputQueue.Dequeue();
            // Process input and update game state
        }
        // Send updated game state to all clients
    }

    public void EnqueueInput(PlayerInput input)
    {
        inputQueue.Enqueue(input);
    }
}

public struct PlayerInput
{
    public int playerId;
    public Vector3 direction;
    public bool jump;
}
