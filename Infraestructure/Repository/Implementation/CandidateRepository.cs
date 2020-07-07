﻿using CandidateService.Database;
using Domain.Entities;
using Domain.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infraestructure.Repository.Implementation
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly DatabaseContext _context;

        public CandidateRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Candidate>> GetAllAsync()
        {
            return await _context.Candidates.Include(inc => inc.Jobs).ToListAsync();
        }

        public async Task<Candidate> GetById(int id)
        {
            var candidate = await _context.Candidates.Include(inc => inc.Jobs)
                                                         .FirstOrDefaultAsync(e => e.CandidateId == id);
            return candidate;
        }

        public async void Update(Candidate candidate)
        {
            _context.Entry(candidate).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async void Add(Candidate candidate)
        {
            _context.Candidates.Add(candidate);
            await _context.SaveChangesAsync();
        }

        public async void Delete(Candidate candidate)
        {
            _context.Candidates.Remove(candidate);
            await _context.SaveChangesAsync();
        }
    }
}
