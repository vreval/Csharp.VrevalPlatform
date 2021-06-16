using System.Collections.Generic;

namespace Vreval.Platform.DataObjects.Forms
{
    public class Form
    {
        public int Id;
        public string ProjectId;
        public string Name;
        public string Description;
        public string CreatedFormatted;
        public string UpdatedRelative;
        public Dictionary<string, FormField> Fields;
    }
}