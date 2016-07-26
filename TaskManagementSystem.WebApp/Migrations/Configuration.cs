namespace TaskManagementSystem.WebApp.Migrations
{
    using Domain.Entities;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TaskManagementSystem.RepositoryEF.Database>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TaskManagementSystem.RepositoryEF.Database context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //


            context.Roles.AddOrUpdate(r => r.Name,
                new IdentityRole { Name = "InactiveUser"},
                new IdentityRole { Name = "User"},               
                new IdentityRole { Name = "Admin"}
            );



            var UserManager = new UserManager<User>(new UserStore<User>(context)); //Adds a user to the admin role
            UserManager.AddToRole("469851ff-8274-4376-bdb1-d077a40e8ef8", "Admin");

           

        }
    }
}