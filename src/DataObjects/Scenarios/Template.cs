using System.Collections.Generic;
using Newtonsoft.Json;

namespace Vreval.Platform.DataObjects.Scenarios
{
    [JsonConverter(typeof(TaskTemplateConverter))]
    public class Template
    {
        public string Text;
        public string Description;
        public int CheckpointId;
        public int FormId;
        public int TimeToAnswer;
        public bool AutoOpenForm;
        public bool AllowMultipleVisits;
        public bool Required;
        public bool PositionalTracking;
        public bool HmdTracking;
        public int PositionalTrackingInterval;
        public int HmdTrackingInterval;
        public List<Model> Models;
        public AvatarSetting AvatarSettings;
    }
}