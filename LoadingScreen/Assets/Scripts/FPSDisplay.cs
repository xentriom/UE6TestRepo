using TMPro;
using UnityEngine;

public class FPSDisplay : MonoBehaviour
{
    public TextMeshProUGUI fpsText;
    public float refreshRate = 0.5f;
    private float timeElapsed = 0f;
    private int frameCount = 0;

    private void Start()
    {
        fpsText.text = "0 FPS";
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;
        frameCount++;

        if (timeElapsed >= refreshRate)
        {
            int fps = Mathf.RoundToInt(frameCount / timeElapsed);
            fpsText.text = $"{fps} FPS";

            timeElapsed = 0f;
            frameCount = 0;
        }
    }
}
