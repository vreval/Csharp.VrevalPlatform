using Newtonsoft.Json;

namespace Vreval.Platform.DataObjects.Forms
{
    public class RatingOption
    {
        [JsonProperty("label_a")]
        public string LabelA;
        [JsonProperty("label_b")]
        public string LabelB;
    }
}