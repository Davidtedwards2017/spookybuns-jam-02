using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    public static class DataExtensions
    {
        public static Type GetDialogueType(this string name)
        {
            switch (name.ToLower())
            {
                case "basic": return typeof(BasicDialogueEntry);
                case "choice": return typeof(ChoiceDialogueEntry);

            }

            return null;
        }

        public static DialogueData DialogueFromJObject(JObject jObj)
        {
            var dialogueData = new DialogueData();

            dialogueData.Id = jObj.Value<string>("id");

            JArray jArray = (JArray)jObj["dialogues"];
            foreach (var token in jArray)
            {
                var typePropertyText = token.Value<string>("type");
                var type = typePropertyText.GetDialogueType();
                if (type == null) continue;

                var instance = (DialogueEntry)Activator.CreateInstance(type);
                instance.PopulateFromJObject(token);
                dialogueData.dialogues.Add(instance);
            }

            return dialogueData;
        }
    }

}
