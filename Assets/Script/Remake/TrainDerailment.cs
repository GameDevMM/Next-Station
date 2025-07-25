using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrainDerailment : MonoBehaviour
{
    [Header("Settings")]
    public SpeedLevel speedLevel;
    public float inputStrenght;
    public float maxDrift;

    [Header("UI")]
    public Slider driftSlider;

    private float currentDrift;
    private float timeSinceLastDrift;
    private bool playerAtCenter;
    private int driftDirection;

    void Start()
    {
        currentDrift = 0f;

        if (driftSlider != null)
        {
            driftSlider.minValue = -maxDrift;
            driftSlider.maxValue = maxDrift;
            driftSlider.value = 0f;
        }

        RaffleNewDirection();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastDrift += Time.deltaTime;

        if (timeSinceLastDrift >= 1f)
        {
            float driftAmount = GetDriftSpeed();
            currentDrift += driftAmount * driftDirection;
            timeSinceLastDrift = 0f;
        }

        //HandleInput();
        UpdateSlider();
        CheckDerailment();
        CheckBackToMiddle();
    }

    public void DerailmentToRight()
    {
        currentDrift += inputStrenght * Time.deltaTime;
    }

    public void DerailmentToLeft()
    {
        currentDrift -= inputStrenght * Time.deltaTime;
    }

    void HandleInput()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            currentDrift -= inputStrenght * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            currentDrift += inputStrenght * Time.deltaTime;
        }
    }

    void UpdateSlider()
    {
        if (driftSlider != null)
        {
            driftSlider.value = currentDrift;
        }
    }

    float GetDriftSpeed()
    {
        switch (TrainSpeedController.instance.currentSpeedLevel)
        {
            case SpeedLevel.Slow:
                return 1f;

            case SpeedLevel.Medium:
                return 2f;

            case SpeedLevel.Fast:
                return 3f;

            default:
                return 1f;
        }
    }

    void CheckDerailment()
    {
        if (Mathf.Abs(currentDrift) >= maxDrift)
        {
            Debug.Log("Game Over");
        }
    }

    void CheckBackToMiddle()
    {
        bool isStoped = !Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.RightArrow);
        bool isMiddle = Mathf.Abs(currentDrift) <= 0.5f;

        if (isStoped && isMiddle && !playerAtCenter)
        {
            playerAtCenter = true;
            RaffleNewDirection();
        }

        if (!isMiddle)
        {
            playerAtCenter = false;
        }
    }

    void RaffleNewDirection()
    {
        driftDirection = Random.Range(0f, 1f) < 0.5f ? -1 : 1;
    }
}
