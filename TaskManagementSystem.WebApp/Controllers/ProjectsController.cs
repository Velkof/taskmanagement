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
using TaskManagementSystem.RepositoryEF.Repositories;
using PagedList;
using Microsoft.AspNet.Identity;

namespace TaskManagementSystem.WebApp.Controllers
{


    public class ProjectsController : Controller
    {
        private IProjectRepository _projectRepository = new ProjectRepository();
        private ICustomerRepository _customerRepository = new CustomerRepository();
        private ITaskRepository _taskRepository = new TaskRepository();

       
        [Authorize(Roles ="User, Admin")]
        public ActionResult Index(int? page)
        {
            var projects = _projectRepository.GetAll();


            return View(projects.ToPagedList(page ?? 1, 5));
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.Customers = _customerRepository.GetAll(); 

            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Project project)
        {
            if(ModelState.IsValid)
            {
                if (_projectRepository.Create(project))
                    return RedirectToAction("Index");
            }
            return RedirectToAction("Create");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            var project = _projectRepository.GetById(id);

            return View(project);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Project project)
        {
            if (_projectRepository.Delete(project.ID))
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            var project = _projectRepository.GetById(id);

            return View(project);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Project project)
        {
            
            if (_projectRepository.Update(project))
                return RedirectToAction("Index");

            return View();
        }

        [Authorize(Roles = "User, Admin")]
        public ActionResult Details(int id)
        {
            var project = _projectRepository.GetById(id);

            return View(project);
        }

        [Authorize(Roles = "User, Admin")]
        public ActionResult TaskList(TaskRepository _taskRepository, int id)
        {
            string projectName = _projectRepository.GetById(id).Name;

            ViewBag.ProjectName = projectName;
            var tasksForSelectedProject = _taskRepository.GetAll().Where(x => x.ProjectId == id);

            return View(tasksForSelectedProject);

        }
    }
}
