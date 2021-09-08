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
    public class Departments : ControllerBase
    {
        private readonly IDepartmentRepository _repo;

        public Departments(IDepartmentRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        {
            return await _repo.GetAllDepartmentsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartmentAsync(int id)
        {
            return await _repo.GetDepartmentAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<Department>> PostDepartmentAsync([FromBody] Department department)
        {
            var newDepartment = await _repo.CreateDepartmentAsync(department);
            //todo understand 
            return CreatedAtAction(nameof(PostDepartmentAsync), new { id = newDepartment.Id });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Department>> UpdateDepartmentAsync([FromRoute]int id, [FromBody]Department department)
        {
            if(id != department.Id)
            {
                return BadRequest("Not the same identifiers!");
            }
            await _repo.UpdateDepartmentAsync(department);
            return NoContent();
        }

        [HttpDelete]
        public async Task<ActionResult<Department>> DeleteDepartmentAsync(int id)
        {
            var deleteDepartment = await _repo.GetDepartmentAsync(id);
            if(deleteDepartment is null)
            {
                return NotFound();
            }
            await _repo.DeleteDepartmentAsync(deleteDepartment.Id);
            return NoContent();
        }
    }
}
