using System.Collections.Generic;

namespace Vreval.Platform.DataObjects
{
    public class ModelBundle
    {
        public int Id;
        public string Name;
        public string Description;
        public string CreatedFormatted;
        public string UpdatedRelative;
        public List<Design> Designs;
    }

    public class Design
    {
        public int Id;
        public string Name;
        public string Description;
        public string[] Tags;
    }
}