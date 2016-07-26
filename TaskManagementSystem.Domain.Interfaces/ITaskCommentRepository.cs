using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Domain.Interfaces
{
    public interface ITaskCommentRepository
    {
        List<TaskComment> GetAllForTask(int taskId);
        List<TaskComment> GetAll();
        TaskComment GetById(int id);
        bool Create(TaskComment taskComment);
        bool Update(TaskComment taskComment);
        bool Delete(int id);
    }
}
