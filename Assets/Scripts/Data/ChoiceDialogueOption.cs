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

        public void PopulateFromJObject(JToken token)
        {
            Id = token.Value<string>("id");
            Value = token.Value<string>("value");
            Post = token.Value<JArray>("post").Values<string>().ToArray();
        }

        public JObject ToJObject()
        {
            throw new System.NotImplementedException();
        }
    }
}