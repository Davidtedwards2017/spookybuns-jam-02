using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Data;

public class DialogueModule : Module
{
    public Button ActionButton;
    public bool ShouldUpdatePosition = true;

    public TextTyper Typer;
    private bool _ShouldProceed;
    
    public void Awake()
    {
        ActionButton.onClick.AddListener(ActionButtonPressed);
    }

    protected override void OnActivated()
    {
        if(ShouldUpdatePosition)
        {
            UpdatePosition();
        }
        base.OnActivated();
    }

    private void Update()
    {
        if(Active && ShouldUpdatePosition)
        {
            UpdatePosition();
        }
    }

    public IEnumerator PerformDialogue(BasicDialogueEntry dialogueEntry)
    {
        ActionButton.animator.SetBool("visible", false);
        Animator.SetBool("visible", true);
        _ShouldProceed = false;
        yield return Typer.PerformTextTyping(dialogueEntry.Value);
        //StartCoroutine(UpdateLayoutGroups());
        //SetDialogueText(dialogueEntry.Value);
        ActionButton.animator.SetBool("visible", true);
        yield return new WaitUntil(() => _ShouldProceed);
        ActionButton.animator.SetBool("visible", false);
        Animator.SetBool("visible", false);
    }

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
        yield return new WaitForFixedUpdate();
//        yield return new WaitForEndOfFrame();
        layoutGroup.enabled = true;
    }

}
