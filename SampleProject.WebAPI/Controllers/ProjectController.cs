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
    public class ProjectController : ControllerBase
    {
        private readonly ISampleProjectRepository _repo;

        public ProjectController(ISampleProjectRepository repo)
        {
            _repo = repo;
        }


        // GET: api/Projects
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var projects = await _repo.GetAllProjects(true);

                return Ok(projects);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // GET: api/Project/5
        [HttpGet("{id}", Name = "GetProjectById")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var projects = await _repo.GetProjectById(id, true);

                return Ok(projects);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
        }

        // POST api/Project
        [HttpPost]
        public async Task<IActionResult> Post(Project model)
        {
            try
            {
                _repo.Add(model);

                if (await _repo.SaveChangeAsync())
                {
                    return Ok("Project added successfully!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
            return BadRequest("Not saved!");
        }

        // PUT: api/Project/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Project model)
        {
            try
            {
                var project = await _repo.GetProjectById(id);

                if (project != null)
                {
                    _repo.Update(model);

                    if (await _repo.SaveChangeAsync())
                        return Ok("Project updated successfully!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
            return Ok("Project not found!");
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var project = await _repo.GetProjectById(id);

                if (project != null)
                {
                    _repo.Delete(project);

                    if (await _repo.SaveChangeAsync())
                        return Ok("Project deleted successfully!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro: {ex}");
            }
            return Ok("Project not deleted!");
        }
    }
}

