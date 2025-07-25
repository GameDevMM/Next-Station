using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI References")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI speakerText;
    [SerializeField] private TextMeshProUGUI bodyText;
    [SerializeField] private Transform choicesContainer;
    [SerializeField] private Button choiceButtonPrefab;

    [Header("Dialogue Data")]
    [SerializeField] private TextAsset dialogueJSON;

    private Dictionary<string, DialogueLine> dialogueMap;
    private string currentLineId;
    private string playerName;

    private DialogueTyper dialogueTyper;

    private bool isTyping = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        dialogueTyper = bodyText.GetComponent<DialogueTyper>();

        LoadDialogue();
    }

    private void LoadDialogue()
    {
        var lines = JsonUtility.FromJson<DialogueWrapper>("{\"lines\":" + dialogueJSON.text + "}");
        dialogueMap = new Dictionary<string, DialogueLine>();
        foreach (var line in lines.lines)
        {
            dialogueMap[line.id] = line;
        }
    }

    public void StartDialogue(string startId)
    {
        dialoguePanel.SetActive(true);
        ShowLine(startId);
    }

    private void ShowLine(string id)
    {
        if (!dialogueMap.ContainsKey(id))
        {
            return;
        }

        ClearChoices();

        currentLineId = id;
        var line = dialogueMap[id];

        speakerText.text = line.speaker;
        string textToShow = line.text.Replace("{playerName}", playerName);

        isTyping = true;
        canAdvance = false;

        dialogueTyper.StartTyping(textToShow, OnTypingComplete);

        if (line.choices != null && line.choices.Length > 0)
        {
            foreach (var choice in line.choices)
            {
                var btn = Instantiate(choiceButtonPrefab, choicesContainer);
                btn.GetComponentInChildren<TextMeshProUGUI>().text = choice.text;
                btn.onClick.AddListener(() =>
                {
                    if (!string.IsNullOrEmpty(choice.action))
                        RunAction(choice.action);
                    if (!string.IsNullOrEmpty(choice.next))
                        ShowLine(choice.next);
                });
            }
        }
        else
        {
            nextLineId = null;

            if (!string.IsNullOrEmpty(line.next))
            {
                nextLineId = line.next;
            }
        }
    }

    private void ClearChoices()
    {
        foreach (Transform child in choicesContainer)
            Destroy(child.gameObject);
    }

    private string nextLineId = null;
    private bool canAdvance = false;

    private void OnTypingComplete()
    {
        canAdvance = true;
        isTyping = false;
    }


    public void OnDialogueClick()
    {
        if (choicesContainer.childCount > 0)
        {
            return;
        }

        if (isTyping)
        {
            dialogueTyper.CompleteTyping();
            isTyping = false;
            canAdvance = true;
        }
        else if (canAdvance)
        {
            var line = dialogueMap[currentLineId];

            if (!string.IsNullOrEmpty(line.action))
            {
                RunAction(line.action);

                if (line.action != "open_name_input" && !string.IsNullOrEmpty(nextLineId))
                {
                    ShowLine(nextLineId);
                }
            }
            else if (!string.IsNullOrEmpty(nextLineId))
            {
                ShowLine(nextLineId);
            }
            else
            {
                dialoguePanel.SetActive(false);
            }
        }
    }

    private void RunAction(string action)
    {
        switch (action)
        {
            case "open_name_input":
                UI_IntroController.Instance.ShowNameInput();
                break;

            case "start_game":
                SceneManager.LoadScene("Summer");
                break;
        }
    }

    public void SetPlayerName(string name)
    {
        playerName = name;
    }

    [System.Serializable]
    private class DialogueWrapper
    {
        public DialogueLine[] lines;
    }
}