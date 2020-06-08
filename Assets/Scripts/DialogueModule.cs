using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Data;

public class DialogueModule : Module
{
    public Button ActionButton;
    public Text TextArea;
    private bool _ShouldProceed;
    
    public void Awake()
    {
        ActionButton.onClick.AddListener(ActionButtonPressed);
    }

    public IEnumerator PerformDialogue(BasicDialogueEntry dialogueEntry)
    {
        _ShouldProceed = false;
        SetDialogueText(dialogueEntry.Value);
        yield return new WaitUntil(() => _ShouldProceed);
    }

    private void SetDialogueText(string text)
    {
        TextArea.text = text;
    }

    private void ActionButtonPressed()
    {
        _ShouldProceed = true;
        //SendMessageUpwards("DialogueActionButtonClicked");
    }
}
