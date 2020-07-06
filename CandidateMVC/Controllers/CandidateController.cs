using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CandidateService.Controllers
{
    public class CandidateController : Controller
    {
        // GET: Candidates
        public IActionResult Index()
        {
            //return View(await _context.Candidates.ToListAsync());
            return View();
        }

        // GET: Candidates/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var candidate = await _context.Candidates
        //        .FirstOrDefaultAsync(m => m.CandidateId == id);
        //    if (candidate == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(candidate);
        //}

        // GET: Candidates/Create
        public IActionResult Create()
        {
            return View();
        }

        //// POST: Candidates/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("CandidateId,Name,LastName,DateOfBirth,Email,PhoneNumber,Resume")] Candidate candidate)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(candidate);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(candidate);
        //}

        //// GET: Candidates/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var candidate = await _context.Candidates.FindAsync(id);
        //    if (candidate == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(candidate);
        //}

        //// POST: Candidates/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("CandidateId,Name,LastName,DateOfBirth,Email,PhoneNumber,Resume")] Candidate candidate)
        //{
        //    if (id != candidate.CandidateId)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(candidate);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!CandidateExists(candidate.CandidateId))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(candidate);
        //}

        //// GET: Candidates/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var candidate = await _context.Candidates
        //        .FirstOrDefaultAsync(m => m.CandidateId == id);
        //    if (candidate == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(candidate);
        //}

        //// POST: Candidates/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var candidate = await _context.Candidates.FindAsync(id);
        //    _context.Candidates.Remove(candidate);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool CandidateExists(int id)
        //{
        //    return _context.Candidates.Any(e => e.CandidateId == id);
        //}
    }
}
