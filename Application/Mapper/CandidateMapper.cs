using Application.DTOs;
using Domain.Entities;
using Newtonsoft.Json;
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
            ICollection<Job> jobs = new List<Job>();

            if (!string.IsNullOrEmpty(candidateDTO.JobsJson))
                jobs = JsonConvert.DeserializeObject<IEnumerable<JobDTO>>(candidateDTO.JobsJson).Select(s => s.ToEntity()).ToList();

            return new Candidate
            {
                CandidateId = candidateDTO.CandidateId,
                DateOfBirth = DateTime.ParseExact(candidateDTO.DateOfBirth, "yyyy-MM-dd", CultureInfo.InvariantCulture),
                Email = candidateDTO.Email,
                LastName = candidateDTO.LastName,
                Name = candidateDTO.Name,
                PhoneNumber = candidateDTO.PhoneNumber,
                Resume = candidateDTO.Resume,
                Jobs = jobs
            };
        }

        public static Candidate ToEntityForUpdate(this Candidate candidate, CandidateDTO candidateDTO)
        {
            ICollection<Job> jobs = new List<Job>();

            if (!string.IsNullOrEmpty(candidateDTO.JobsJson))
                jobs = JsonConvert.DeserializeObject<IEnumerable<JobDTO>>(candidateDTO.JobsJson).Select(s => s.ToEntity()).ToList();

            candidate.DateOfBirth = DateTime.ParseExact(candidateDTO.DateOfBirth, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            candidate.Email = candidateDTO.Email;
            candidate.LastName = candidateDTO.LastName;
            candidate.Name = candidateDTO.Name;
            candidate.PhoneNumber = candidateDTO.PhoneNumber;
            candidate.Resume = candidateDTO.Resume;
            candidate.Jobs = jobs;

            return candidate;
        }

        public static CandidateDTO ToDTO(this Candidate candidate)
        {
            return new CandidateDTO
            {
                CandidateId = candidate.CandidateId,
                DateOfBirth = candidate.DateOfBirth.ToString("yyyy-MM-dd"),
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