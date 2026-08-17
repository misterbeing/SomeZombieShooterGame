using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FPSCalculator : MonoBehaviour
{
    public TextMeshProUGUI fpsText;

    private float timer;
    private int frames;
    private float fps;

    [SerializeField] private TextMeshProUGUI playerHealthCounter;

    [SerializeField] private Image healthmeter;


    private void Start()
    {
        Application.targetFrameRate = 120;
    }

    private void OnEnable()
    {
        GameManager.onUpdateHealth += UpdateHealthMeter;
    }

    private void OnDisable()
    {
        GameManager.onUpdateHealth -= UpdateHealthMeter;
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

    public void UpdateHealthMeter(float _currentHealth, float _totalHealth)
    {
        playerHealthCounter.text = $"{_currentHealth.ToString()}/{_totalHealth.ToString()}";
        var val = Mathf.Clamp01(_currentHealth/_totalHealth);
        healthmeter.fillAmount = val;
    }
}
