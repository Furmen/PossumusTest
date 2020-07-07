using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class CandidateDTO
    {
        public int CandidateId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "The lenght of Name is 50 characters")]
        [DataType(DataType.Text, ErrorMessage = "Name invalid")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        [StringLength(50, ErrorMessage = "The lenght of LastName is 50 characters")]
        [DataType(DataType.Text, ErrorMessage = "LastName invalid")]
        [Display(Name = "Lastname")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Date of Birth is required")]
        [DisplayFormat(DataFormatString = "dd/MM/yyyy")]
        [Display(Name = "Date of Birth")]
        public string DateOfBirth { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Email invalid")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Phone Number invalid")]
        [Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Resume is required")]
        [DataType(DataType.Upload, ErrorMessage = "Resume invalid")]
        [Display(Name = "Resume")]
        public string Resume { get; set; }

        public IEnumerable<JobDTO> Jobs { get; set; }
    }
}