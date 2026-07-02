
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartHospitalSystem.Service.ViewModels; // تم تعديل الـ using هنا

namespace SmartHospitalSystem.Service.Abstractions
{
    public interface IDepartmentService
    {
        Task<List<DepartmentViewModel>> GetAllDepartmentsAsync();
        Task<DepartmentViewModel?> GetDepartmentByIdAsync(int id);
        Task AddDepartmentAsync(DepartmentCreateViewModel model);
    }
}