using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Data;

public class DialogueModule : Module
{
    public Button ActionButton;

    public TextTyper Typer;
    private bool _ShouldProceed;
    
    public void Awake()
    {
        ActionButton.onClick.AddListener(ActionButtonPressed);
    }

    protected override void OnActivated()
    {
        UpdatePosition();
        base.OnActivated();
    }

    private void Update()
    {
        if(Active)
        {
            UpdatePosition();
        }
    }

    public IEnumerator PerformDialogue(BasicDialogueEntry dialogueEntry)
    {
        _ShouldProceed = false;
        ActionButton.animator.SetBool("visible", false);
        yield return Typer.PerformTextTyping(dialogueEntry.Value);
        //SetDialogueText(dialogueEntry.Value);
        ActionButton.animator.SetBool("visible", true);
        yield return new WaitUntil(() => _ShouldProceed);
        ActionButton.animator.SetBool("visible", false);
    }

    //private void SetDialogueText(string text)
    //{
    //    TextArea.text = text;
    //    StartCoroutine(UpdateLayoutGroups());
    //}

    private void ActionButtonPressed()
    {
        _ShouldProceed = true;
    }

    public void UpdatePosition()
    {
        var newWorldPos = ConversationController.Instance.ConvoDialogueTransform.position;
        Vector2 screenPos = Camera.main.WorldToScreenPoint(newWorldPos);
        SetPosition(screenPos);
    }

    private IEnumerator UpdateLayoutGroups()
    {
        var layoutGroup = GetComponentInChildren<LayoutGroup>();
        layoutGroup.enabled = false;
        yield return new WaitForEndOfFrame();
        layoutGroup.enabled = true;
    }

}
