using Newtonsoft.Json;

namespace Vreval.Platform.DataObjects.Scenarios
{
    public class Model
    {
        [JsonProperty("id")]
        public int Id;
        [JsonProperty("model_id")]
        public int ModelId;
        [JsonProperty("name")]
        public string Name;
        [JsonProperty("attached_model")]
        public string AttachedModel;
    }
}