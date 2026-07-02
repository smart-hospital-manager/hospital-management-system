using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartHospitalSystem.Service.Abstractions;
using SmartHospitalSystem.Service.ViewModels;
using System.Threading.Tasks;

namespace SmartHospitalSystem.Web.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly IPatientService _patientService;
        private readonly IDoctorService _doctorService;
        private readonly IDepartmentService _departmentService;

        public AppointmentController(
            IAppointmentService appointmentService,
            IPatientService patientService,
            IDoctorService doctorService,
            IDepartmentService departmentService)
        {
            _appointmentService = appointmentService;
            _patientService = patientService;
            _doctorService = doctorService;
            _departmentService = departmentService;
        }

        // 1. شاشة عرض كل المواعيد المحجوزة بالـ DataTable
        public async Task<IActionResult> Index()
        {
            var appointments = await _appointmentService.GetAllAppointmentsAsync();
            return View(appointments);
        }

        // 2. شاشة حجز موعد جديد (تجهيز القوائم المنسدلة لـ المرضى، الدكاترة، والأقسام)
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var patients = await _patientService.GetAllPatientsAsync();
            var doctors = await _doctorService.GetAllDoctorsAsync();
            var departments = await _departmentService.GetAllDepartmentsAsync();

            ViewBag.Patients = new SelectList(patients, "Id", "FirstName"); // للتبسيط هيعرض الاسم الأول، وممكن تدمجهم
            ViewBag.Doctors = new SelectList(doctors, "Id", "FirstName");
            ViewBag.Departments = new SelectList(departments, "Id", "Name");

            return View();
        }

        // 3. استقبال البيانات وحفظ الموعد
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AppointmentCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _appointmentService.AddAppointmentAsync(model);
                return RedirectToAction(nameof(Index));
            }

            // إعادة تحميل القوائم في حالة وجود خطأ في البيانات
            var patients = await _patientService.GetAllPatientsAsync();
            var doctors = await _doctorService.GetAllDoctorsAsync();
            var departments = await _departmentService.GetAllDepartmentsAsync();

            ViewBag.Patients = new SelectList(patients, "Id", "FirstName");
            ViewBag.Doctors = new SelectList(doctors, "Id", "FirstName");
            ViewBag.Departments = new SelectList(departments, "Id", "Name");

            return View(model);
        }
    }
}