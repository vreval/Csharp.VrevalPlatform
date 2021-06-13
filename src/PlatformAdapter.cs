using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
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
            _serializer = new JsonSerializer
            {
                ContractResolver = new VrevalDataObjectResolver()
            };
        }

        public async Task<KeyValuePair<string, string>> CheckAccessToken(AccessToken accessToken)
        {
            var request = new RestRequest($"/projects/{accessToken.ResourceId}/check-access-token", Method.POST);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("X-Auth-Token", Environment.GetEnvironmentVariable("APP_TOKEN"));
            request.AddHeader("Authorization", $"Bearer {accessToken.BearerToken}");

            var result = await _client.ExecuteAsync<string>(request);

            return new KeyValuePair<string, string>(result.Content, result.StatusCode.ToString());
        }

        public IRestResponse GetComponentDefaults(string componentSlug = "")
        {
            var request = new RestRequest("/component-defaults", Method.GET);
            request.AddHeader("Accept", "application/json");
            request.AddParameter("like", componentSlug);

            return _client.Execute(request);
        }

        public async Task<List<Project>> GetProjectsAsync(AccessToken accessToken)
        {
            var request = new RestRequest($"/projects/{accessToken.ResourceId}", Method.GET);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("X-Auth-Token", Environment.GetEnvironmentVariable("APP_TOKEN"));
            request.AddHeader("Authorization", $"Bearer {accessToken.BearerToken}");

            var result = await _client.ExecuteAsync<string>(request);

            return DeserializeComponents<Project>(result.Content);
        }

        public IRestResponse GetMarkers(AccessToken accessToken)
        {
            var request = new RestRequest($"/projects/{accessToken.ResourceId}/checkpoints", Method.GET);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("X-Auth-Token", Environment.GetEnvironmentVariable("APP_TOKEN"));
            request.AddHeader("Authorization", $"Bearer {accessToken.BearerToken}");

            return _client.Execute(request);
        }

        public async Task<List<Marker>> GetMarkersAsync(AccessToken accessToken)
        {
            var request = new RestRequest($"/projects/{accessToken.ResourceId}/checkpoints", Method.GET);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("X-Auth-Token", Environment.GetEnvironmentVariable("APP_TOKEN"));
            request.AddHeader("Authorization", $"Bearer {accessToken.BearerToken}");

            var result = await _client.ExecuteAsync<string>(request);

            return DeserializeComponents<Marker>(result.Content);
        }

        public IRestResponse PostMarkers(string markers, AccessToken accessToken)
        {
            var request = new RestRequest($"/projects/{accessToken.ResourceId}/checkpoint-collections", Method.POST);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("X-Auth-Token", Environment.GetEnvironmentVariable("APP_TOKEN"));
            request.AddHeader("Authorization", $"Bearer {accessToken.BearerToken}");
            request.AddParameter("application/json", markers, ParameterType.RequestBody);

            return _client.Execute(request);
        }

        public async Task<string> PostMarkersAsync(List<Marker> markers, AccessToken accessToken)
        {
            var request = new RestRequest($"/projects/{accessToken.ResourceId}/checkpoint-collections", Method.POST);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("X-Auth-Token", Environment.GetEnvironmentVariable("APP_TOKEN"));
            request.AddHeader("Authorization", $"Bearer {accessToken.BearerToken}");
            request.AddParameter("application/json", SerializeComponents(markers), ParameterType.RequestBody);

            var result = await _client.ExecuteAsync<string>(request);

            return result.Content;
        }

        public List<T> DeserializeComponents<T>(string data)
        {
            return JObject.Parse(data).SelectToken("data")?.ToObject<List<T>>(_serializer);
        }

        public string SerializeComponents<T>(List<T> components)
        {
            return "{\"data\":" + JArray.FromObject(components, _serializer) + "}";
        }

        public async Task DownloadAssetBundleAsync(string savePath, int modelId, AccessToken token)
        {
            var request = new RestRequest($"/model-downloads/{modelId}", Method.GET);
            request.AddHeader("X-Auth-Token", Environment.GetEnvironmentVariable("APP_TOKEN"));
            request.AddHeader("Authorization", $"Bearer {token.BearerToken}");

            var result = await _client.ExecuteAsync<byte[]>(request);

            if (result.StatusCode != HttpStatusCode.OK)
            {
                Console.WriteLine(result.Content);
                return;
            }

            File.WriteAllBytes(savePath, result.RawBytes);
        }
    }
}