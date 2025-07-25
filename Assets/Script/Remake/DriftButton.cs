using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DriftButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isRight;
    public TrainDerailment derailment;

    private bool isHeld = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        isHeld = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isHeld = false;
    }

    void Update()
    {
        if (!isHeld || derailment == null) return;

        if (isRight)
        {
            derailment.DerailmentToRight();
        }
        else
        {
            derailment.DerailmentToLeft();
        }
    }
}
