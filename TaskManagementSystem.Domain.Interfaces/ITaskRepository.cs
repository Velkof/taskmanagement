﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManagementSystem.Domain.Entities;

namespace TaskManagementSystem.Domain.Interfaces
{
    public interface ITaskRepository
    {
        List<Task> GetAll();
        Task GetById(int id);
        bool Create(Task task);
        bool Update(Task task);
        bool Delete(int id);
        bool ChangeProgress(string taskId, string newTaskProgress);

        List<TaskComment> GetComments(int taskId);      
        string GetAssignee(int taskId);

    }
}
