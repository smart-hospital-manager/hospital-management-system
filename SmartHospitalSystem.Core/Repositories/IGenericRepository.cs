using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SmartHospitalSystem.Core.Repositories
{
    // استخدمنا <T> حيث T هو أي كلاس يرث من BaseEntity (يعني أي جدول عندنا)
    public interface IGenericRepository<T> where T : class
    {
        // جلب كل البيانات
        Task<List<T>> GetAllAsync();

        // 👈 الميثود الجديدة: جلب البيانات بناءً على شرط (Filter) مع إمكانية عمل Include لجداول أخرى
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> criteria, string[] includes = null);

        // جلب سجل معين باستخدام الـ Id
        Task<T?> GetByIdAsync(int id);

        // إضافة سجل جديد
        Task AddAsync(T entity);

        // تعديل سجل موجود
        void Update(T entity);

        // مسح سجل (هنا مسح صريح أو لتغيير حالة IsDeleted)
        void Delete(T entity);
    }
}