using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Application.DTOs;
using Application.Mapper;
using Domain.Repository.Interface;
using Microsoft.Extensions.Logging;

namespace CandidateService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateRepository candidateRepository;
        private readonly ILogger<CandidateController> log;

        public CandidateController(ICandidateRepository candidateRepository,
                                    ILogger<CandidateController> log)
        {
            this.candidateRepository = candidateRepository;
            this.log = log;
        }

        // GET: api/Candidate
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CandidateDTO>>> GetCandidates()
        {
            log.LogInformation("Getl all candidates");

            try
            {
                var candidates = await candidateRepository.GetAllAsync();
                return candidates.Select(s => s.ToDTO()).ToList();
            }
            catch (System.Exception ex)
            {
                log.LogInformation("Error getting all candidates: " + ex.Message ?? ex.InnerException.Message);
                return new List<CandidateDTO>();
            }
        }

        // GET: api/Candidate/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CandidateDTO>> GetCandidate(int id)
        {
            log.LogInformation("Getl candidate by id: " + id);

            var candidate = await candidateRepository.GetByIdAsync(id);

            if (candidate == null)
            {
                log.LogWarning("Candidate by Id - Not Found: " + id);

                return NotFound();
            }

            return candidate.ToDTO();
        }

        // PUT: api/Candidate
        [HttpPut]
        public async Task<IActionResult> PutCandidateAsync(CandidateDTO candidate)
        {
            log.LogInformation("Update candidate - CandidateId: " + candidate.CandidateId);

            try
            {
                candidateRepository.RemovePreviousJobsById(candidate.CandidateId);

                var candidateUpdated = await candidateRepository.GetByIdAsync(candidate.CandidateId);

                candidate.ToEntity(candidateUpdated);

                candidateRepository.Update(candidateUpdated);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                log.LogError("Database update error: " + ex.Message ?? ex.InnerException.Message);
            }

            log.LogWarning("Updated candidate successfully - CandidateId: " + candidate.CandidateId);

            return NoContent();
        }

        // POST: api/Candidate
        [HttpPost]
        public ActionResult<CandidateDTO> PostCandidate(CandidateDTO candidate)
        {
            log.LogInformation("Create candidate");

            try
            {
                candidateRepository.Add(candidate.ToEntity());

                log.LogInformation("Create successfully - new CandidateId: " + candidate.CandidateId);
            }
            catch (System.Exception ex)
            {
                log.LogError("Error creating candidate: " + ex.Message ?? ex.InnerException.Message);
            }

            return CreatedAtAction("GetCandidate", new { id = candidate.CandidateId }, candidate);
        }

        // DELETE: api/Candidate/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CandidateDTO>> DeleteCandidate(int id)
        {
            log.LogInformation("Delete candidate");

            var candidate = await candidateRepository.GetByIdAsync(id);

            if (candidate == null)
            {
                log.LogWarning("Candidate not found - CandidateId: " + id);

                return NotFound();
            }

            try
            {
                candidateRepository.Delete(candidate);

                log.LogInformation("Candidate successfully deleted");
            }
            catch (System.Exception ex)
            {
                log.LogError("Error when trying to delete candidate: " + ex.Message ?? ex.InnerException.Message);
            }

            return candidate.ToDTO();
        }
    }
}