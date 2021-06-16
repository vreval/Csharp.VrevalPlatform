using System;
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Vreval.Platform.DataObjects.Forms;

namespace Vreval.Platform
{
    public class FormFieldTemplateConverter : JsonConverter
    {
        private Dictionary<string, Func<JObject, Template>> classMap = new Dictionary<string, Func<JObject, Template>>();

        public FormFieldTemplateConverter()
        {
            classMap["Header"] = o => new Template() {Text = (string) o["text"], TypeName = "Header"};
            classMap["Paragraph"] = o => new Template() {Text = (string) o["text"], TypeName = "Paragraph"};
            classMap["Section"] = o => new Template() {Text = (string) o["text"], TypeName = "Section"};
            classMap["Selection"] = o => new Selection()
            {
                Text = (string) o["text"],
                ShuffleOptions = (bool) o["shuffle_options"],
                TypeName = "Selection"
            };
            classMap["Rating"] = o => new Rating()
            {
                Text = (string) o["text"],
                Levels = (int) o["levels"],
                Options = o["options"].ToObject<RatingOption[]>(),
                Symbols = (string) o["symbols"],
                Required = (bool) o["required"],
                LevalsMax = (int) o["levels_max"],
                ShowLabels = (string) o["show_labels"],
                TypeName = "Rating"
            };
        }
        
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject o = JObject.Load(reader);
            var type = (string)o["type_name"] ?? "Header";
            return classMap[type](o);
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }
    }
}