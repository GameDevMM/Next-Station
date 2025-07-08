using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class IntroController : MonoBehaviour
{
    public enum Gender
    {
        None,
        Male,
        Female
    }

    [Header ("Animators Controller")]
    public Animator femaleAnimator;
    public Animator maleAnimator;

    [Header("Buttons Controller")]
    public Button maleButton;
    public Button femaleButton;
    public Button selectGenderButton;

    public Gender selectedGender { get; private set; } = Gender.None;
    
    void Start()
    {
        selectGenderButton.interactable = false;

        maleButton.onClick.AddListener(() => SelectGender(Gender.Male));
        femaleButton.onClick.AddListener(() => SelectGender(Gender.Female));
    }

    void Update()
    {
        CheckMouseClickOutUI();
        CheckTouchOutUI();
    }

    void SelectGender(Gender gender)
    {
        if (gender == selectedGender)
        {
            return;
        }

        selectedGender = gender;
        selectGenderButton.interactable = true;

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
        selectGenderButton.interactable = false;

        maleAnimator.Play("M_Idle");
        femaleAnimator.Play("F_Idle");
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
