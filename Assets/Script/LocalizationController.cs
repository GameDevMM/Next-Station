using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LocalizationController : MonoBehaviour
{
    public TextMeshProUGUI playButtonMainMenuText;
    public TextMeshProUGUI optionsButtonMainMenuText;
    public TextMeshProUGUI creditsButtonMainMenuText;
    public TextMeshProUGUI quitButtonMainMenuText;

    public TextMeshProUGUI languageButtonOptionsMenuText;

    private void Update()
    {
        playButtonMainMenuText.text = LocalizationManager.Instance.Get("main_menu_play");
        optionsButtonMainMenuText.text = LocalizationManager.Instance.Get("main_menu_options");
        creditsButtonMainMenuText.text = LocalizationManager.Instance.Get("main_menu_credits");
        quitButtonMainMenuText.text = LocalizationManager.Instance.Get("main_menu_quit");
        languageButtonOptionsMenuText.text = LocalizationManager.Instance.Get("option_menu_language");
    }
}
