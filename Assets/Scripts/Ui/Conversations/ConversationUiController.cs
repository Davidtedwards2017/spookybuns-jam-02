using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationUiController : UiController<ConversationUiController>
{
    public ConversationModule Module;
    public IEnumerator PerformDialogue(Node dialogue)
    {
        yield return Module.PerformDialogue(dialogue);
    }
}
