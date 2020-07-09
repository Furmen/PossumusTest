using Application.DTOs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Command
{
    public class CandidateCommand : ICandidateCommand
    {
        public async Task CreateCommandAsync(string baseURL, CandidateDTO candidate)
        {
            var client = new RestClient(baseURL);
            var request = new RestRequest(Method.POST);
            var jobsJson = string.Empty;

            if (candidate.Jobs is IEnumerable<JobDTO> jobs)
                jobsJson = JsonConvert.SerializeObject(jobs);

            JObject jObjectbody = new JObject
            {
                { "CandidateId", candidate.CandidateId },
                { "Name", candidate.Name },
                { "LastName", candidate.LastName },
                { "DateOfBirth", candidate.DateOfBirth },
                { "Email", candidate.Email },
                { "PhoneNumber", candidate.PhoneNumber },
                { "Resume", candidate.Resume },
                { "JobsJson", jobsJson }
            };

            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);

            await client.ExecuteAsync(request);
        }

        public async Task DeleteCommandAsync(string baseURL, int candidateId)
        {
            var client = new RestClient(baseURL + "/" + candidateId);
            var request = new RestRequest(Method.DELETE);
            await client.ExecuteAsync(request);
        }

        public async Task EditCommandAsync(string baseURL, CandidateDTO candidate)
        {
            var client = new RestClient(baseURL);
            var request = new RestRequest(Method.PUT);
            var jobsJson = string.Empty;

            if (candidate.Jobs is IEnumerable<JobDTO> jobs)
                jobsJson = JsonConvert.SerializeObject(jobs);

            JObject jObjectbody = new JObject
            {
                { "CandidateId", candidate.CandidateId },
                { "Name", candidate.Name },
                { "LastName", candidate.LastName },
                { "DateOfBirth", candidate.DateOfBirth },
                { "Email", candidate.Email },
                { "PhoneNumber", candidate.PhoneNumber },
                { "Resume", candidate.Resume },
                { "JobsJson", jobsJson }
            };

            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);

            await client.ExecuteAsync(request);
        }
    }
}
