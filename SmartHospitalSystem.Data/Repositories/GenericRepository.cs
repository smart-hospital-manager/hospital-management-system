using Microsoft.EntityFrameworkCore;
using SmartHospitalSystem.Core.Repositories;
using SmartHospitalSystem.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SmartHospitalSystem.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        // حجز نسخة من الـ DbContext لتمريرها عبر الـ Constructor
        protected readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // جلب كل السجلات
        public async Task<List<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        // 👈 التنفيذ الجديد للميثود: جلب البيانات بناءً على شرط مع دعم الـ Includes
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null)
        {
            IQueryable<T> query = _context.Set<T>();

            // لو في جداول تانية مبعوتة نعمل لها Include (زي الـ Department مع الدكتور)
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            // تنفيذ الفلترة وإرجاع النتيجة
            return await query.Where(criteria).ToListAsync();
        }

        // جلب سجل واحد بالـ Id
        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        // إضافة سجل جديد في الـ Memory
        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        // تعديل سجل
        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        // حذف سجل (أو تجهيزه للحذف)
        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }
    }
}