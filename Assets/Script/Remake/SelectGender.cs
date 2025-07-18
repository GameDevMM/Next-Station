using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectGender : MonoBehaviour
{
    public enum Gender
    {
        None,
        Male,
        Female
    }

    [Header ("Animators Controller")]
    [SerializeField] private Animator femaleAnimator;
    [SerializeField] private Animator maleAnimator;

    [Header("Buttons Controller")]
    [SerializeField] private Button maleButton;
    [SerializeField] private Button femaleButton;
    [SerializeField] private Button confirmGenderButton;

    public Gender selectedGender { get; private set; } = Gender.None;
    
    void Start()
    {
        confirmGenderButton.interactable = false;

        maleButton.onClick.AddListener(() => ChoiceGender(Gender.Male));
        femaleButton.onClick.AddListener(() => ChoiceGender(Gender.Female));
    }

    void Update()
    {
        CheckInputOutUI();
    }

    void ChoiceGender(Gender gender)
    {
        if (gender == selectedGender)
        {
            return;
        }

        selectedGender = gender;
        confirmGenderButton.interactable = true;

        switch(gender)
        {
            case Gender.Male:
                maleAnimator.Play("M_Walk");
                femaleAnimator.Play("F_Idle");
                break;

            case Gender.Female:
                femaleAnimator.Play("F_Walk");
                maleAnimator.Play("M_Idle");
                break;
        }
    }

    void DeselectGender()
    {
        selectedGender = Gender.None;
        confirmGenderButton.interactable = false;

        maleAnimator.Play("M_Idle");
        femaleAnimator.Play("F_Idle");
    }

    void CheckInputOutUI()
    {
        CheckMouseClickOutUI();
        CheckTouchOutUI();
    }

    void CheckMouseClickOutUI()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                DeselectGender();
            }
        }
    }

    void CheckTouchOutUI()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (!EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                DeselectGender();
            }
        }
    }
}