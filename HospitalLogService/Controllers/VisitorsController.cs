using HospitalLogService.Model;
using HospitalLogService.Repositories.Contracts;
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
    public class VisitorsController : ControllerBase
    {
        private readonly IVisitorRepository _repo;

        public VisitorsController(IVisitorRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IEnumerable<Visitor>> GetVisitorsAsync()
        {
            return await _repo.GetVisitorsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Visitor>> GetVisitorAsync(int id)//from route or not?
        {
            var visitor = await _repo.GetVisitorAsync(id);
            if(visitor != null)
            {
                return Ok(visitor);
            }
            return NotFound("Visit was not found!");
        }

        [HttpPost]
        public async Task<ActionResult<Visitor>> PostVisitorAsync([FromBody] Visitor visitor)
        {
            visitor.Id = 0;
            visitor.DateOfBirth = DateTime.Now;

            return await _repo.AddVisitorAsync(visitor);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Visitor>> PostVisitorAsync([FromRoute] int id, [FromBody] Visitor visitor)
        {
            if (id != visitor.Id)
            {
                return BadRequest("Not the same Identifiers!");
            }
            await _repo.UpdateVisitorAsync(visitor);
            return Ok("Successfully updated!");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Visitor>> DeleteVisitorAsync(int id)//from route or not?
        {
            var deleteVisitor = await _repo.GetVisitorAsync(id);
            if(deleteVisitor is null)
            {
                return NotFound();
            }

            await _repo.DeleteVisitorAsync(deleteVisitor.Id);
            return Ok("Successfully deleted!");
        }
    }
}
