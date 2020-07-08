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
        public async Task ExecuteCommandAsync(string baseURL, CandidateDTO candidate, Method typeMethod)
        {
            var client = new RestClient(baseURL);
            var request = new RestRequest(typeMethod);
            var jobsJson =  string.Empty;

            if (candidate.Jobs is IEnumerable<JobDTO> jobs)
                jobsJson =  JsonConvert.SerializeObject(jobs);

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
