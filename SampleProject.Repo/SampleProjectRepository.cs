using Microsoft.EntityFrameworkCore;
using SampleProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Repo
{
    public class SampleProjectRepository : ISampleProjectRepository
    {
        private readonly EmployeeContext _context;

        public SampleProjectRepository(EmployeeContext context)
        {
            _context = context;
        }
        public void Add<E>(E entity) where E : class
        {
            _context.Add(entity);
        }

        public void Update<E>(E entity) where E : class
        {
            _context.Update(entity);
        }

        public void Delete<E>(E entity) where E : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangeAsync()
        {
           return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<Employee[]> GetAllEmployees(bool includeProject = false)
        {
            IQueryable<Employee> query = _context.Employees;

            if (includeProject)
            {
                query = query.Include(e => e.Assignments).ThenInclude(employeeassig => employeeassig.Project);
            }

            query = query.AsNoTracking().OrderBy(e => e.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Employee> GetEmployeeById(int id, bool includeProject = false)
        {
            IQueryable<Employee> query = _context.Employees;

            if (includeProject)
            {
                query = query.Include(e => e.Assignments).ThenInclude(employeeassig => employeeassig.Project);
            }

            query = query.AsNoTracking().OrderBy(e => e.Id);

            return await query.FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<Project[]> GetAllProjects(bool includeEmployee = false)
        {
            IQueryable<Project> query = _context.Projects;

            if (includeEmployee)
            {
                query = query.Include(p => p.Assignments).ThenInclude(employeeassig => employeeassig.Employee);
            }

            query = query.AsNoTracking().OrderBy(p => p.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Project> GetProjectById(int id, bool includeEmployee = false)
        {
            IQueryable<Project> query = _context.Projects;

            if (includeEmployee)
            {
                query = query.Include(p => p.Assignments).ThenInclude(employeeassig => employeeassig.Employee);
            }

            query = query.AsNoTracking().OrderBy(p => p.Id);

            return await query.FirstOrDefaultAsync();
        }
    }
}
