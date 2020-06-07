using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Data
{
    [System.Serializable]
    public class BasicDialogueEntry : DialogueEntry
    {
        public string Value;
        public string[] Post;

        public override void PopulateFromJObject(JToken token)
        {
            base.PopulateFromJObject(token);
            Value = token.Value<string>("value");
            Post = token.Value<JArray>("post").Values<string>().ToArray();
        }
    }
}