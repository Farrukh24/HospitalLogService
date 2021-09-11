using HospitalLogService.Model;
using HospitalLogService.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalLogService.Contracts
{
    public interface ILogRepository
    {
        Task<IEnumerable<Log>> GetAllAsync();
        Task<Log> GetAsync(int id);

        Task<Log> CreateAsync(Log log);
        Task UpdateAsync(Log log);
        Task DeleteAsync(int id);
        Task<IEnumerable<Log>> SearchAsync(SearchRequest request);       

    }
}
