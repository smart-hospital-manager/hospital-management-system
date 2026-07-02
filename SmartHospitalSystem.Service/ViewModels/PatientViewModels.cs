using SmartHospitalSystem.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHospitalSystem.Service.ViewModels
{
    public class PatientViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public DateTime DateOfBirth { get; set; }
        public Gender Gender { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalId { get; set; }
        public string BloodType { get; set; }
        public string Address { get; set; }
    }

    public class PatientCreateViewModel
    {
        [Required(ErrorMessage = "اسم المريض الأول إجباري")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "اسم المريض الأخير إجباري")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "تاريخ الميلاد إجباري")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "تحديد النوع إجباري")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "رقم الهاتف إجباري")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "الرقم القومي إجباري")]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "الرقم القومي 14 رقم")]
        public string NationalId { get; set; }

        public string Email { get; set; }
        public string BloodType { get; set; }
        public string Address { get; set; }
    }
}
