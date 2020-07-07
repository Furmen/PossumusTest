using Application.DTOs;
using RestSharp;
using System.Threading.Tasks;

namespace Application.Command
{
    public class CandidateCommand : ICandidateCommand
    {
        public async Task ExecuteCommandAsync(string baseURL, CandidateDTO candidate, Method typeMethod)
        {
            var client = new RestClient(baseURL);
            var request = new RestRequest(typeMethod);

            request.AddHeader("Cookie", ".AspNet.Cookies=FpoqaqScpCJH6AKf3tY6OaDGRp14dGtYkBcBzZ5C873PZ3U1OSiXeoflCEPH7VLYHepxTk7zQb4uWcaPdlAyaDroODoOk6TpLLOmIRWDH-XajrwEdibK0vHxSiyrx_UMiGJ9nDokxJbmeIUo3QdnEO6Q1MpvWsfdfoXUwLOsqjGt4DGA54eyjW_tmGDl8naztxhOis5VfscPNbqAF8Lqi4CfALCn7AU-GUsxwGUoxO6irLfN4ulFJuA37wSOw4tI_0-geHvfuVyH-PCHwRjiSuaOZHvkSKybsL2BcdSDhoP1p9n3mJqG0cZ15aUw4uQnxLM1r3gboBGcL8Xop7h_WuNE875g9x0rfnQ28pjQANvkOq-rJ0kj26tiIzgXBf3DDtgUsGuLWxXsLRxq84-2Hc4o2P3zLHZtnY-kJvqGy_iYxp8Cani-h2xPZdt79kGzLDsCq9rlAJb7RJZdD91Dqe7nW6hgaD9RkDZ2zv8i2n4");
            IRestResponse response = await client.ExecuteAsync(request);
        }
    }
}
