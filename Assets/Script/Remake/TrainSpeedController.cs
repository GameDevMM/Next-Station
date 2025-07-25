using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public enum SpeedLevel
{
    Slow,
    Medium,
    Fast
}

public class TrainSpeedController : MonoBehaviour
{
    public static TrainSpeedController instance;

    [Header ("Speed Settings")]
    public float slowSpeed;
    public float mediumSpeed;
    public float fastSpeed;

    [Header("Current State")]
    public SpeedLevel currentSpeedLevel = SpeedLevel.Medium;
    public float currentSpeed { get; private set; }

    [Header("UI")]
    public TextMeshProUGUI currentSpeedText;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        UpdateSpeedUI();
    }

    public void IncreaseSpeed()
    {
        if((int)currentSpeedLevel < Enum.GetValues(typeof(SpeedLevel)).Length - 1)
        {
            SetSpeedLevel(currentSpeedLevel + 1);
        }
    }

    public void DecreaseSpeed()
    {
        if ((int)currentSpeedLevel > 0)
        {
            SetSpeedLevel(currentSpeedLevel - 1);

        }
    }

    private void UpdateSpeedUI()
    {
        switch (currentSpeedLevel)
        {
            case SpeedLevel.Slow:
                currentSpeed = slowSpeed;
                currentSpeedText.text = "Lenta";
                break;

            case SpeedLevel.Medium:
                currentSpeed = mediumSpeed;
                currentSpeedText.text = "Médio";
                break;

            case SpeedLevel.Fast:
                currentSpeed = fastSpeed;
                currentSpeedText.text = "Rápida";
                break;
        }
    }

    public void SetSpeedLevel(SpeedLevel level)
    {
        if(currentSpeedLevel != level)
        {
            currentSpeedLevel = level;
            UpdateSpeedUI();
        }
    }
}
