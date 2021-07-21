using Newtonsoft.Json;

namespace Vreval.Platform.DataObjects.Forms
{
    public class RatingItemResult
    {
        [JsonProperty("label_a")]
        public string LabelA;
        [JsonProperty("label_b")]
        public string LabelB;
        [JsonProperty("value")]
        public int Value;
    }
}