using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class JobDTO
    {
        public int JobId { get; set; }

        [Required(ErrorMessage = "Company Name is required")]
        [StringLength(50, ErrorMessage = "The lenght of Company Name is 50 characters")]
        [DataType(DataType.Text, ErrorMessage = "Company Name invalid")]
        public string CompanyName { get; set; }

        [Required(ErrorMessage = "Period is required")]
        [DataType(DataType.Date, ErrorMessage = "Period invalid")]
        [DisplayFormat(DataFormatString = "dd/MM/yyyy")]
        public string Period { get; set; }

        public int JobIndex { get; set; }
    }
}