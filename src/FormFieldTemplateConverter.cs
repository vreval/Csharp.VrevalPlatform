using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Vreval.Platform.DataObjects.Forms;

namespace Vreval.Platform
{
    public class FormFieldTemplateConverter : JsonConverter
    {
        private readonly Dictionary<string, Func<JObject, Template>> _classMap = new Dictionary<string, Func<JObject, Template>>();

        public FormFieldTemplateConverter()
        {
            _classMap["Header"] = o => new Template() {Text = (string) o["text"], TypeName = "Header"};
            _classMap["Paragraph"] = o => new Template() {Text = (string) o["text"], TypeName = "Paragraph"};
            _classMap["Section"] = o => new Template() {Text = (string) o["text"], TypeName = "Section"};
            _classMap["Selection"] = o => new Selection()
            {
                Text = (string) o["text"],
                ShuffleOptions = (bool) o["shuffle_options"],
                TypeName = "Selection"
            };
            _classMap["Rating"] = o => new Rating()
            {
                Text = (string) o["text"],
                Levels = (int) o["levels"],
                Options = o["options"].ToObject<RatingOption[]>(),
                Symbols = (RatingSymbols) Enum.Parse(typeof(RatingSymbols), (string) o["symbols"] ?? "asc"),
                Required = (bool) o["required"],
                LevelsMax = (int) o["levels_max"],
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
            return _classMap[type](o);
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }
    }
}