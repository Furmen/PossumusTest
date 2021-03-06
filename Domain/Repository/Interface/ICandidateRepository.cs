﻿using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repository.Interface
{
    public interface ICandidateRepository
    {
        Task<IEnumerable<Candidate>> GetAllAsync();

        Task<Candidate> GetByIdAsync(int id);

        void Update(Candidate candidate);

        void Add(Candidate candidate);

        void Delete(Candidate candidate);

        void RemovePreviousJobsById(int candidateId);
    }
}
