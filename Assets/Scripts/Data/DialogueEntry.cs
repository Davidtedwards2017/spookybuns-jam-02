using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Data
{

    [System.Serializable]
    public abstract class DialogueEntry : IJsonSerializeable
    {
        public string Type;
        public string Id;
        public string Who;

        public JObject ToJObject()
        {
            throw new System.NotImplementedException();
        }

        public virtual void PopulateFromJObject(JToken token)
        {
            Type = token.Value<string>("type");
            Id = token.Value<string>("id");
            Who = token.Value<string>("who");
        }
    }
}