using Newtonsoft.Json;

namespace Vreval.Platform.DataObjects.Scenarios
{
    public class Task
    {
        public int Id;
        [JsonProperty("uuid")]
        public string Uuid;
        public int TypeId;
        public int FieldIndex;
        public Template Template;
    }
}