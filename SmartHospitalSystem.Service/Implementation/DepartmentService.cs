using AutoMapper;
using SmartHospitalSystem.Core.Entities;
using SmartHospitalSystem.Core.Repositories;
using SmartHospitalSystem.Service.Abstractions;
using SmartHospitalSystem.Service.ViewModels; // تم التعديل هنا ليقرأ من الفولدر الجديد
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHospitalSystem.Service.Implementation
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<DepartmentViewModel>> GetAllDepartmentsAsync()
        {
            var departments = await _unitOfWork.Departments.GetAllAsync();
            return _mapper.Map<List<DepartmentViewModel>>(departments);
        }

        public async Task<DepartmentViewModel?> GetDepartmentByIdAsync(int id)
        {
            var department = await _unitOfWork.Departments.GetByIdAsync(id);
            if (department == null) return null;

            return _mapper.Map<DepartmentViewModel>(department);
        }

        public async Task AddDepartmentAsync(DepartmentCreateViewModel model)
        {
            var departmentEntity = _mapper.Map<Department>(model);
            await _unitOfWork.Departments.AddAsync(departmentEntity);
            await _unitOfWork.CompleteAsync();
        }
    }
}