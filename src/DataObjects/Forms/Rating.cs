namespace Vreval.Platform.DataObjects.Forms
{
    public class Rating : Template
    {
        public int Levels;
        public RatingOption[] Options;
        public RatingSymbols Symbols;
        public bool Required;
        public int LevelsMax;
        public string ShowLabels;
    }

    public enum RatingSymbols
    {
        asc, desc, mirrored, none, pos_neg
    }
}