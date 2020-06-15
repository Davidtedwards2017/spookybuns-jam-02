using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Data
{
    [System.Serializable]
    public class ChoiceDialogueOption : IJsonSerializeable
    {
        public string Id;
        public string Value;
        public string[] Post;
        public bool OnlyOnce = false;
        public string Condition;

        public void PopulateFromJObject(JToken token)
        {
            Id = token.Value<string>("id");
            Value = token.Value<string>("value");
            Post = token.Value<JArray>("post").Values<string>().ToArray();
            Condition = token.Value<string>("condition");
            OnlyOnce = token.Value<bool>("onlyonce");
        }

        public JObject ToJObject()
        {
            throw new System.NotImplementedException();
        }
    }
}