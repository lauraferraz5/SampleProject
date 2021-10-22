using SampleProject.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleProject.Repo
{
    public interface ISampleProjectRepository
    {
        void Add<E>(E entity) where E : class;
        void Update<E>(E entity) where E : class;
        void Delete<E>(E entity) where E : class;

        Task<bool> SaveChangeAsync();

        Task<Employee[]> GetAllEmployees(bool includeProject = false);
        Task<Employee> GetEmployeeById(int id, bool includeProject = false);

        Task<Project[]> GetAllProjects(bool includeEmployee = false);
        Task<Project> GetProjectById(int id, bool includeEmployee = false);
    }
}
