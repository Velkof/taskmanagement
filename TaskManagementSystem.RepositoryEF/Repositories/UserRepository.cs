using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Interfaces;

namespace TaskManagementSystem.RepositoryEF.Repositories
{
    public class UserRepository : IUserRepository
    {
        Database db = new Database();

        //    public bool Create(User user)
        //    {
        //        user.DateCreated = DateTime.Now;
        //        user.IsActive = false;
        //        db.Users.Add(user);
        //        db.SaveChanges();

        //        return true;
        //    }


        public bool Delete(string id)
        {
            var dbUser = GetById(id);

            if (dbUser != null)
            {
                var user = GetById(id);

                db.Users.Remove(user);
                db.SaveChanges();

                return true;
            }
            return false;
        }

        public List<User> GetAll()
        {
            return db.Users.ToList();
        }

        public User GetById(string id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public bool Update(User user)
        {
            var dbProject = GetById(user.Id);

            if (dbProject != null)
            {
                

                db.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
