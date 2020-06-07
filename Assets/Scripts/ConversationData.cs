using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ConversationData : MonoBehaviour
{
    public DialogueData[] Dialogues;

    [System.Serializable]
    public class DialogueData
    {
        public string id;
        public string value;
    }
}
