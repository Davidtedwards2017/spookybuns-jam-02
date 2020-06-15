using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Data
{
    public static class DataExtensions
    {
        public static Type GetNodeType(this string name)
        {
            switch (name.ToLower())
            {
                case "basic": return typeof(BasicDialogueEntry);
                case "choice": return typeof(ChoiceDialogueEntry);
                case "condition": return typeof(Conditional);
            }

            return null;
        }

        public static List<DialogueData> NodesFromJObject(JObject jObj)
        {
            var collections = new List<DialogueData>();
            JArray collectionJArray = (JArray)jObj["collection"];
            foreach (var entry in collectionJArray)
            {
                var dialogueData = new DialogueData();
                dialogueData.Id = entry.Value<string>("id");

                JArray jArray = (JArray)entry["dialogues"];
                foreach (var token in jArray)
                {
                    var typePropertyText = token.Value<string>("type");
                    var type = typePropertyText.GetNodeType();
                    if (type == null) continue;

                    var instance = (Node)Activator.CreateInstance(type);
                    instance.PopulateFromJObject(token);
                    dialogueData.nodes.Add(instance);
                }

                collections.Add(dialogueData);
            }

            return collections;
        }
    }

}
