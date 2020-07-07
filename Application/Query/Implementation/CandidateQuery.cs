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

            request.AddHeader("Cookie", ".AspNet.Cookies=FpoqaqScpCJH6AKf3tY6OaDGRp14dGtYkBcBzZ5C873PZ3U1OSiXeoflCEPH7VLYHepxTk7zQb4uWcaPdlAyaDroODoOk6TpLLOmIRWDH-XajrwEdibK0vHxSiyrx_UMiGJ9nDokxJbmeIUo3QdnEO6Q1MpvWsfdfoXUwLOsqjGt4DGA54eyjW_tmGDl8naztxhOis5VfscPNbqAF8Lqi4CfALCn7AU-GUsxwGUoxO6irLfN4ulFJuA37wSOw4tI_0-geHvfuVyH-PCHwRjiSuaOZHvkSKybsL2BcdSDhoP1p9n3mJqG0cZ15aUw4uQnxLM1r3gboBGcL8Xop7h_WuNE875g9x0rfnQ28pjQANvkOq-rJ0kj26tiIzgXBf3DDtgUsGuLWxXsLRxq84-2Hc4o2P3zLHZtnY-kJvqGy_iYxp8Cani-h2xPZdt79kGzLDsCq9rlAJb7RJZdD91Dqe7nW6hgaD9RkDZ2zv8i2n4");
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
                    Period = s.Value<DateTime>("period")
                })
            }).ToList();
        }

        public async Task<CandidateDTO> GetCandidateByIdAsync(string baseURL, int id)
        {
            var client = new RestClient(baseURL + "/" + id);
            var request = new RestRequest(Method.GET);
            request.AddHeader("Cookie", ".AspNet.Cookies=FpoqaqScpCJH6AKf3tY6OaDGRp14dGtYkBcBzZ5C873PZ3U1OSiXeoflCEPH7VLYHepxTk7zQb4uWcaPdlAyaDroODoOk6TpLLOmIRWDH-XajrwEdibK0vHxSiyrx_UMiGJ9nDokxJbmeIUo3QdnEO6Q1MpvWsfdfoXUwLOsqjGt4DGA54eyjW_tmGDl8naztxhOis5VfscPNbqAF8Lqi4CfALCn7AU-GUsxwGUoxO6irLfN4ulFJuA37wSOw4tI_0-geHvfuVyH-PCHwRjiSuaOZHvkSKybsL2BcdSDhoP1p9n3mJqG0cZ15aUw4uQnxLM1r3gboBGcL8Xop7h_WuNE875g9x0rfnQ28pjQANvkOq-rJ0kj26tiIzgXBf3DDtgUsGuLWxXsLRxq84-2Hc4o2P3zLHZtnY-kJvqGy_iYxp8Cani-h2xPZdt79kGzLDsCq9rlAJb7RJZdD91Dqe7nW6hgaD9RkDZ2zv8i2n4");
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
                    Period = s.Value<DateTime>("period")
                })
            };
        }
    }
}
