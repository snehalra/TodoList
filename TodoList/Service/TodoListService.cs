using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TodoList.Models;
using static TodoList.Models.AccountViewModels;

namespace TodoList.Service
{
    [Authorize]
    public class TodoListService : ITodoListService, IDisposable
    {
        //entity object

        private RecordContext _db = new RecordContext();

        public IEnumerable<Todo> GetAllTodosByUser(ApplicationUser currentUser)
        {            
            return _db.Todo.ToList().Where(todo => todo.User?.Id == currentUser.Id);
        }
        public IEnumerable<Todo> GetAllTodos()
        {
            return _db.Todo.ToList();
        }
        public Todo GetTodoByID (int? id)
        {
            return _db.Todo.FirstOrDefault(d => d.ID == id);
        }

        public void CreateNewTodo(Todo todoData)
        {
            _db.Todo.Add(todoData);
            _db.SaveChanges();
        }

        public int SaveChanges()
        {
            return _db.SaveChanges();
        }

        public void DeleteTodo(int id)
        {
            var todoToDel = GetTodoByID(id);
            _db.Todo.Remove(todoToDel);
            _db.SaveChanges();
        }
        public UserManager<ApplicationUser> Getmanager()
        {
            return new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_db));
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}