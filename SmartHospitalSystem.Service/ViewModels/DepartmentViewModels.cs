using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHospitalSystem.Service.ViewModels
{
    // كلاس العرض في الجداول
    public class DepartmentViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
    }

    // كلاس الإدخال من الفورم مع الـ Validation
    public class DepartmentCreateViewModel
    {
        [Required(ErrorMessage = "اسم القسم إجباري")]
        [StringLength(100, ErrorMessage = "اسم القسم لا يزيد عن 100 حرف")]
        public string Name { get; set; }

        [Required(ErrorMessage = "وصف القسم إجباري")]
        [MaxLength(500, ErrorMessage = "الوصف لا يزيد عن 500 حرف")]
        public string Description { get; set; }
    }
}
