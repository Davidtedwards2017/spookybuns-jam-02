using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Data
{
    public class Conditional : Node
    {
        public string Condition;
        public string[] If;
        public string[] Else;

        public override void PopulateFromJObject(JToken token)
        {
            base.PopulateFromJObject(token);

            Condition = token.Value<string>("condition");
            If = token.Value<JArray>("if").Values<string>().ToArray();
            Else = token.Value<JArray>("else").Values<string>().ToArray();
        }
    }
}