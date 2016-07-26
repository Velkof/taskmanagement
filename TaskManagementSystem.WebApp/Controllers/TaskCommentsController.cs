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
using TaskManagementSystem.RepositoryEF.Repositories;

namespace TaskManagementSystem.WebApp.Controllers
{
    public class TaskCommentsController : Controller
    {
        private RepositoryEF.Database db = new RepositoryEF.Database();
        ITaskCommentRepository _taskCommentRepository = new TaskCommentRepository();
        ITaskRepository _taskRepository = new TaskRepository();




        // GET: TaskComments
        public ActionResult Index()
        {
            var taskComments = _taskCommentRepository.GetAll();
            return View(taskComments);
        }


        public ActionResult Create(string taskID)
        {

            ViewBag.UserId = User.Identity.GetUserId();

            ViewBag.TaskId = taskID;

            ViewBag.Tasks = _taskRepository.GetAll().ToList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TaskComment taskComment, string taskID)
        {
            taskComment.TaskId = Int32.Parse(taskID);

            if (ModelState.IsValid)
            {
                if (_taskCommentRepository.Create(taskComment))
                    return RedirectToAction("TaskCommentList", "Tasks", new { id = taskComment.TaskId });
            }
            return RedirectToAction("Create");
        }


        public ActionResult Delete(int id)
        {
            var taskComment = _taskCommentRepository.GetById(id);

            return View(taskComment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(TaskComment taskComment)
        {
            if (_taskCommentRepository.Delete(taskComment.ID))
            {
                return RedirectToAction("Index");
            }

            return View();
        }


        public ActionResult Edit(int id)
        {
            var taskComment = _taskCommentRepository.GetById(id);

            return View(taskComment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TaskComment taskComment)
        {

            if (_taskCommentRepository.Update(taskComment))
                return RedirectToAction("Index");

            return View();
        }

        public ActionResult Details(int id)
        {
            var taskComment = _taskCommentRepository.GetById(id);

            return View(taskComment);
        }
    }
}
