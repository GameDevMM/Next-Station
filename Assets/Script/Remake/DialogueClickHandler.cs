using UnityEngine;
using UnityEngine.EventSystems;

public class DialogueClickHandler : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        if (DialogueManager.Instance != null)
            DialogueManager.Instance.OnDialogueClick();
        else
            Debug.LogWarning("DialogueManager.Instance está null");
    }
}