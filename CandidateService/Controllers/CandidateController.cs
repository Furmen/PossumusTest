using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Application.DTOs;
using Application.Mapper;
using Domain.Repository.Interface;
using Microsoft.Extensions.Configuration;
using System.Globalization;
using System;

namespace CandidateService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly ICandidateRepository candidateRepository;
        private readonly IConfiguration configuration;

        public CandidateController(ICandidateRepository candidateRepository,
                                   IConfiguration configuration)
        {
            this.candidateRepository = candidateRepository;
            this.configuration = configuration;
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

        // PUT: api/Candidate
        [HttpPut]
        public async Task<IActionResult> PutCandidateAsync(CandidateDTO candidate)
        {
            try
            {
                candidateRepository.RemovePreviousJobsById(candidate.CandidateId);

                var candidateUpdated = await candidateRepository.GetById(candidate.CandidateId);

                candidateUpdated.ToEntityForUpdate(candidate);

                candidateRepository.Update(candidateUpdated);
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