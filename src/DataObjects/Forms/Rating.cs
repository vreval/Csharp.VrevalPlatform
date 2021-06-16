namespace Vreval.Platform.DataObjects.Forms
{
    public class Rating : Template
    {
        public int Levels;
        public RatingOption[] Options;
        public string Symbols;
        public bool Required;
        public int LevalsMax;
        public string ShowLabels;
    }
}