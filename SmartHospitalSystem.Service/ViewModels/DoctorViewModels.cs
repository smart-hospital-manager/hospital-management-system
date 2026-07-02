using System;
using System.ComponentModel.DataAnnotations;

namespace SmartHospitalSystem.Service.ViewModels
{
    public class DoctorViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string Specialization { get; set; }
        public string PhoneNumber { get; set; }
        public string NationalId { get; set; }
        public string Email { get; set; }
        public decimal ConsultationFee { get; set; }
        public string Biography { get; set; } // هنستخدم الحقل ده لتخزين مسار الصورة النهائي
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
    }

    public class DoctorCreateViewModel
    {
        [Required(ErrorMessage = "الاسم الأول مطلوب")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "الاسم الأخير مطلوب")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "التخصص الطبي مطلوب")]
        public string Specialization { get; set; }

        [Required(ErrorMessage = "يجب تحديد القسم الطبي")]
        public int DepartmentId { get; set; }

        [Required(ErrorMessage = "رقم الهاتف مطلوب")]
        [Phone(ErrorMessage = "رقم الهاتف غير صحيح")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "الرقم القومي مطلوب")]
        public string NationalId { get; set; }

        [Required(ErrorMessage = "البريد الإلكتروني مطلوب")]
        [EmailAddress(ErrorMessage = "صيغة البريد الإلكتروني غير صحيحة")]
        public string Email { get; set; }

        public string Biography { get; set; } // مسار الصورة المسجل
    }
}