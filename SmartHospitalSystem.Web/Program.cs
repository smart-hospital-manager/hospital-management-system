using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SmartHospitalSystem.Data.Context;
// === الـ usings للربط بين الطبقات ===
using SmartHospitalSystem.Core.Repositories;
using SmartHospitalSystem.Data.Repositories;
using SmartHospitalSystem.Service.Abstractions;
using SmartHospitalSystem.Service.Implementation;
using SmartHospitalSystem.Service.Mapping;

namespace SmartHospitalSystem.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ==========================================
            // 1. تسجيل خدمات قاعدة البيانات والـ Identity
            // ==========================================

            // تسجيل الـ DbContext ليتصل بـ SQL Server بناءً على الـ Connection String من appsettings.json
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // تسجيل خدمات الـ ASP.NET Core Identity لإدارة المستخدمين والتسجيل والـ Roles
            builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                // إعدادات الباسورد (مخففة ومريحة للتجربة في مشروع التخرج)
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();


            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // ========================================================
            // 2. تسجيل الـ Repositories والـ Services والـ AutoMapper
            // ========================================================

            // تسجيل الـ Unit of Work والـ Generic Repository
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // تسجيل الـ Services الخاصة ببيزنس السيستم (تمت إضافة المواعيد، السجلات، والإشعارات)
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IDoctorService, DoctorService>();
            builder.Services.AddScoped<IPatientService, PatientService>();
            builder.Services.AddScoped<IAppointmentService, AppointmentService>();
            builder.Services.AddScoped<IMedicalRecordService, MedicalRecordService>();
            builder.Services.AddScoped<INotificationService, NotificationService>();

            // تسجيل الـ AutoMapper ليتعرف على ملف الـ MappingProfile تلقائياً
            builder.Services.AddAutoMapper(typeof(MappingProfile));


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            // ==========================================
            // 3. تفعيل الـ Middleware الخاصة بالأمان (الترتيب حاسم جداً)
            // ==========================================
            app.UseAuthentication(); // التحقق من هوية المستخدم (هل مسجل دخول؟)
            app.UseAuthorization();  // التحقق من صلاحيات المستخدم (هل هو Admin أم دكتور؟)

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}