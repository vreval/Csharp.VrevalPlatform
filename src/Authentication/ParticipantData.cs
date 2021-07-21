using Newtonsoft.Json;

namespace Vreval.Platform.Authentication
{
    public class ParticipantData
    {
        public int Id;
        public string Code;
        public int EvaluationId;
        public int EvaluationSnapshotId;
        public int ProjectSnapshotId;
        public int GroupId;
        public ParticipantLogEntry[] Log;
        public string FirstLogin;
        public string LoggedInAt;
        public string Completed;
        public string[] CompletedTasks;
        public bool IsClassified;
    }
}