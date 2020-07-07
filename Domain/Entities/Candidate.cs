using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Candidate
    {
        public int CandidateId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "The lenght of Name is 50 characters")]
        [DataType(DataType.Text, ErrorMessage = "Name invalid")]
        public string Name { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        [StringLength(50, ErrorMessage = "The lenght of LastName is 50 characters")]
        [DataType(DataType.Text, ErrorMessage = "LastName invalid")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Date of Birth is required")]
        [DataType(DataType.Date, ErrorMessage = "Date of Birth invalid")]
        [DisplayFormat(DataFormatString = "dd/MM/yyyy")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email invalid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Phone Number invalid")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Resume is required")]
        [DataType(DataType.Upload, ErrorMessage = "Resume invalid")]
        public string Resume { get; set; }

        public IEnumerable<Job> Jobs { get; set; }
    }
}