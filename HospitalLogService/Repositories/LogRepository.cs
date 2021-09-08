﻿using HospitalLogService.Contracts;
using HospitalLogService.Data;
using HospitalLogService.Model;
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
            return await _db.Logs.ToListAsync();
        }

        public async Task<Log> GetAsync(int id)
        {
            return await _db.Logs.FindAsync(id);
        }

        public async Task UpdateAsync(Log log)
        {
            _db.Entry(log).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }
    }
}