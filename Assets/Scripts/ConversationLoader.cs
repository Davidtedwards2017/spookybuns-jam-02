using Data;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;

public class ConversationLoader : Singleton<ConversationLoader>
{
    private const string FILE_PATTERN = "conversation_*.json";

    public List<DialogueData> ConversationData = new List<DialogueData>();

    // Start is called before the first frame update
    void Awake()
    {
        ConversationData = new List<DialogueData>();
        var conversationFiles = Directory.GetFiles(Application.streamingAssetsPath, FILE_PATTERN);
        foreach(var file in conversationFiles)
        {
            ConversationData.AddRange(Load(file));
        }
    }

    public DialogueData GetCharacterConversation(GameInfo.Character character)
    {
        return ConversationData.FirstOrDefault(c => c.Id.Equals(character.ToString()));
    }

    public List<DialogueData> Load(string filePath)
    {
        if (File.Exists(filePath))
        {
            var text = File.ReadAllText(filePath);
            var jobj = JObject.Parse(text);
            return DataExtensions.NodesFromJObject(jobj);
        }

        return null;
    }

}
