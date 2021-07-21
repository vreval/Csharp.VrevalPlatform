using Newtonsoft.Json;

namespace Vreval.Platform.DataObjects.Forms
{
    public class SelectionResult : FormFieldResult
    {
        [JsonProperty("results")]
        public string[] Results;
    }
}