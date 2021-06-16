using Newtonsoft.Json;

namespace Vreval.Platform.DataObjects.Forms
{
    [JsonConverter(typeof(FormFieldTemplateConverter))]
    public class Template
    {
        public string TypeName;
        public string Text;
    }
}