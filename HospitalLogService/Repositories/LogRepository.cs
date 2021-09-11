using HospitalLogService.Contracts;
using HospitalLogService.Data;
using HospitalLogService.Model;
using HospitalLogService.Repositories.Contracts;
using HospitalLogService.Requests;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalLogService.Repositories
{
    public class LogRepository : ILogRepository
    {
        private readonly ApplicationDbContext _db;
        public LogRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Log>> SearchAsync(SearchRequest request)
        {
            return await _db.Logs
                 .Include(a => a.Department)
                 .Include(a => a.Visitor)
                 .Where(i => i.CreatedOn > request.From && i.CreatedOn < request.To && i.Visitor.FullName.Contains(request.Username)).ToListAsync();
        }

        public async Task<Log> CreateAsync(Log log)
        {
            await _db.Logs.AddAsync(log);
            await _db.SaveChangesAsync();

            return log;
        }

        public async Task DeleteAsync(int id)
        {
            var logToDelete = await _db.Logs.FindAsync(id);
            _db.Logs.Remove(logToDelete);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Log>> GetAllAsync()
        {

            return await _db.Logs
                .AsNoTracking()
                .AsQueryable()
                .Include(i => i.Visitor)
                .Include(i => i.Department)
                .ToListAsync();
        }

        public async Task<Log> GetAsync(int id)
        {
            return await _db.Logs
               .Include(i => i.Department)
               .Include(i => i.Visitor)
               .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task UpdateAsync(Log log)
        {
            _db.Entry(log).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }
    }
}
