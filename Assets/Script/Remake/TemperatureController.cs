using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum TemperatureZone
{
    CriticalCold,
    AlertCold,
    Ideal,
    AlertHot,
    CriticalHot
}

public static class GameStats
{
    public static int currentScore = 0;
}

public class TemperatureController : MonoBehaviour
{
    [Header("Game Timer")]
    public float gameDuration;
    [SerializeField] private float gameTimer;
    [SerializeField] private bool gameEnded = false;
    [SerializeField] private TextMeshProUGUI timerText;

    [Header("Points")]
    public int totalScore;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI feedbackText;

    [Header("Feedback Visual Settings")]
    public float feedbackDisplayTime;

    private Coroutine feedbackCoroutine;

    public static TemperatureController instance;

    [Header("General Settings")]
    [Range(0, 100)] public float currentTemperature;

    [Header("Temperature Increment by Speed")]
    public int slowHeatPerTick;
    public int mediumHeatPerTick;
    public int fastHeatPerTick;

    [Header("Update Interval")]
    public float updateInterval;

    [Header("Scoring by Zone")]
    public int comfortPoints;
    public int alertPenalty;
    public int dangerPenalty;

    private float timer;

    public delegate void OnZoneChange(TemperatureZone newZone);
    public event OnZoneChange ZongeChanged;

    public TemperatureZone currentZone = TemperatureZone.Ideal;

    public int heat;
    public int scoreChange;

    [SerializeField] private TextMeshProUGUI currentTemperatureText;

    public Image temperatureZoneImage;

    [Header("Temperature Zone Images")]
    public Sprite criticalColdSprite;
    public Sprite alertColdSprite;
    public Sprite idealSprite;
    public Sprite alertHotSprite;
    public Sprite criticalHotSprite;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        ConvertToRealTemperature();

        timer += Time.deltaTime;
        if (timer >= updateInterval)
        {
            timer = 0;
            ApplyAutomaticHeating();
            CheckTemperatureZone();
            UpdateScoreOverTime();
        }

        if (!gameEnded)
        {
            gameTimer += Time.deltaTime;

            float timeLeft = Mathf.Max(gameDuration - gameTimer, 0);
            UpdateTimerUI(timeLeft);

            if (gameTimer >= gameDuration)
            {
                gameEnded = true;
                EndGame();
                return;
            }
        }
    }

    private void UpdateTimerUI(float timeLeft)
    {
        int minutes = Mathf.FloorToInt(timeLeft / 60f);
        int seconds = Mathf.FloorToInt(timeLeft % 60f);
        timerText.text = $"{minutes:00}:{seconds:00}";
    }

    private void UpdateScoreOverTime()
    {
        totalScore += scoreChange;
        UpdateScoreUI();

        if (scoreChange != 0)
        {
            ShowScoreFeedback(scoreChange);
        }
    }

    private void ShowScoreFeedback(int amount)
    {
        if (feedbackCoroutine != null)
        {
            StopCoroutine(feedbackCoroutine);
        }

        feedbackCoroutine = StartCoroutine(FeedbackRoutine(amount));
    }

    private IEnumerator FeedbackRoutine(int amount)
    {
        Debug.Log("Update Score");
        feedbackText.text = amount > 0 ? $"+{amount}" : amount.ToString();
        feedbackText.color = amount > 0 ? Color.green : Color.red;
        feedbackText.gameObject.SetActive(true);

        yield return new WaitForSeconds(feedbackDisplayTime);

        feedbackText.gameObject.SetActive(false);
    }

    private void EndGame()
    {
        GameStats.currentScore = totalScore;
        SceneManager.LoadScene("Game Over");
    }

    private void ApplyAutomaticHeating()
    {
        switch (TrainSpeedController.instance.currentSpeedLevel)
        {
            case SpeedLevel.Slow:
                heat = slowHeatPerTick;
                Debug.Log("Está lento");
                break;

            case SpeedLevel.Medium:
                heat = mediumHeatPerTick;
                Debug.Log("Está médio");
                break;

            case SpeedLevel.Fast:
                heat = fastHeatPerTick;
                Debug.Log("Está rápido");
                break;

            default:
                heat = 0;
                break;
        }

        currentTemperature = Mathf.Clamp(currentTemperature + heat, 0, 100);
    }

    public void IncreaseTemperature()
    {
        currentTemperature = Mathf.Clamp(currentTemperature + 1, 0, 100);
        CheckTemperatureZone();
    }

    public void DecreaseTemperature()
    {
        currentTemperature = Mathf.Clamp(currentTemperature - 1, 0, 100);
        CheckTemperatureZone();
    }

    private void CheckTemperatureZone()
    {
        var newZone = GetTemperatureZone(currentTemperature);

        if (newZone != currentZone)
        {
            currentZone = newZone;
            ZongeChanged?.Invoke(newZone);
            UpdateTemperatureZoneImage(newZone);
        }

        if (newZone == TemperatureZone.CriticalCold || newZone == TemperatureZone.CriticalHot)
        {
            bool isHot = newZone == TemperatureZone.CriticalHot;
            AlarmFlasher.instance.TriggerCriticalAlarm(true, isHot);
        }
        else
        {
            AlarmFlasher.instance.TriggerCriticalAlarm(false, false);
        }

        ApplyScoreByZone(currentZone);
    }

    private TemperatureZone GetTemperatureZone(float temp)
    {
        if (temp <= 19) return TemperatureZone.CriticalCold;
        if (temp <= 30) return TemperatureZone.AlertCold;
        if (temp <= 60) return TemperatureZone.Ideal;
        if (temp <= 80) return TemperatureZone.AlertHot;
        return TemperatureZone.CriticalHot;
    }

    private void ApplyScoreByZone(TemperatureZone zone)
    {
        switch (zone)
        {
            case TemperatureZone.Ideal:
                scoreChange = comfortPoints;
                break;

            case TemperatureZone.AlertCold:
            case TemperatureZone.AlertHot:
                scoreChange = alertPenalty;
                break;

            case TemperatureZone.CriticalCold:
            case TemperatureZone.CriticalHot:
                scoreChange = dangerPenalty;
                break;
            default:
                scoreChange = 0;
                break;
        }
    }

    void UpdateTemperatureZoneImage(TemperatureZone zone)
    {
        switch (zone)
        {
            case TemperatureZone.CriticalHot:
                temperatureZoneImage.sprite = criticalHotSprite;
                break;

            case TemperatureZone.AlertHot:
                temperatureZoneImage.sprite = alertHotSprite;
                break;

            case TemperatureZone.Ideal:
                temperatureZoneImage.sprite = idealSprite;
                break;

            case TemperatureZone.AlertCold:
                temperatureZoneImage.sprite = alertColdSprite;
                break;

            case TemperatureZone.CriticalCold:
                temperatureZoneImage.sprite = criticalColdSprite;
                break;
            default:
                temperatureZoneImage.sprite = null;
                break;
        }
    }

    private void ApplyZoneEffects(TemperatureZone zone)
    {
        switch(zone)
        {
            case TemperatureZone.CriticalCold:
            case TemperatureZone.CriticalHot:
                TrainSpeedController.instance.SetSpeedLevel(SpeedLevel.Slow);
                break;

            case TemperatureZone.Ideal:
                TrainSpeedController.instance.SetSpeedLevel(SpeedLevel.Medium);
                break;

            default:

                break;
        }
    }

    private float ConvertToCelsius(float internalTemp)
    {
        float maxCelsius = 40f;
        return (internalTemp / 100f) * maxCelsius;
    }

    private void ConvertToRealTemperature()
    {
        float displayTemp = ConvertToCelsius(currentTemperature);
        currentTemperatureText.text = displayTemp.ToString("F1") + "°C";
    }

    private void UpdateScoreUI()
    {
        scoreText.text = $"Pontuação: {totalScore}";
    }
}
