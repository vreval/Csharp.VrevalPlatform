using Newtonsoft.Json;

namespace Vreval.Platform.DataObjects.Scenarios
{
    public class WayfindingWaypoint : Waypoint
    {
        [JsonProperty("is_optional")]
        public bool IsOptional;
    }
}