using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationModule : Module
{
    public DialogueModule DialogueModule;
    public ChoiceDialogueModule ChoiceDialogueModule;

    public IEnumerator PerformDialogue(DialogueEntry dialogue)
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
