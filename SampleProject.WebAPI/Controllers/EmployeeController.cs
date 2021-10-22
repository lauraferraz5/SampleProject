using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleProject.Repo;
using SampleProject.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleProject.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ISampleProjectRepository _repo;

        public EmployeeController(ISampleProjectRepository repo)
        {
            _repo = repo;
        }


        // GET: api/Employees
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var employees = await _repo.GetAllEmployees(true);

                return Ok(employees);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // GET: api/Employee/5
        [HttpGet("{id}", Name = "GetEmployeeById")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var employees = await _repo.GetEmployeeById(id, true);

                return Ok(employees);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // POST api/Employee
        [HttpPost]
        public async Task<IActionResult> Post(Employee model)
        {
            try
            {
                _repo.Add(model);

                if (await _repo.SaveChangeAsync())
                {
                    return Ok("Employee added successfully!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
            return BadRequest("Not saved!");
        }

        // PUT: api/Employee/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Employee model)
        {
            try
            {
                var employee = await _repo.GetEmployeeById(id);

                if (employee != null)
                {
                    _repo.Update(model);

                    if (await _repo.SaveChangeAsync())
                        return Ok("Employee updated successfully!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
            return Ok("Employee not found!");
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var employee = await _repo.GetEmployeeById(id);

                if (employee != null)
                {
                    _repo.Delete(employee);

                    if (await _repo.SaveChangeAsync())
                        return Ok("Employee deleted successfully!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
            return Ok("Employee not deleted!");
        }
    }
}

