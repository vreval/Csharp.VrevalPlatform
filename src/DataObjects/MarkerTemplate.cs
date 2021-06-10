using Newtonsoft.Json;

namespace Vreval.Platform.DataObjects
{
    public class MarkerTemplate
    {
        [JsonProperty("cad_data")] public MarkerCadData CadData = new MarkerCadData();
        public float Height = 1.2f;
        public string HeightUnit = Unit.meters.ToString();
        public float Perimeter = 3f;
        public float PerimeterDistance = 12f;
        public string PerimeterDistanceUnit = Unit.meters.ToString();
        public string PerimeterUnit = Unit.meters.ToString();
        public float PlacementDistance = 1.5f;
        public string PlacementDistanceUnit = Unit.meters.ToString();
        public string Visibility = MarkerVisibility.always.ToString();
    }
}