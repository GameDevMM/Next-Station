using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TrainSpeedController : MonoBehaviour
{
    public enum SpeedLevel
    {
        Slow,
        Medium,
        Fast
    }

    [Header ("Speed Settings")]
    public float slowSpeed;
    public float mediumSpeed;
    public float fastSpeed;

    [Header("Current State")]
    [SerializeField] private SpeedLevel currentSpeedLevel = SpeedLevel.Medium;
    public float currentSpeed { get; private set; }

    [Header("UI")]
    public TextMeshProUGUI currentSpeedText;

    void Start()
    {
        UpdateSpeedUI();
    }

    public void IncreaseSpeed()
    {
        if((int)currentSpeedLevel < Enum.GetValues(typeof(SpeedLevel)).Length - 1)
        {
            currentSpeedLevel++;
            UpdateSpeedUI();
        }
    }

    public void DecreaseSpeed()
    {
        if ((int)currentSpeedLevel > 0)
        {
            currentSpeedLevel--;
            UpdateSpeedUI();
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
}
