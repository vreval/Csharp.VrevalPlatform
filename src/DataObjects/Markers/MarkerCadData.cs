namespace Vreval.Platform.DataObjects
{
    public class MarkerCadData
    {
        public int CadId = 0;
        public string MarkerDescription = "";
        public float[] Position = {0f, 0f, 0f};
        public string PositionUnit = Unit.meters.ToString();
        public float ProjectRotation = 0f;
        public string ProjectRotationUnit = Unit.degrees.ToString();
        public float[] Rotation = {0f, 0f, 0f};
        public string RotationUnit = Unit.degrees.ToString();
        public float[] SurveyPoint = {0f, 0f, 0f};
        public string Type = MarkerType.Checkpoint.ToString();
    }
}