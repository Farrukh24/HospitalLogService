using HospitalLogService.Data;
using HospitalLogService.Model;
using HospitalLogService.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalLogService.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly ApplicationDbContext _db;

        public DepartmentRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Department> CreateDepartmentAsync(Department department)
        {
            await _db.Departments.AddAsync(department);
            await _db.SaveChangesAsync();

            return department;
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            var deleteDepartment = await _db.Departments.FindAsync(id);
            _db.Departments.Remove(deleteDepartment);
            await _db.SaveChangesAsync();
            
        }

        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        {
            return await _db.Departments.ToListAsync();
        }

        public async Task<Department> GetDepartmentAsync(int id)
        {
            var findedDepartment = await _db.Departments.FindAsync(id);
            return findedDepartment;
        }      

        public async Task UpdateDepartmentAsync(Department department)
        {
             _db.Entry(department).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }
    }
}
