using Application.DTOs;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Newtonsoft.Json.Linq;
using System;

namespace Application.Query
{
    public class CandidateQuery : ICandidateQuery
    {
        public async Task<IEnumerable<CandidateDTO>> GetAllCandidatesAsync(string baseURL)
        {
            var client = new RestClient(baseURL);
            var request = new RestRequest(Method.GET);

            IRestResponse response = await client.ExecuteAsync(request);

            var candidates = JArray.Parse(response.Content);

            return candidates.Select(s => new CandidateDTO
            {
                CandidateId = s.Value<int>("candidateId"),
                Name= s.Value<string>("name"),
                LastName = s.Value<string>("lastName"),
                DateOfBirth = s.Value<string>("dateOfBirth"),
                Email = s.Value<string>("email"),
                PhoneNumber = s.Value<string>("phoneNumber"),
                Resume = s.Value<string>("resume"),
                Jobs = s.Value<JArray>("jobs").Select(s => new JobDTO { 
                    JobId = s.Value<int>("jobId"),
                    CompanyName = s.Value<string>("companyName"),
                    Period = s.Value<string>("period")
                })
            }).ToList();
        }

        public async Task<CandidateDTO> GetCandidateByIdAsync(string baseURL, int id)
        {
            var client = new RestClient(baseURL + "/" + id);
            var request = new RestRequest(Method.GET);

            IRestResponse response = await client.ExecuteAsync(request);

            var candidate = JObject.Parse(response.Content);

            return new CandidateDTO
            {
                CandidateId = candidate.Value<int>("candidateId"),
                Name = candidate.Value<string>("name"),
                LastName = candidate.Value<string>("lastName"),
                DateOfBirth = candidate.Value<string>("dateOfBirth"),
                Email = candidate.Value<string>("email"),
                PhoneNumber = candidate.Value<string>("phoneNumber"),
                Resume = candidate.Value<string>("resume"),
                Jobs = candidate.Value<JArray>("jobs").Select(s => new JobDTO
                {
                    JobId = s.Value<int>("jobId"),
                    CompanyName = s.Value<string>("companyName"),
                    Period = s.Value<string>("period")
                })
            };
        }
    }
}
