using Application.DTOs;
using RestSharp;
using System.Threading.Tasks;

namespace Application.Command
{
    public interface ICandidateCommand
    {
        Task ExecuteCommandAsync(string baseURL, CandidateDTO candidate, Method typeMethod);
    }
}
