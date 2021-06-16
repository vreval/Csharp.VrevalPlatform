using System.Collections.Generic;
using Newtonsoft.Json;
using Vreval.Platform.DataObjects.Forms;

namespace Vreval.Platform.DataObjects
{
    public class Project
    {
        public string DateFormatted;
        public string Description;
        public string Id;
        public Dictionary<string, Form> Forms;
        [JsonProperty("checkpoints")] public Dictionary<string, Marker> Markers;
        public Dictionary<string, ModelBundle> Models;
        public string Name;
        public string UpdatedRelative;
    }
}