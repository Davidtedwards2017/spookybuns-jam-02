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

    public IEnumerator PerformDialogue(IDialogueNode dialogue, Character_IC ic)
    {
        if(dialogue is BasicDialogueEntry)
        {
            var basicDialogue = (BasicDialogueEntry)dialogue;
            switch (basicDialogue.Who)
            {
                case "actor":
                    yield return ActorDialogueModule.PerformDialogue(basicDialogue, ic.TypingSounds);
                    break;
                case "player":
                    yield return PlayerDialogueModule.PerformDialogue(basicDialogue,
                        MasterGameStateController.Instance.PlayerTypingSounds);
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
