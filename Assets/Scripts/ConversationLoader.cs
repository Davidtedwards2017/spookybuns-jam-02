﻿using Data;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ConversationLoader : Singleton<ConversationLoader>
{
    public string FilePath
    {
        get
        {
            return Application.dataPath + "/StreamingAssets/" + "basic_conversation.json";
        }
    }

    public List<DialogueData> ConversationData = new List<DialogueData>();

    // Start is called before the first frame update
    void Awake()
    {
        ConversationData.Add(Load(FilePath));
    }

    public DialogueData Load(string filePath)
    {
        if (File.Exists(filePath))
        {
            var text = File.ReadAllText(FilePath);
            var jobj = JObject.Parse(text);
            return DataExtensions.NodesFromJObject(jobj);
        }

        return null;
    }

}
