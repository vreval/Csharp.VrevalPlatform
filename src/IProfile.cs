using System;
using System.Threading.Tasks;
using RestSharp;
using Vreval.Platform.DataObjects;
using Vreval.Platform.DataObjects.Results;

namespace Vreval.Platform
{
    public interface IProfile
    {
        int Id { get; }
        string Code { get; }
        Project Project { get; }
        AccessToken AccessToken { get; }
        Task Initialize();
        bool HasCompletedTask(string code);
        Task<string> Login();
        Task<string> Logout();
        Task<string> MakeLogEntry(string content);
        Task<string> Complete();
        Task<string> AddTaskResult(TaskResult taskResult, Action<IRestResponse<string>> callback);
    }
}