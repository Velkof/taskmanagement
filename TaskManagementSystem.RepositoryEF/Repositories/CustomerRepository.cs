using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Interfaces;

namespace TaskManagementSystem.RepositoryEF
{
    public class CustomerRepository : ICustomerRepository
    {
        Database db = new Database();

        public string MySecretCustomerPassword()
        {
            return "secretPassword";
        }

        public bool Create(Customer customer)
        {
            customer.DateCreated = DateTime.Now;
            customer.IsActive = true;

            db.Customers.Add(customer);
            db.SaveChanges();

            return true;

        }

        public bool Delete(int id)
        {
            var dbCustomer = GetById(id);
            if(dbCustomer != null)
            {
                db.Customers.Remove(dbCustomer);
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Customer> GetAll()
        {
            return db.Customers.ToList();
        }

        public Customer GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.ID == id);
        }

        public bool Update(Customer customer)
        {
            var dbCustomer = GetById(customer.ID);
            
            if(dbCustomer != null)
            {
                dbCustomer.Name = customer.Name;
                dbCustomer.Company = customer.Company;
                dbCustomer.Email = customer.Email;
                dbCustomer.IsActive = customer.IsActive;

                db.SaveChanges();
                return true;
            }        
            return false;
        }

    }
}
