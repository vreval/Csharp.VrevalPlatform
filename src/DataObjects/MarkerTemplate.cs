namespace Vreval.Platform.DataObjects
{
    public class MarkerTemplate
    {
        public MarkerCadData CadData = new MarkerCadData();
        public string Description = "";
        public float Height = 1.2f;
        public Unit HeightUnit = Unit.meters;
        public float Perimeter = 3f;
        public float PerimeterDistance = 12f;
        public Unit PerimeterDistanceUnit = Unit.meters;
        public Unit PerimeterUnit = Unit.meters;
        public MarkerVisibility Visibility = MarkerVisibility.always;
    }
}