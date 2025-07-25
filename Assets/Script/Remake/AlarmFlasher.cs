using UnityEngine;
using UnityEngine.UI;

public class AlarmFlasher : MonoBehaviour
{
    public static AlarmFlasher instance;

    public Image alarmImage;
    public float flashSpeed;

    private bool isCritical = false;
    private Color targetColor = Color.clear;

    public Color criticalHotColor;
    public Color criticalColdColor;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (isCritical)
        {
            float alpha = Mathf.PingPong(Time.time * flashSpeed, 0.5f) + 0.2f;
            float finalAlpha = alpha * targetColor.a;
            alarmImage.color = new Color(targetColor.r, targetColor.g, targetColor.b, finalAlpha);
        }
        else
        {
            alarmImage.color = Color.Lerp(alarmImage.color, Color.clear, Time.deltaTime * 5f);
        }
    }

    public void TriggerCriticalAlarm(bool active, bool isHot)
    {
        isCritical = active;
        targetColor = isHot ? criticalHotColor : criticalColdColor;
    }
}
