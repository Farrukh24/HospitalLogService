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
    public class VisitorRepository : IVisitorRepository
    {
        private readonly ApplicationDbContext _db;

        public VisitorRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Visitor> AddVisitorAsync(Visitor visitor)
        {
            await _db.Visitors.AddAsync(visitor);
            await _db.SaveChangesAsync();
            return visitor;
        }

        public async Task DeleteVisitorAsync(int id)
        {
            var deleteVisitor = await _db.Visitors.FindAsync(id);
            _db.Visitors.Remove(deleteVisitor);
            await _db.SaveChangesAsync();
        }

        public async Task<Visitor> GetVisitorAsync(int id)
        {
            return await _db.Visitors.FindAsync(id);
        }

        public async Task<IEnumerable<Visitor>> GetVisitorsAsync()
        {
            return await _db.Visitors.ToListAsync();
        }

        public async Task UpdateVisitorAsync(Visitor visitor)
        {
            _db.Entry(visitor).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }
    }
}
