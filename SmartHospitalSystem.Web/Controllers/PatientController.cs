using Microsoft.AspNetCore.Mvc;
using SmartHospitalSystem.Service.Abstractions;
using SmartHospitalSystem.Service.ViewModels;
using SmartHospitalSystem.Data.Context; // لقراءة الـ DbContext
using SmartHospitalSystem.Core.Entities; // لقراءة الـ Patient Entity
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SmartHospitalSystem.Web.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientService _patientService;
        private readonly ApplicationDbContext _context; // حقن الـ DbContext

        public PatientController(IPatientService patientService, ApplicationDbContext context)
        {
            _patientService = patientService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var patients = await _patientService.GetAllPatientsAsync();
            return View(patients);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PatientCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var patientEntity = new Patient
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    NationalId = model.NationalId,
                    PhoneNumber = model.PhoneNumber,
                    Email = model.Email,
                    BloodType = model.BloodType,
                    Address = model.Address
                };

                _context.Patients.Add(patientEntity);
                await _context.SaveChangesAsync(); // حفظ مباشر في الداتابيز

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null) return NotFound();

            var model = new PatientCreateViewModel
            {
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                NationalId = patient.NationalId,
                PhoneNumber = patient.PhoneNumber,
                Email = patient.Email,
                BloodType = patient.BloodType,
                Address = patient.Address
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, PatientCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var patient = await _context.Patients.FindAsync(id);
                if (patient == null) return NotFound();

                // تحديث البيانات الحقيقية
                patient.FirstName = model.FirstName;
                patient.LastName = model.LastName;
                patient.PhoneNumber = model.PhoneNumber;
                patient.Email = model.Email;
                patient.BloodType = model.BloodType;
                patient.Address = model.Address;

                _context.Patients.Update(patient);
                await _context.SaveChangesAsync(); // تعديل مباشر في الداتابيز

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }
    }
}