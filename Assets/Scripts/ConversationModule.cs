using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationModule : Module
{
    public DialogueModule DialogueModule;

    public void LoadDialogue(ConversationData.DialogueData dialogue)
    {
        DialogueModule.SetDialogue(dialogue.value);
    }


}
