using Newtonsoft.Json;

namespace Vreval.Platform.DataObjects.Scenarios
{
    public class AbTestWaypoint : Waypoint
    {
        [JsonProperty("post_form_id")]
        public int PostFormId;
        [JsonProperty("model_options")]
        public Model[] ModelOptions;
    }
}