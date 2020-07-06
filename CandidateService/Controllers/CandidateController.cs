using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CandidateService.Database;
using Common.DTOs;
using CandidateService.Mapper;

namespace CandidateService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CandidateController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public CandidateController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Candidate
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CandidateDTO>>> GetCandidates()
        {
            return await _context.Candidates.Include(inc => inc.Jobs).Select(s => s.ToDTO()).ToListAsync();
        }

        // GET: api/Candidate/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CandidateDTO>> GetCandidate(int id)
        {
            var candidate = await _context.Candidates.Include(inc => inc.Jobs)
                                                     .FirstOrDefaultAsync(e => e.CandidateId == id);

            if (candidate == null)
            {
                return NotFound();
            }

            return candidate.ToDTO();
        }

        // PUT: api/Candidate/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCandidate(int id, CandidateDTO candidate)
        {
            if (id != candidate.CandidateId)
            {
                return BadRequest();
            }

            _context.Entry(candidate.ToEntity()).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CandidateExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Candidate
        [HttpPost]
        public async Task<ActionResult<CandidateDTO>> PostCandidate(CandidateDTO candidate)
        {
            _context.Candidates.Add(candidate.ToEntity());
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCandidate", new { id = candidate.CandidateId }, candidate);
        }

        // DELETE: api/Candidate/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CandidateDTO>> DeleteCandidate(int id)
        {
            var candidate = await _context.Candidates.Include(inc => inc.Jobs)
                                                     .FirstOrDefaultAsync(e => e.CandidateId == id);
            if (candidate == null)
            {
                return NotFound();
            }

            _context.Candidates.Remove(candidate);
            await _context.SaveChangesAsync();

            return candidate.ToDTO();
        }

        private bool CandidateExists(int id)
        {
            return _context.Candidates.Any(e => e.CandidateId == id);
        }
    }
}