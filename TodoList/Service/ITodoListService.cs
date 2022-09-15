using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Models;
using static TodoList.Models.AccountViewModels;

namespace TodoList.Service
{
   public interface ITodoListService
    {
        
        IEnumerable<Todo> GetAllTodosByUser(ApplicationUser currentUser);
        IEnumerable<Todo> GetAllTodos();
        void CreateNewTodo(Todo todoData);
        void DeleteTodo(int id);
        Todo GetTodoByID(int? id);
        int SaveChanges();
        UserManager<ApplicationUser> Getmanager();
        void Dispose();


    }
}
