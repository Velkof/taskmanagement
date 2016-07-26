using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Interfaces;

namespace TaskManagementSystem.RepositoryEF.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        Database db = new Database();

        ICustomerRepository _customerRepository = new CustomerRepository();

        public bool Create(Project project)
        {
            var dbCustomer = _customerRepository.GetById(project.CustomerId);

            if(dbCustomer != null)
            {
                project.DateCreated = DateTime.Now;
                project.IsActive = true;

                db.Projects.Add(project);
                db.SaveChanges();

                return true;
            }
            return false;
        }

        public bool Delete(int id)
        {
            var dbProject = GetById(id);

            if (dbProject != null)
            {
                db.Projects.Remove(dbProject);
                db.SaveChanges();

                return true;
            }


            return false; 
        }

        public List<Project> GetAll()
        {
            return db.Projects.ToList();
        }

        public Project GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.ID == id);

        }

        public bool Update(Project project)
        {
            var dbProject = GetById(project.ID);

            if (dbProject != null)
            {

                dbProject.Name = project.Name;
                dbProject.IsActive = project.IsActive;

                db.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
