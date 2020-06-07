using Data;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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
    // Start is called before the first frame update
    void Start()
    {
        var foo = Load(FilePath);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public DialogueData Load(string filePath)
    {
        if (File.Exists(filePath))
        {
            var text = File.ReadAllText(FilePath);
            var jobj = JObject.Parse(text);
            return DataExtensions.FromJObject(jobj);

        //    BinaryFormatter bf = new BinaryFormatter();
        //    using (FileStream stream = new FileStream(filePath, FileMode.Open))
        //    {
        //        var jobj = JObject.Parse((string)bf.Deserialize(stream));
        //        return DataExtensions.FromJObject(jobj);
        //    }

        }

        return null;
    }

}
