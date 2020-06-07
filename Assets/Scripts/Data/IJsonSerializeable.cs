using Newtonsoft.Json.Linq;
namespace Data
{
    public interface IJsonSerializeable
    {
        JObject ToJObject();
        void PopulateFromJObject(JToken token);
    }
}