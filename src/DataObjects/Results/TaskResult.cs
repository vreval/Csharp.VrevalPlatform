namespace Vreval.Platform.DataObjects.Results
{
    public class TaskResult
    {
        public string Uuid;
        public int ParticipantId;
        public int TaskId;
        public bool IsUsingVr;
        public string TaskCode;
        public string CompletedAt;
        public TaskResultData Results;
    }

    public class TaskResultData
    {
        public string CompletedAt;
    }
}