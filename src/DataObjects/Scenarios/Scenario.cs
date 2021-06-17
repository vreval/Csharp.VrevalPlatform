using System.Collections.Generic;

namespace Vreval.Platform.DataObjects.Scenarios
{
    public class Scenario
    {
        public int Id;
        public string Name;
        public string Description;
        public string CreatedFormatted;
        public string UpdatedRelative;
        public Dictionary<string, Task> Tasks;
    }
}