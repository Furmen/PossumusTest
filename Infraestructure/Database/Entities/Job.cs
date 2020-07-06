﻿using System;
using System.ComponentModel.DataAnnotations;

namespace CandidateService.Database.Entities
{
    public class Job
    {
        public int JobId { get; set; }

        [Required(ErrorMessage = "Company Name is required")]
        [StringLength(50, ErrorMessage = "The lenght of Company Name is 50 characters")]
        [DataType(DataType.Text, ErrorMessage = "Company Name invalid")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Period is required")]
        [DataType(DataType.Date, ErrorMessage = "Period invalid")]
        [DisplayFormat(DataFormatString = "dd/MM/yyyy")]
        public DateTime Period { get; set; }

        public Candidate Candidate { get; set; }
    }
}