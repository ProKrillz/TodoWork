﻿using TodoWork.BLL.DTOModels;
using TodoWork.Domain.Entities;

namespace TodoWork.BLL.TodoServices
{
    public interface ITodoServices
    {
        void CreateTask(DTOTodo todo);
        void UpdateTask(DTOTodo todo);
        List<DTOTodo> GetAllTask();
        void CompletTask(int id);
        void DeleteTask(int id);
    }
}
