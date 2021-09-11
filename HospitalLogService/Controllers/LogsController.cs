using HospitalLogService.Contracts;
using HospitalLogService.Model;
using HospitalLogService.Repositories.Contracts;
using HospitalLogService.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HospitalLogService.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly ILogRepository _repo;
        private readonly IDepartmentRepository _repoDepartment;
        private readonly IVisitorRepository _repoVisitor;
        public LogsController(ILogRepository repo, IDepartmentRepository repoDepartment, IVisitorRepository repoVisitor)
        {
            _repo = repo;
            _repoDepartment = repoDepartment;
            _repoVisitor = repoVisitor;
        }

        [HttpGet]
        public async Task<IEnumerable<Log>> GetLogsAsync()
        {
            return await _repo.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Log>> GetLogsAsync(int id)
        {
            var log = await _repo.GetAsync(id);
            if (log != null)
            {
                return Ok(log);
            }
            return NotFound("Visit was not found!");
        }

        [HttpPost]
        public async Task<ActionResult<Log>> PostLogsAsync([FromBody] Log log)
        {
            log.Id = 0;
            log.CreatedOn = DateTime.Now;           

            var departmenModel = await _repoDepartment.GetDepartmentAsync(log.DepartmentId);
            if(departmenModel is null)
            {
                return NotFound("Could not find data with the given department id!");
            }
                
            var visitorModel = await _repoVisitor.GetVisitorAsync(log.VisitorId);
            if (visitorModel != null)
            {
                if (visitorModel.Type == Enums.VisitorType.Student)
                {
                    return BadRequest("Student type has only read access!");
                }
            }
            else return NotFound("Could not find data with the given visitor id!");


            return await _repo.CreateAsync(log);
        }

        [HttpPost("Search")]
        public async Task<IEnumerable<Log>> SearchAsync([FromBody] SearchRequest request)
        {
            if (request.Username is null)
            {
                NotFound("Please enter username to complete searching successfully!");
            }

            return await _repo.SearchAsync(request);
        }
    }
}
