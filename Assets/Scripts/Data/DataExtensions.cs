using Newtonsoft.Json.Linq;
using System;

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

        public static DialogueData NodesFromJObject(JObject jObj)
        {
            var dialogueData = new DialogueData();

            dialogueData.Id = jObj.Value<string>("id");

            JArray jArray = (JArray)jObj["dialogues"];
            foreach (var token in jArray)
            {
                var typePropertyText = token.Value<string>("type");
                var type = typePropertyText.GetNodeType();
                if (type == null) continue;

                var instance = (Node)Activator.CreateInstance(type);
                instance.PopulateFromJObject(token);
                dialogueData.nodes.Add(instance);
            }

            return dialogueData;
        }
    }

}
