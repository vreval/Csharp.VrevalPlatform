using Newtonsoft.Json;

namespace Vreval.Platform.DataObjects.Forms
{
    public class RatingResult : FormFieldResult
    {
        [JsonProperty("results")]
        public RatingItemResult[] Results;
    }
}