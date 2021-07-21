using System.Collections.Generic;
using Newtonsoft.Json;
using Vreval.Platform.DataObjects.Forms;
using Vreval.Platform.DataObjects.Scenarios;

namespace Vreval.Platform.DataObjects
{
    public class Project
    {
        public string CreatedFormatted;
        public string Description;
        public string Id;
        public Dictionary<string, Playlist> Playlists;
        public Dictionary<string, Form> Forms;
        [JsonProperty("checkpoints")] public Dictionary<string, Marker> Markers;
        public Dictionary<string, ModelBundle> Models;
        public Dictionary<string, Scenario> Scenarios;
        public string Name;
        public string UpdatedRelative;
    }

    public class Playlist
    {
        public int Id;
        public string Name;
        public string Mode;
        public int[] Scenarios;
    }
}