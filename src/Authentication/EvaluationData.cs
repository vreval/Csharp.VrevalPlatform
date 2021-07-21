using System.Collections.Generic;
using Vreval.Platform.DataObjects;

namespace Vreval.Platform.Authentication
{
    public class EvaluationData
    {
        public int Id;
        public string Name;
        public string Token;
        public int SnapshotId;
        public int SnapshotVersion;
        public ClassificationQuestion[] Questions;
        public string EndsOn;
        public Dictionary<string, Group> Groups;
    }
}