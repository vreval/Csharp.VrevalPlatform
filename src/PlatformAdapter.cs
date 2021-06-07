using System;
using System.Collections.Generic;
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
        
        public IRestResponse GetCheckpoints(string projectId, string bearerToken)
        {
            var request = new RestRequest($"/projects/{projectId}/checkpoints", Method.GET);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("X-Auth-Token", Environment.GetEnvironmentVariable("APP_TOKEN"));
            request.AddHeader("Authorization", $"Bearer {bearerToken}");
            
            return _client.Execute(request);
        }

        public IRestResponse PostCheckpoints(string checkpoints, string projectId, string bearerToken)
        {
            var request = new RestRequest($"/projects/{projectId}/checkpoint-collections", Method.POST);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("X-Auth-Token", Environment.GetEnvironmentVariable("APP_TOKEN"));
            request.AddHeader("Authorization", $"Bearer {bearerToken}");

            request.AddParameter("application/json", checkpoints, ParameterType.RequestBody);
            
            return _client.Execute(request);
        }

        public List<Checkpoint> DeserializeCheckpoints(string data)
        {
            return JObject.Parse(data).SelectToken("data")?.ToObject<List<Checkpoint>>(_serializer);
        }

        public string SerializeCheckpoints(List<Checkpoint> checkpoints)
        {
            return "{\"data\":" + JArray.FromObject(checkpoints, _serializer) + "}";
        }
    }
}