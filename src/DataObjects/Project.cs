using System.Collections.Generic;
using Newtonsoft.Json;

namespace Vreval.Platform.DataObjects
{
    public class Project
    {
        public string DateFormatted;
        public string Description;
        public string Id;
        [JsonProperty("checkpoints")] public Dictionary<string, Marker> Markers;
        public Dictionary<string, ModelBundle> Models;
        public string Name;
        public string UpdatedRelative;
    }
}