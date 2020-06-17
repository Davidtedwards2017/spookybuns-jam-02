using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationModule : Module
{
    public DialogueModule ActorDialogueModule;
    public DialogueModule PlayerDialogueModule;
    public ChoiceDialogueModule ChoiceDialogueModule;
    public Animator Animator;


    protected override void OnActivated()
    {
        Animator.SetBool("choice", false);
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
            var basicDialogue = (BasicDialogueEntry)dialogue;
            switch (basicDialogue.Who)
            {
                case "actor":
                    yield return ActorDialogueModule.PerformDialogue(basicDialogue);
                    break;
                case "player":
                    yield return PlayerDialogueModule.PerformDialogue(basicDialogue);
                    break;
            }
        }
        else if(dialogue is ChoiceDialogueEntry)
        {
            Animator.SetBool("choice", true);
            yield return ChoiceDialogueModule.PerformDialogue((ChoiceDialogueEntry)dialogue);
            Animator.SetBool("choice", false);
        }

        yield break;
    }
}
