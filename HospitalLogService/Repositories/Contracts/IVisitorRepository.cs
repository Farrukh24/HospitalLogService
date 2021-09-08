using HospitalLogService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalLogService.Repositories.Contracts
{
    public interface IVisitorRepository
    {
        Task<IEnumerable<Visitor>> GetVisitorsAsync();
        Task<Visitor> GetVisitorAsync(int id);
        Task<Visitor> AddVisitorAsync(Visitor visitor);
        Task UpdateVisitorAsync(Visitor visitor);
        Task DeleteVisitorAsync(int id);
    }
}
