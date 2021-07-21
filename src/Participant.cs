using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using Vreval.Platform.Authentication;
using Vreval.Platform.DataObjects;
using Vreval.Platform.DataObjects.Results;
using Vreval.Platform.DataObjects.Scenarios;
using Task = System.Threading.Tasks.Task;

namespace Vreval.Platform
{
    public class Participant : IProfile, IClassifiable
    {
        private readonly ParticipantAuthenticator _authenticator;
        
        private ParticipantData _data;
        private EvaluationData _evaluationData;
        private Project _project;
        private bool _isInitialized;

        public Participant(string code, ParticipantAuthenticator participantAuthenticator)
        {
            Code = code;
            _authenticator = participantAuthenticator;
        }

        public int Id => _data.Id;
        public string Code { get; }
        public bool IsClassified => _data.IsClassified;
        public string ClassificationQuestion => _evaluationData.Questions[0].Question;
        public string[] ClassificationAnswers => _evaluationData.Questions[0].Options;
        public Project Project => _project;
        public AccessToken AccessToken => new AccessToken($"{_project.Id}.{_evaluationData.Token}");

        public Classification GetClassification(string[] answers)
        {
            return new Classification()
            {
                Answers = answers,
                Code = Code,
                SnapshotId = _evaluationData.SnapshotId
            };
        }
        
        public async Task Initialize()
        {
            if (_isInitialized) throw new Exception("Participant already initialized");
            _isInitialized = true;

            _data = await _authenticator.GetParticipantData(Code);
            _evaluationData = await _authenticator.GetEvaluationData(_data);
            _project = await _authenticator.GetProjectSnapshot(_evaluationData);
        }

        public Queue<KeyValuePair<Playlist, Scenario[]>> GetGroups()
        {
            if (!_isInitialized) throw new Exception("Participant not initialized yet");
            var playlistIds = _evaluationData.Groups[_data.GroupId.ToString()].PlaylistIds;


            var queue = new Queue<KeyValuePair<Playlist, Scenario[]>>();
            foreach (var playlistId in playlistIds)
            {
                var playlist = _project.Playlists[playlistId.ToString()];
                var scenarios = playlist.Scenarios.Select(item => _project.Scenarios[item.ToString()]).ToArray();
                queue.Enqueue(new KeyValuePair<Playlist, Scenario[]>(playlist, scenarios));
            }

            return queue;
        }

        public bool HasCompletedTask(string code)
        {
            return _data.CompletedTasks.Contains(code);
        }

        public async Task<string> Classify(Classification classification)
        {
            if (!_isInitialized) throw new Exception("Participant not initialized yet");

            return await _authenticator.ClassifyParticipant(classification);
        }
        
        public async Task<string> Login()
        {
            if (!_isInitialized) throw new Exception("Participant not initialized yet");

            return await _authenticator.Login(_data.Code, _evaluationData.Token);
        }
        
        public async Task<string> Logout()
        {
            if (!_isInitialized) throw new Exception("Participant not initialized yet");

            return await _authenticator.Logout(_data.Code, _evaluationData.Token);
        }

        public async Task<string> MakeLogEntry(string content)
        {
            if (!_isInitialized) throw new Exception("Participant not initialized yet");

            var entry = new ParticipantLogEntry()
            {
                Content = content,
                Context = "client"
            };

            return await _authenticator.MakeParticipantLogEntry(_data.Code, _evaluationData.Token, entry);
        }

        public async Task<string> Complete()
        {
            if (!_isInitialized) throw new Exception("Participant not initialized yet");

            return await _authenticator.SetParticipantAsComplete(_data.Code, _evaluationData.Token);
        }

        public async Task<string> AddTaskResult(TaskResult taskResult, Action<IRestResponse<string>> callback)
        {
            if (!_isInitialized) throw new Exception("Participant not initialized yet");

            return await _authenticator.AddResult(taskResult, _data.Code, _evaluationData.Token, callback);
        }
    }
}