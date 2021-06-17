using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Vreval.Platform.DataObjects.Scenarios
{
    public class TaskTemplateConverter : JsonConverter
    {
        private readonly Dictionary<string, Func<JObject, Template>> _classMap = new Dictionary<string, Func<JObject, Template>>();

        public TaskTemplateConverter()
        {
            _classMap["Placing"] = CreatePlacing;
            _classMap["Annotation"] = CreateAnnotation;
            _classMap["Questionnaire"] = CreateQuestionnaire;
            _classMap["AB Test"] = CreateAbTest;
            _classMap["Wayfinding"] = CreateWayfinding;
        }

        private T CreateTemplate<T>(JObject jObject) where T : Template, new()
        {
            return new T()
            {
                Text = (string) jObject["text"],
                Description = (string) jObject["description"],
                CheckpointId = (int) jObject["checkpoint_id"],
                FormId = (int) jObject["form_id"],
                TimeToAnswer = (int) jObject["time_to_answer"],
                AutoOpenForm = (bool) jObject["auto_open_form"],
                AllowMultipleVisits = (bool) jObject["allow_multiple_visits"],
                Required = (bool) jObject["required"],
                PositionalTracking = (bool) jObject["positional_tracking"],
                HmdTracking = (bool) jObject["hmd_tracking"],
                Models = jObject["models"].ToObject<List<Model>>(),
                AvatarSettings = jObject["avatar_settings"].ToObject<AvatarSetting>(),
            };
        }

        private Template CreateAbTest(JObject jObject)
        {
            var template = CreateTemplate<AbTest>(jObject);
            template.Waypoints = jObject["waypoints"].ToObject<List<AbTestWaypoint>>();
            
            return template;
        }

        private Template CreateQuestionnaire(JObject jObject)
        {
            var template = CreateTemplate<Questionnaire>(jObject);
            template.Waypoints = jObject["waypoints"].ToObject<List<Waypoint>>();

            return template;
        }

        private Template CreateAnnotation(JObject jObject)
        {
            var template = CreateTemplate<Annotation>(jObject);
            template.Amount = (int) jObject["amount"];
            template.AnnotationFormId = (int) jObject["annotation_form_id"];

            return template;
        }

        private Template CreateWayfinding(JObject jObject)
        {
            var template = CreateTemplate<Wayfinding>(jObject);
            template.Waypoints = jObject["waypoints"].ToObject<List<WayfindingWaypoint>>();
            template.Destinations = jObject["destinations"].ToObject<List<WayfindingWaypoint>>();
            template.MultipleDestinations = (bool) jObject["multiple_destinations"];
            template.VisitInOrder = (bool) jObject["visit_in_order"];

            return template;
        }

        private Template CreatePlacing(JObject jObject)
        {
            var template = CreateTemplate<Placing>(jObject);
            template.Amount = (int) jObject["amount"];
            template.PlacingFormId = (int) jObject["placing_form_id"];

            return template;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject o = JObject.Load(reader);
            var type = (string)o["type_name"] ?? "Placing";
            return _classMap[type](o);
        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }
    }
}