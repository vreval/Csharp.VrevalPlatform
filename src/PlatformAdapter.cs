using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using Vreval.Platform.DataObjects;

namespace Vreval.Platform
{
    public class PlatformAdapter
    {
        private readonly RestClient _client;
        private readonly JsonSerializer _serializer;
        
        public PlatformAdapter(string baseUri)
        {
            _client = new RestClient(baseUri);
            _serializer = new JsonSerializer()
            {
                ContractResolver = new VrevalDataObjectResolver()
            };
        }

        public IRestResponse GetComponentDefaults(string componentSlug = "")
        {
            var request = new RestRequest($"/component-defaults", Method.GET);
            request.AddHeader("Accept", "application/json");
            request.AddParameter("like", componentSlug);

            return _client.Execute(request);
        }
        
        public IRestResponse GetMarkers(string projectId, string bearerToken)
        {
            var request = new RestRequest($"/projects/{projectId}/checkpoints", Method.GET);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("X-Auth-Token", Environment.GetEnvironmentVariable("APP_TOKEN"));
            request.AddHeader("Authorization", $"Bearer {bearerToken}");
            
            return _client.Execute(request);
        }

        public async Task<List<Marker>> GetMarkersAsync(string projectId, string bearerToken)
        {
            var request = new RestRequest($"/projects/{projectId}/checkpoints", Method.GET);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("X-Auth-Token", Environment.GetEnvironmentVariable("APP_TOKEN"));
            request.AddHeader("Authorization", $"Bearer {bearerToken}");

            var result = await _client.ExecuteAsync<string>(request);
            
            return DeserializeMarkers(result.Content);
        }

        public IRestResponse PostMarkers(string checkpoints, string projectId, string bearerToken)
        {
            var request = new RestRequest($"/projects/{projectId}/checkpoint-collections", Method.POST);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("X-Auth-Token", Environment.GetEnvironmentVariable("APP_TOKEN"));
            request.AddHeader("Authorization", $"Bearer {bearerToken}");

            request.AddParameter("application/json", checkpoints, ParameterType.RequestBody);
            
            return _client.Execute(request);
        }

        public async Task<string> PostMarkersAsync(List<Marker> checkpoints, string projectId,
            string bearerToken)
        {
            var request = new RestRequest($"/projects/{projectId}/checkpoint-collections", Method.POST);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("X-Auth-Token", Environment.GetEnvironmentVariable("APP_TOKEN"));
            request.AddHeader("Authorization", $"Bearer {bearerToken}");

            request.AddParameter("application/json", SerializeMarkers(checkpoints), ParameterType.RequestBody);
            var result = await _client.ExecuteAsync<string>(request);
            
            return result.Content;
        }

        public List<Marker> DeserializeMarkers(string data)
        {
            return JObject.Parse(data).SelectToken("data")?.ToObject<List<Marker>>(_serializer);
        }

        public string SerializeMarkers(List<Marker> checkpoints)
        {
            return "{\"data\":" + JArray.FromObject(checkpoints, _serializer) + "}";
        }
    }
}