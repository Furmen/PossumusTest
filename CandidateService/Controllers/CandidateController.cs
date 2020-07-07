using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Application.DTOs;
using Application.Mapper;
using Domain.Repository.Interface;

namespace CandidateService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateRepository candidateRepository;

        public CandidateController(ICandidateRepository candidateRepository)
        {
            this.candidateRepository = candidateRepository;
        }

        // GET: api/Candidate
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CandidateDTO>>> GetCandidates()
        {
            var candidates = await candidateRepository.GetAllAsync();
            return candidates.Select(s => s.ToDTO()).ToList();
        }

        // GET: api/Candidate/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CandidateDTO>> GetCandidate(int id)
        {
            var candidate = await candidateRepository.GetById(id);

            if (candidate == null)
                return NotFound();

            return candidate.ToDTO();
        }

        // PUT: api/Candidate/5
        [HttpPut("{id}")]
        public IActionResult PutCandidate(int id, CandidateDTO candidate)
        {
            if (id != candidate.CandidateId)
                return BadRequest();

            try
            {
                candidateRepository.Update(candidate.ToEntity());
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw ex;
            }

            return NoContent();
        }

        // POST: api/Candidate
        [HttpPost]
        public ActionResult<CandidateDTO> PostCandidate(CandidateDTO candidate)
        {
            candidateRepository.Add(candidate.ToEntity());

            return CreatedAtAction("GetCandidate", new { id = candidate.CandidateId }, candidate);
        }

        // DELETE: api/Candidate/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CandidateDTO>> DeleteCandidate(int id)
        {
            var candidate = await candidateRepository.GetById(id);

            if (candidate == null)
                return NotFound();

            candidateRepository.Delete(candidate);

            return candidate.ToDTO();
        }
    }
}