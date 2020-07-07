using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Application.Mapper
{
    public static class CandidateMapper
    {
        public static Candidate ToEntity(this CandidateDTO candidateDTO)
        {
            return new Candidate
            {
                CandidateId = candidateDTO.CandidateId,
                DateOfBirth = DateTime.ParseExact("dd/MM/yyyy", candidateDTO.DateOfBirth, CultureInfo.InvariantCulture),
                Email = candidateDTO.Email,
                LastName = candidateDTO.LastName,
                Name = candidateDTO.Name,
                PhoneNumber = candidateDTO.PhoneNumber,
                Resume = candidateDTO.Resume,
                Jobs = candidateDTO.Jobs != null && candidateDTO.Jobs.Any() ? 
                            candidateDTO.Jobs.Select(s => s.ToEntity()) : new List<Job>()
            };
        }

        public static CandidateDTO ToDTO(this Candidate candidate)
        {
            return new CandidateDTO
            {
                CandidateId = candidate.CandidateId,
                DateOfBirth = candidate.DateOfBirth.ToString("dd/MM/yyyy"),
                Email = candidate.Email,
                LastName = candidate.LastName,
                Name = candidate.Name,
                PhoneNumber = candidate.PhoneNumber,
                Resume = candidate.Resume,
                Jobs = candidate.Jobs != null && candidate.Jobs.Any() ? 
                           candidate.Jobs.Select(s => s.ToDTO()) : new List<JobDTO>()
            };
        }
    }
}