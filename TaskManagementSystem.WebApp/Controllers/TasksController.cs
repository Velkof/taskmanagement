using Microsoft.AspNet.Identity;
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
using TaskManagementSystem.RepositoryEF.Repositories;

namespace TaskManagementSystem.WebApp.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        RepositoryEF.Database db = new RepositoryEF.Database();
        private ITaskRepository _taskRepository = new TaskRepository();
        private IProjectRepository _projectRepository = new ProjectRepository();
        private ITaskCommentRepository _taskCommentRepository = new TaskCommentRepository();


        // GET: Tasks
        public ActionResult Index(int? page)
        {
            var tasks = _taskRepository.GetAll();

            return View(tasks.ToPagedList(page ?? 1, 5));
        }

        public ActionResult Create(string projectID)
        {
            ViewBag.UserId = User.Identity.GetUserId();            

            ViewBag.ProjectId = projectID;

            ViewBag.Projects = _projectRepository.GetAll().ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Task task, string projectID)
        {
            task.ProjectId = Int32.Parse(projectID);


            if (ModelState.IsValid)
            {
                if (_taskRepository.Create(task))
                    return RedirectToAction("TaskList", "Projects", new { id = task.ProjectId });
            }
            return RedirectToAction("Create");
        }

        public ActionResult Details(int id)
        {
            var task = _taskRepository.GetById(id);

            return View(task);
        }
        public ActionResult Delete(int id)
        {
            var task = _taskRepository.GetById(id);

            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Task task)
        {


            if (_taskRepository.Delete(task.ID))
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Edit(int id)
        {
            var task = _taskRepository.GetById(id);

            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Task task)
        {

            if (_taskRepository.Update(task))
                return RedirectToAction("Index");

            return View();
        }

        [HttpPost]
        public JsonResult ChangeProgress(string taskId, string taskProgress)
        {

            _taskRepository.ChangeProgress(taskId, taskProgress);
              return Json(new { success = true });
        }

        public ActionResult TaskCommentList(TaskCommentRepository _taskCommentRepository, int id, int? page)
        {

            ViewBag.TaskID = id.ToString();

            var taskCommentsForSelectedTask = _taskCommentRepository.GetAll().Where(x => x.TaskId == id);

            return View(taskCommentsForSelectedTask.ToPagedList(page ?? 1, 5));
        }



    }
}
