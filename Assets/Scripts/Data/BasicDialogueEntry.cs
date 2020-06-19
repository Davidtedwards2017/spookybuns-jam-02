using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Data
{
    [System.Serializable]
    public class BasicDialogueEntry : Node, IDialogueNode
    {
        public string Value;
        public string[] Post;
        public string Who { get; private set; }
        public string Expression { get; private set; }

        public override void PopulateFromJObject(JToken token)
        {
            base.PopulateFromJObject(token);
            Value = token.Value<string>("value");
            Who = token.Value<string>("who");
            Expression = token.Value<string>("expression");
            Post = token.Value<JArray>("post").Values<string>().ToArray();
        }
    }
}