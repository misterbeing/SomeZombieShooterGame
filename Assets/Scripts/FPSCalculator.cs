using TMPro;
using UnityEngine;

public class FPSCalculator : MonoBehaviour
{
    public TextMeshProUGUI fpsText;

    private float timer;
    private int frames;
    private float fps;


    private void Start()
    {
        Application.targetFrameRate = 120;
    }
    private void Update()
    {
        frames++;
        timer += Time.unscaledDeltaTime;

        // Update FPS display every 0.5 sec
        if (timer >= 0.5f)
        {
            fps = frames / timer;

            fpsText.text = "FPS: " + Mathf.RoundToInt(fps);

            frames = 0;
            timer = 0f;
        }
    }
}
