using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using Data;

public class ReincarnationAssetLoader : Singleton<ReincarnationAssetLoader>
{
    public List<ReincarnationAsset> Reincarnations;

    private const string REINCARNATION_FILE_NAME = "reincarnation.json";

    private void Start()
    {
        LoadReincarnationFile();
    }
    
    private void LoadReincarnationFile()
    {
        var filePath = Path.Combine(Application.streamingAssetsPath, REINCARNATION_FILE_NAME);
        if (File.Exists(filePath))
        {
            var text = File.ReadAllText(filePath);
            var jobj = JObject.Parse(text);
            Reincarnations = LoadReincarnationFile(jobj);
        }
    }

    public static List<ReincarnationAsset> LoadReincarnationFile(JObject jObj)
    {
        var entries = new List<ReincarnationAsset>();
        JArray collectionJArray = (JArray)jObj["entries"];
        foreach (var entry in collectionJArray)
        {           
            var instance = new ReincarnationAsset();
            instance.PopulateFromJObject(entry);
            entries.Add(instance);
        }
        return entries;
    }

}
