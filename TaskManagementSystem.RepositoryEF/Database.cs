using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.RepositoryEF
{

    public class Database : IdentityDbContext<User>
    {

        public static Database Create()
        {
            return new Database();
        }

        public DbSet<Customer> Customers { get; set;}
        public DbSet<Project> Projects { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<TaskComment> TaskComments { get; set; }


    }
}
