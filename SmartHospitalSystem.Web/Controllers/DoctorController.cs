using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartHospitalSystem.Service.Abstractions;
using SmartHospitalSystem.Service.ViewModels;
using SmartHospitalSystem.Data.Context; // عشان يقرأ الـ DbContext بتاعك
using SmartHospitalSystem.Core.Entities; // عشان يقرأ الـ Doctor Entity
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SmartHospitalSystem.Web.Controllers
{
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;
        private readonly IDepartmentService _departmentService;
        private readonly IAppointmentService _appointmentService;
        private readonly ApplicationDbContext _context; // حقن الـ DbContext مباشرة

        public DoctorController(IDoctorService doctorService,
                                IDepartmentService departmentService,
                                IAppointmentService appointmentService,
                                ApplicationDbContext context)
        {
            _doctorService = doctorService;
            _departmentService = departmentService;
            _appointmentService = appointmentService;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var doctors = await _doctorService.GetAllDoctorsAsync();
            return View(doctors);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var departments = await _departmentService.GetAllDepartmentsAsync();
            ViewBag.Departments = new SelectList(departments, "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DoctorCreateViewModel model, IFormFile ImageFile)
        {
            if (ImageFile == null || ImageFile.Length == 0)
            {
                ModelState.AddModelError("Biography", "يرجى اختيار صورة الطبيب الشخصية من جهازك أولاً");
            }

            if (ModelState.IsValid)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");

                if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

                var filePath = Path.Combine(uploadsFolder, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                model.Biography = "/uploads/" + fileName;

                // الحفظ الفعلي المباشر في قاعدة البيانات
                var doctorEntity = new Doctor
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Specialization = model.Specialization,
                    DepartmentId = model.DepartmentId,
                    PhoneNumber = model.PhoneNumber,
                    NationalId = model.NationalId,
                    Email = model.Email,
                    Biography = model.Biography
                };

                _context.Doctors.Add(doctorEntity);
                await _context.SaveChangesAsync(); // السطر السحري للحفظ

                return RedirectToAction(nameof(Index));
            }

            var departments = await _departmentService.GetAllDepartmentsAsync();
            ViewBag.Departments = new SelectList(departments, "Id", "Name", model.DepartmentId);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var doctor = await _context.Doctors.FindAsync(id);
            if (doctor == null) return NotFound();

            var departments = await _departmentService.GetAllDepartmentsAsync();
            ViewBag.Departments = new SelectList(departments, "Id", "Name", doctor.DepartmentId);

            var model = new DoctorCreateViewModel
            {
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Specialization = doctor.Specialization,
                DepartmentId = doctor.DepartmentId,
                PhoneNumber = doctor.PhoneNumber,
                NationalId = doctor.NationalId,
                Email = doctor.Email,
                Biography = doctor.Biography
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DoctorCreateViewModel model, IFormFile ImageFile)
        {
            if (ModelState.IsValid)
            {
                var doctor = await _context.Doctors.FindAsync(id);
                if (doctor == null) return NotFound();

                if (ImageFile != null && ImageFile.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                    if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

                    var filePath = Path.Combine(uploadsFolder, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(stream);
                    }
                    doctor.Biography = "/uploads/" + fileName;
                }

                // تحديث البيانات الحقيقية
                doctor.FirstName = model.FirstName;
                doctor.LastName = model.LastName;
                doctor.Specialization = model.Specialization;
                doctor.DepartmentId = model.DepartmentId;
                doctor.PhoneNumber = model.PhoneNumber;
                doctor.Email = model.Email;

                _context.Doctors.Update(doctor);
                await _context.SaveChangesAsync(); // السطر السحري للتحديث

                return RedirectToAction(nameof(Index));
            }

            var departments = await _departmentService.GetAllDepartmentsAsync();
            ViewBag.Departments = new SelectList(departments, "Id", "Name", model.DepartmentId);
            return View(model);
        }

        public async Task<IActionResult> Appointments(int id)
        {
            var allAppointments = await _appointmentService.GetAllAppointmentsAsync();
            var doctorAppointments = allAppointments.Where(a => a.DoctorId == id).ToList();

            var currentDoc = await _context.Doctors.FindAsync(id);
            ViewBag.DoctorName = currentDoc != null ? $"{currentDoc.FirstName} {currentDoc.LastName}" : "المختار";

            return View(doctorAppointments);
        }
    }
}