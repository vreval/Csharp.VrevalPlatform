using Newtonsoft.Json;

namespace Vreval.Platform.DataObjects.Scenarios
{
    public class AvatarSetting
    {
        public string Movement;
        [JsonProperty("movement_speed")]
        public int MovementSpeed;
        public bool Footsteps;
    }
}