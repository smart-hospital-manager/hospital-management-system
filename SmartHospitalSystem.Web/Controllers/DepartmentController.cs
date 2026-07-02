using Microsoft.AspNetCore.Mvc;
using SmartHospitalSystem.Service.Abstractions;
using SmartHospitalSystem.Service.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHospitalSystem.Web.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;

        // حقن خدمة الأقسام فقط داخل الـ Controller
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        // 1. شاشة عرض كل الأقسام
        public async Task<IActionResult> Index()
        {
            var departments = await _departmentService.GetAllDepartmentsAsync();
            return View(departments);
        }

        // 2. شاشة الإدخال (طلب صفحة الـ Form)
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // 3. استقبال البيانات وحفظها بعد الـ Validation
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _departmentService.AddDepartmentAsync(model);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // 🛠️ 4. شاشة جلب بيانات القسم الحالية للتعديل
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var departments = await _departmentService.GetAllDepartmentsAsync();
            var dept = departments.FirstOrDefault(d => d.Id == id);

            if (dept == null)
            {
                return NotFound();
            }

            // تحويل بيانات القسم للـ ViewModel المخصص للإدخال والتعديل
            var model = new DepartmentCreateViewModel
            {
                Name = dept.Name,
                Description = dept.Description
            };

            return View(model);
        }

        // 🛠️ 5. استقبال بيانات القسم المعدلة وحفظها
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DepartmentCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                // هنا بيتم حفظ التعديل عبر السيرفيس الخاصة بالأقسام
                // await _departmentService.UpdateDepartmentAsync(id, model);

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }
}