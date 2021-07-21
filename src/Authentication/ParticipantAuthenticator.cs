using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using Vreval.Platform.DataObjects;
using Vreval.Platform.DataObjects.Results;

namespace Vreval.Platform.Authentication
{
    public class ParticipantAuthenticator
    {
        private readonly RestClient _client;
        private readonly JsonSerializer _serializer;

        public ParticipantAuthenticator(string baseUri)
        {
            _client = new RestClient(baseUri);
            _serializer = new JsonSerializer
            {
                ContractResolver = new VrevalDataObjectResolver(),
                TypeNameHandling = TypeNameHandling.Auto,
                NullValueHandling = NullValueHandling.Ignore
            };
        }
        
        public async Task<ParticipantData> GetParticipantData(string participantCode)
        {
            var request = new RestRequest($"/codes/{participantCode}", Method.GET);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("X-Auth-Token", Environment.GetEnvironmentVariable("APP_TOKEN"));

            var result = await _client.ExecuteAsync<string>(request);

            return JObject.Parse(result.Data).SelectToken("data")?.ToObject<ParticipantData>(_serializer);
        }

        public async Task<EvaluationData> GetEvaluationData(ParticipantData participantData)
        {
            var request = new RestRequest($"/evaluations/{participantData.EvaluationId}/codes/{participantData.Code}", Method.GET);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("X-Auth-Token", Environment.GetEnvironmentVariable("APP_TOKEN"));

            var result = await _client.ExecuteAsync<string>(request);

            return JObject.Parse(result.Data).SelectToken("data")?.ToObject<EvaluationData>(_serializer);
        }

        public async Task<Project> GetProjectSnapshot(EvaluationData evaluationData)
        {
            var request = new RestRequest($"/evaluations/{evaluationData.Id}/project-snapshot", Method.GET);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("X-Auth-Token", Environment.GetEnvironmentVariable("APP_TOKEN"));

            var result = await _client.ExecuteAsync<string>(request);

            return JObject.Parse(result.Data).SelectToken("project_data")?.ToObject<Project>(_serializer);
        }

        public async Task<string> GetClassification(EvaluationData evaluationData, Classification classification)
        {
            var request = new RestRequest($"/evaluations/{evaluationData.Id}/classifications", Method.POST);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("X-Auth-Token", Environment.GetEnvironmentVariable("APP_TOKEN"));
            request.AddHeader("Authorization", $"Bearer {evaluationData.Token}");

            request.AddParameter("application/json", JObject.FromObject(classification, _serializer), ParameterType.RequestBody);

            var result = await _client.ExecuteAsync<string>(request);

            return result.Data;
        }

        public async Task<string> ClassifyParticipant(Classification classification)
        {
            var participationData = await GetParticipantData(classification.Code);
            var evaluationData = await GetEvaluationData(participationData);
            
            return await GetClassification(evaluationData, classification);
        }

        public async Task<string> Login(string participantCode, string bearerToken)
        {
            var request = new RestRequest($"/codes/{participantCode}/login", Method.POST);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("X-Auth-Token", Environment.GetEnvironmentVariable("APP_TOKEN"));
            request.AddHeader("Authorization", $"Bearer {bearerToken}");
            
            var result = await _client.ExecuteAsync<string>(request);

            return result.Data;
        }
        
        public async Task<string> Logout(string participantCode, string bearerToken)
        {
            var request = new RestRequest($"/codes/{participantCode}/login", Method.DELETE);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("X-Auth-Token", Environment.GetEnvironmentVariable("APP_TOKEN"));
            request.AddHeader("Authorization", $"Bearer {bearerToken}");
            
            var result = await _client.ExecuteAsync<string>(request);

            return result.Data;
        }

        public async Task<string> MakeParticipantLogEntry(string code, string bearerToken, ParticipantLogEntry entry)
        {
            var request = new RestRequest($"/codes/{code}/log", Method.POST);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("X-Auth-Token", Environment.GetEnvironmentVariable("APP_TOKEN"));
            request.AddHeader("Authorization", $"Bearer {bearerToken}");
            
            request.AddParameter("application/json", JObject.FromObject(entry, _serializer), ParameterType.RequestBody);
            
            var result = await _client.ExecuteAsync<string>(request);

            return result.Data;
        }

        public async Task<string> SetParticipantAsComplete(string code, string bearerToken)
        {
            var request = new RestRequest($"/codes/{code}/complete", Method.POST);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("X-Auth-Token", Environment.GetEnvironmentVariable("APP_TOKEN"));
            request.AddHeader("Authorization", $"Bearer {bearerToken}");
            
            var result = await _client.ExecuteAsync<string>(request);

            return result.Data;
        }

        public async Task<string> AddResult(TaskResult taskResult, string code, string bearerToken, Action<IRestResponse<string>> callback)
        {
            var request = new RestRequest($"/codes/{code}/results", Method.POST);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("X-Auth-Token", Environment.GetEnvironmentVariable("APP_TOKEN"));
            request.AddHeader("Authorization", $"Bearer {bearerToken}");
            
            request.AddParameter("application/json", JObject.FromObject(taskResult, _serializer), ParameterType.RequestBody);
            
            var result = await _client.ExecuteAsync<string>(request);
            callback(result);

            return result.Data;
        }
    }
}