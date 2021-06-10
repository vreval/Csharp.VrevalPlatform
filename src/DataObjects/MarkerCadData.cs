namespace Vreval.Platform.DataObjects
{
    public class MarkerCadData
    {
        public int CadId = 0;
        public float[] Position = {0f,0f,0f};
        public Unit PositionUnit = Unit.meters;
        public float ProjectRotation = 0f;
        public Unit ProjectRotationUnit = Unit.degrees;
        public float[] Rotation = {0f,0f,0f};
        public Unit RotationUnit = Unit.degrees;
        public float[] SurveyPoint = {0f,0f,0f};
        public MarkerType Type = MarkerType.Checkpoint;
        public string MarkerName = "Default Marker";
        public string MarkerDescription = "";
    }
}