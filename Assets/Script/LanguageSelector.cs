using TMPro;
using UnityEngine;

public class LanguageSelector : MonoBehaviour
{
    public TMP_Dropdown languageDropdown;

    void Start()
    {
        if (languageDropdown == null)
        {
            Debug.LogError("Language Dropdown não está atribuído no Inspector!");
            return;
        }

        // Preenche as opções (caso queira criar por código)
        languageDropdown.ClearOptions();
        languageDropdown.AddOptions(new System.Collections.Generic.List<string> { "English", "Português" });

        // Define o valor atual baseado no idioma carregado
        languageDropdown.value = LocalizationManager.Instance.currentLanguage == LocalizationManager.Language.English ? 0 : 1;

        // Adiciona o listener
        languageDropdown.onValueChanged.AddListener(ChangeLanguage);
    }

    void ChangeLanguage(int index)
    {
        switch (index)
        {
            case 0:
                LocalizationManager.Instance.SetLanguage(LocalizationManager.Language.English);
                break;
            case 1:
                LocalizationManager.Instance.SetLanguage(LocalizationManager.Language.Portuguese);
                break;
        }
    }
}