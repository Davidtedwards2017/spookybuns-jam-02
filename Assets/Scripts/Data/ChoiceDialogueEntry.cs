using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Data
{
    [System.Serializable]
    public class ChoiceDialogueEntry : DialogueEntry
    {
        public ChoiceDialogueOption[] Options;

        public override void PopulateFromJObject(JToken token)
        {
            base.PopulateFromJObject(token);
            
            var optionsJArray = token.Value<JArray>("options");
            var collection = new List<ChoiceDialogueOption>();
            foreach (var optionToken in optionsJArray)
            {
                ChoiceDialogueOption option = new ChoiceDialogueOption();
                option.PopulateFromJObject(optionToken);
                collection.Add(option);
            }

            Options = collection.ToArray();
        }
    }
}
