﻿using Application.DTOs;
using Domain.Entities;

namespace Application.Mapper
{
    public static class JobMapper
    {
        public static Job ToEntity(this JobDTO jobDTO)
        {
            return new Job
            {
                JobId = jobDTO.JobId,
                Period = jobDTO.Period,
                CompanyName = jobDTO.CompanyName
            };
        }

        public static JobDTO ToDTO(this Job job)
        {
            return new JobDTO
            {
                JobId = job.JobId,
                Period = job.Period,
                CompanyName = job.CompanyName
            };
        }
    }
}