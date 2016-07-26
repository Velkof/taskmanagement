using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Web;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Interfaces;
using TaskManagementSystem.RepositoryEF.Repositories;

namespace TaskManagementSystem.RepositoryEF
{
    public class TaskRepository : ITaskRepository
    {
        Database db = new Database();

        IProjectRepository _projectRepository = new ProjectRepository();


        public bool Create(Task task)
        {
            var dbProject = _projectRepository.GetById(task.ProjectId);

                if(dbProject != null)
            {
                task.IsActive = true;
                task.DateCreated = DateTime.Now;
                task.UserId = HttpContext.Current.User.Identity.GetUserId();
                db.Tasks.Add(task);
                db.SaveChanges();

                return true;     
            }
            return false;
        }


        public bool Delete(int id)
        {
            var dbTask = GetById(id);

            if (dbTask != null)
            {
                db.Tasks.Remove(dbTask);
                db.SaveChanges();

                return true;
            }


            return false;
        }



        public List<Task> GetAll()
        {
            return db.Tasks.ToList();
        }

        public string GetAssignee(int taskId)
        {
            throw new NotImplementedException();
        }

        public Task GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.ID == id);
        }

        public List<TaskComment> GetComments(int taskId)
        {
            throw new NotImplementedException();
        }

        public bool ChangeProgress(string taskId, string newTaskProgress)
        {
            int id = Int32.Parse(taskId);

            var dbTask = GetById(id);

            if (dbTask != null)
            {
                switch (newTaskProgress)
                {
                    case "ToDo":
                        dbTask.Progress = TaskProgress.ToDo;
                        break;
                    case "InProgress":
                        dbTask.Progress = TaskProgress.InProgress;
                        break;
                    case "InTestingQA":
                        dbTask.Progress = TaskProgress.InTestingQA;
                        break;
                    case "Done":
                        dbTask.Progress = TaskProgress.Done;
                        break;
                    default:
                        break;
                }

                db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool Update(Task task)
        {
            var dbTask = GetById(task.ID);

            if (dbTask != null)
            {

                dbTask.Name = task.Name;
                dbTask.IsActive = task.IsActive;
                //dbTask.Progress = task.Progress;
                dbTask.StartDateTime = task.StartDateTime;
                dbTask.EndDateTime = task.EndDateTime;
                dbTask.Description = task.Description;
                dbTask.Type = task.Type;
                dbTask.EstimatedHours = task.EstimatedHours;
                //dbTask.DateCreated = task.DateCreated;

                if (dbTask.Progress.ToString() == "ToDo" && task.Progress.ToString() != "Done")
                    dbTask.Progress = task.Progress;
                else if (dbTask.Progress.ToString() == "InProgress")
                    dbTask.Progress = task.Progress;
                else if (dbTask.Progress.ToString() == "InTestingQA" && task.Progress.ToString() != "InProgress")
                    dbTask.Progress = task.Progress;
                else
                    task.Progress = task.Progress;


                db.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
