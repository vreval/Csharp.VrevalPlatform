using Newtonsoft.Json;

namespace Vreval.Platform.DataObjects.Scenarios
{
    public class Waypoint
    {
        [JsonProperty("checkpoint_id")]
        public int CheckpointId;
        [JsonProperty("form_id")]
        public int FormId;
        public string Name;
    }
}