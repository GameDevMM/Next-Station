using System.Collections;
using TMPro;
using UnityEngine;

public class DialogueTyper : MonoBehaviour
{
    [SerializeField] private float typingSpeed = 0.03f;

    private TextMeshProUGUI textComponent;
    private string fullText;
    private Coroutine typingCoroutine;
    private System.Action onComplete;

    private bool isTyping = false;

    private void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    public void StartTyping(string text, System.Action onCompleteCallback = null)
    {
        fullText = text;
        onComplete = onCompleteCallback;
        if (typingCoroutine != null) StopCoroutine(typingCoroutine);
        typingCoroutine = StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        isTyping = true;
        textComponent.text = "";
        for (int i = 0; i <= fullText.Length; i++)
        {
            textComponent.text = fullText.Substring(0, i);
            yield return new WaitForSeconds(typingSpeed);
        }
        isTyping = false;
        onComplete?.Invoke();
    }

    public void CompleteTyping()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        textComponent.text = fullText;
        isTyping = false;
        onComplete?.Invoke();
    }
}
