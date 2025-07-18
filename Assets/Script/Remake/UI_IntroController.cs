using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_IntroController : MonoBehaviour
{
    public static UI_IntroController Instance;

    [SerializeField] private GameObject genderPanel;
    [SerializeField] private GameObject dialoguePanel;

    [Header("Painel de Nome")]
    [SerializeField] private GameObject nameInputPanel;
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private Button confirmNameButton;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void ShowNameInput()
    {
        dialoguePanel.SetActive(false);
        nameInputPanel.SetActive(true);
        confirmNameButton.interactable = false;
        nameInput.text = "";
        nameInput.onValueChanged.AddListener(value =>
        {
            confirmNameButton.interactable = !string.IsNullOrWhiteSpace(value);
        });
    }

    public void ConfirmName()
    {
        string name = nameInput.text;
        DialogueManager.Instance.SetPlayerName(name);
        nameInputPanel.SetActive(false);
        DialogueManager.Instance.StartDialogue("confirm_name");
    }

    public void OnConfirmGender()
    {
        genderPanel.SetActive(false);
        dialoguePanel.SetActive(true);

        // Inicia o diálogo de abertura (você pode alterar essa string se estiver usando chaves no JSON)
        DialogueManager.Instance.StartDialogue("intro_1");
    }
}
