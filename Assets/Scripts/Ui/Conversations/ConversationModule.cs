using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationModule : Module
{
    public DialogueModule DialogueModule;
    public ChoiceDialogueModule ChoiceDialogueModule;
    public Animator Animator;


    protected override void OnActivated()
    {
        Animator.SetBool("visible", true);
        base.OnActivated();
    }

    protected override void OnDeactivated()
    {
        Animator.SetBool("visible", false);
        base.OnDeactivated();
    }

    public IEnumerator PerformDialogue(Node dialogue)
    {
        if(dialogue is BasicDialogueEntry)
        {
            yield return DialogueModule.PerformDialogue((BasicDialogueEntry)dialogue);
        }
        else if(dialogue is ChoiceDialogueEntry)
        {
            yield return ChoiceDialogueModule.PerformDialogue((ChoiceDialogueEntry)dialogue);
        }

        yield break;
    }
}
