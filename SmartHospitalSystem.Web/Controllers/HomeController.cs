using Microsoft.AspNetCore.Mvc;
using SmartHospitalSystem.Service.Abstractions;
using System.Threading.Tasks;

namespace SmartHospitalSystem.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDoctorService _doctorService;
        private readonly IPatientService _patientService;
        private readonly IAppointmentService _appointmentService;

        // حقن الخدمات لحساب الإحصائيات فورا من قاعدة البيانات
        public HomeController(IDoctorService doctorService, IPatientService patientService, IAppointmentService appointmentService)
        {
            _doctorService = doctorService;
            _patientService = patientService;
            _appointmentService = appointmentService;
        }

        public async Task<IActionResult> Index()
        {
            var doctors = await _doctorService.GetAllDoctorsAsync();
            var patients = await _patientService.GetAllPatientsAsync();
            var appointments = await _appointmentService.GetAllAppointmentsAsync();

            // نرسل الأعداد للشاشة عن طريق الـ ViewBag
            ViewBag.DoctorsCount = doctors?.Count ?? 0;
            ViewBag.PatientsCount = patients?.Count ?? 0;
            ViewBag.AppointmentsCount = appointments?.Count ?? 0;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}