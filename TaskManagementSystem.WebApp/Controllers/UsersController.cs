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
using TaskManagementSystem.WebApp.Models;
using PagedList;



namespace TaskManagementSystem.WebApp.Controllers
{
    [Authorize(Roles ="Admin")]
    public class UsersController : Controller
    {
        RepositoryEF.Database db = new RepositoryEF.Database();
        IUserRepository _userRepository = new UserRepository();

        public ActionResult Index(int? page)
        {


            var admins = (from u in db.Users
                          where u.Roles.Any(r => r.RoleId == "92a2598a-0481-4462-809d-a2001e035c4a")
                          select u).ToList();

            var allUsers = db.Users.ToList();

            var users = allUsers.Except(admins).ToList();
            ViewBag.Admins = admins;

            return View(users.ToPagedList(page ?? 1, 5));
        }

        public JsonResult GetUserId(User user)
        {
            return Json(User.Identity.GetUserId(), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "User, Admin")]
        public ActionResult TaskList(TaskRepository _taskRepository, string id, int? page)
        {
            string userName = db.Users.FirstOrDefault(x => x.Id == id).UserName;

            ViewBag.UserName = userName;
            var tasksForSelectedUser = _taskRepository.GetAll().Where(x => x.UserId == id);

            return View(tasksForSelectedUser.ToPagedList(page ?? 1, 5));
        }



        public ActionResult Details(string id)
        {
            string userId = db.Users.FirstOrDefault(x => x.Id == id).Id;
            var user = _userRepository.GetById(id);

            return View(user);
        }

        public ActionResult Delete(string id)
        {
            var task = _userRepository.GetById(id);

            return View(task);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(User user)
        {

            if (_userRepository.Delete(user.Id))
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Edit(string id)
        {
            var user = _userRepository.GetById(id);

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {

            if (_userRepository.Update(user))
                return RedirectToAction("Index");

            return View();
        }
    }
}
