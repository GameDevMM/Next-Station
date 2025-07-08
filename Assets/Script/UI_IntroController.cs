using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_IntroController : MonoBehaviour
{
    [Header ("Select Gender UI Controller")]
    public GameObject selectGenderController;
    public GameObject selectGenderUI;

    [Header("Dialogue UI Controller")]
    public GameObject dialogueUI;

    public void ConfirmGender()
    {
        selectGenderController.SetActive(false);
        selectGenderUI.SetActive(false);
        EnableDialogueUI();
    }

    public void DisableDialogueUI()
    {
        dialogueUI.SetActive(false);
    }

    public void EnableDialogueUI()
    {
        dialogueUI.SetActive(true);
    }
}
