using System;
using Newtonsoft.Json;

namespace Vreval.Platform.DataObjects.Forms
{
    public class FormFieldResultConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var data = (FormFieldResult) value;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }
    }
}