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
            return await _repo.GetAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<Log>> PostLogsAsync([FromBody] Log log)
        {
            var newLog = await _repo.CreateAsync(log);

            return CreatedAtAction(nameof(GetLogsAsync), new { id = newLog.Id });
        }
        
        [HttpPut("{id}")]
        public async Task<ActionResult<Log>> PutLogsAsync([FromRoute]int id, [FromBody] Log log)
        {
            if (id != log.Id)
            {
                return BadRequest("Not the same identifiers!");
            }
            await _repo.UpdateAsync(log);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Log>> DeleteLogsAsync(int id)
        {
            var deleteLog = await _repo.GetAsync(id);
            if(deleteLog is null)
            {
                return NotFound();
            }
            await _repo.DeleteAsync(deleteLog.Id);

            return NoContent();
        }
    }
}
