using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Domain.Interfaces;
using TaskManagementSystem.RepositoryEF;

namespace TaskManagementSystem.RepositoryEF.Repositories
{
    public class TaskCommentRepository : ITaskCommentRepository
    {
        Database db = new Database();

        ITaskRepository _taskRepository = new TaskRepository();

        public bool Create(TaskComment taskComment)
        {
            var dbTask = _taskRepository.GetById(taskComment.TaskId); 

            if (dbTask != null )
            {
                taskComment.DateCreated = DateTime.Now;
                taskComment.IsActive = true;
                taskComment.UserId = HttpContext.Current.User.Identity.GetUserId();

                db.TaskComments.Add(taskComment);
                db.SaveChanges();
                return true;
            }
            return false;
        }


        public bool Delete(int id)
        {
            var dbTaskComment = GetById(id);

            if (dbTaskComment != null)
            {
                db.TaskComments.Remove(dbTaskComment);
                db.SaveChanges();

                return true;
            }
            return false;
        }

        public List<TaskComment> GetAllForTask(int taskId)
        {
            return db.TaskComments.Where(x => x.TaskId == taskId).ToList();

        }

        public List<TaskComment> GetAll()
        {
            return db.TaskComments.ToList();

        }

        public TaskComment GetById(int id)
        {
            return db.TaskComments.FirstOrDefault(x => x.ID == id);
        }


        public bool Update(TaskComment taskComment)
        {
            var dbTaskComment = GetById(taskComment.ID);

            if (dbTaskComment != null)
            {
                dbTaskComment.IsActive = taskComment.IsActive;
                dbTaskComment.Comment = taskComment.Comment;

                db.SaveChanges();
                return true;
            }

            return false;
        }

    }
}
