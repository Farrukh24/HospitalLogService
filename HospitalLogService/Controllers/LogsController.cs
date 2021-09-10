using HospitalLogService.Contracts;
using HospitalLogService.Model;
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
        public LogsController(ILogRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        //todo async
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

            //todo validate visitorId
            //todo validare departmentId

           return await _repo.CreateAsync(log);                 
        }       
              
    }
}
