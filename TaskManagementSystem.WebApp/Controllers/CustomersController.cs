using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Interfaces;
using TaskManagementSystem.RepositoryEF;
using PagedList;

namespace TaskManagementSystem.WebApp.Controllers
{
    [Authorize(Roles="Admin")]
    public class CustomersController : Controller
    {
        private ICustomerRepository _customerRepository = new CustomerRepository();

        // GET: Customers
        public ActionResult Index(int? page)
        {
            var customers = _customerRepository.GetAll();
            return View(customers.ToPagedList(page ?? 1, 5));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer customer)
        {
            if(ModelState.IsValid)
            {
                if (_customerRepository.Create(customer))
                    return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Details(int id)
        {
            var customer = _customerRepository.GetById(id);

            return View(customer);
        }


        public ActionResult Delete(int id)
        {
            var customer = _customerRepository.GetById(id);

            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Customer customer)
        {
            if (_customerRepository.Delete(customer.ID))
            {
                return RedirectToAction("Index");
            }
            return View(); 
        }


        public ActionResult Edit(int id)
        {
            var customer = _customerRepository.GetById(id);

            return View(customer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Customer customer)
        {
            if (_customerRepository.Update(customer))
                return RedirectToAction("Index");

            return View();
        }

    }
}
