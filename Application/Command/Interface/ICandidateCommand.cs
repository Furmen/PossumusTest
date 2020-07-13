using Application.DTOs;
using System.Threading.Tasks;

namespace Application.Command
{
    public interface ICandidateCommand
    {
        Task CreateCommandAsync(string baseURL, CandidateDTO candidate);

        Task EditCommandAsync(string baseURL, CandidateDTO candidate);

        Task DeleteCommandAsync(string baseURL, int candidateId);
    }
}
