using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueModule : Module
{
    public Text TextArea;

    public void SetDialogue(string text)
    {
        TextArea.text = text;
    }
}
