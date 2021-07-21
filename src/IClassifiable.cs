using System.Collections.Generic;
using System.Threading.Tasks;
using Vreval.Platform.Authentication;
using Vreval.Platform.DataObjects;
using Vreval.Platform.DataObjects.Scenarios;

namespace Vreval.Platform
{
    public interface IClassifiable
    {
        bool IsClassified { get; }
        string ClassificationQuestion { get; }
        string[] ClassificationAnswers { get; }
        Classification GetClassification(string[] answers);
        Queue<KeyValuePair<Playlist, Scenario[]>> GetGroups();
        Task<string> Classify(Classification classification);
    }
}