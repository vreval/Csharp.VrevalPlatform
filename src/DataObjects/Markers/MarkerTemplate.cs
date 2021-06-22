using Newtonsoft.Json;

namespace Vreval.Platform.DataObjects
{
    public class MarkerTemplate
    {
        [JsonProperty("cad_data")] public MarkerCadData CadData = new MarkerCadData();
        public float Height = 1.2f;
        public string HeightUnit = Unit.meters.ToString();
        public float PerimeterRadius = 3f;
        public string PerimeterRadiusUnit = Unit.meters.ToString();
        public float PlacementDistance = 1.5f;
        public string PlacementDistanceUnit = Unit.meters.ToString();
    }
}