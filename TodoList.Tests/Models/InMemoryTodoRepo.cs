using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Models;
using TodoList.Service;
using static TodoList.Models.AccountViewModels;

namespace TodoList.Tests.Models
{
  public class InMemoryTodoRepo : ITodoListService, IDisposable
    {
        private List<Todo> _db = new List<Todo>();
        public Exception ExceptionToThrow { get; set; }

        public UserManager<ApplicationUser> Getmanager()
        {
            //var userStore = new Mock<IUserStore<ApplicationUser>>();
            //var userManager = new UserManager(userStore.Object);
            //var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
            //return new UserManager<ApplicationUser>(
            //    userStoreMock.Object, null, null, null, null, null, null, null, null, null, null, null, null, null);
            var mockStore = new Mock<IUserStore<ApplicationUser>>();
            var userManager = new UserManager<ApplicationUser>(mockStore.Object);

            
            return userManager;
        }
        public IEnumerable<Todo> GetAllTodos()
        {
            return _db.ToList();
        }
        public IEnumerable<Todo> GetAllTodosByUser(ApplicationUser currentUser)
        {
            return _db.ToList().Where(todo => todo.User.Id == currentUser?.Id);
        }
        public Todo GetTodoByID(int? id)
        {
            return _db.FirstOrDefault(d => d.ID == id);
        }
        public void CreateNewTodo(Todo todoData)
        {
            if (ExceptionToThrow != null)
                throw ExceptionToThrow;
            _db.Add(todoData);
        }
        public void SaveChanges(Todo todoToUpdate)
        {
            foreach (Todo todoData in _db)
            {
                if (todoData.ID == todoToUpdate.ID)
                {
                    _db.Remove(todoData);
                    _db.Add(todoToUpdate);
                    break;
                }
            }
        }
        public void Add(Todo todoData)
        {
            _db.Add(todoData);
        }
        public int SaveChanges()
        {
            return 1;
        }
        public void DeleteTodo(int id)
        {
            _db.Remove(GetTodoByID(id));
        }
      
           
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db = null;
            }

        }
    }
}
