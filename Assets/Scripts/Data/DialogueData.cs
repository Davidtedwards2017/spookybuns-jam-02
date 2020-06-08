using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [System.Serializable]
    public class DialogueData
    {
        public string Id;
        public List<DialogueEntry> dialogues = new List<DialogueEntry>();
    }
}