using Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Query
{
    public interface ICandidateQuery
    {
        Task<IEnumerable<CandidateDTO>> GetAllCandidatesAsync(string baseURL);

        Task<CandidateDTO> GetCandidateByIdAsync(string baseURL, int id);
    }
}
