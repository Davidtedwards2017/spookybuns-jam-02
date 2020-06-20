using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace Data
{
    public class ReincarnationAsset : IJsonSerializeable
    {
        public string Id;
        public string Description;
        public string[] Conditions;
        public Sprite Sprite;

        public void PopulateFromJObject(JToken token)
        {
            Id = token.Value<string>("id");
            Description = token.Value<string>("description");
            Conditions = token.Value<JArray>("conditions").Values<string>().ToArray();

            var imageUrl = token.Value<string>("imageurl");

            Sprite = loadSpriteFromFile(Path.Combine(Application.streamingAssetsPath, imageUrl));
        }

        private static Sprite loadSpriteFromFile(string filePath)
        {
            Texture2D tex = new Texture2D(2, 2);
            byte[] imgData = File.ReadAllBytes(filePath);

            //Load raw Data into Texture2D 
            tex.LoadImage(imgData);

            //Convert Texture2D to Sprite
            Vector2 pivot = new Vector2(0.5f, 0.5f);
            return Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), pivot, 100.0f);
        }

        public JObject ToJObject()
        {
            throw new System.NotImplementedException();
        }
    }
}