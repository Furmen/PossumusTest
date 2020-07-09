using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Job
    {
        public int JobId { get; set; }

        [Required(ErrorMessage = "Company Name is required")]
        [StringLength(50, ErrorMessage = "The lenght of Company Name is 50 characters")]
        [DataType(DataType.Text, ErrorMessage = "Company Name invalid")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Period is required")]
        public string Period { get; set; }

        public Candidate Candidate { get; set; }
    }
}